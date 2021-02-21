using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OtpNet;
using FinanceApp.Server.Data;
using FinanceApp.Server.Classes;
using FinanceApp.Shared.Models;
using FinanceApp.Shared.Classes;

namespace FinanceApp.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly FinanceAppContext _dbContext;

        private readonly FinanceAppOptions _options;

        public UserController(FinanceAppContext context, IOptions<FinanceAppOptions> options)
        {
            _dbContext = context;
            _options = options.Value;
        }

        static readonly string[] scopeRequiredByApi = new string[] { "api.access" };

        [HttpGet]
        [Route("account/{accountId:guid}")]
        public IActionResult GetByAccountId(Guid accountId)
        {
            User user = _dbContext.Users.SingleOrDefault(u => u.AccountId == accountId);
            if(user != null)
            {
                // Note: GetByAccountId() is an endpoint that is expicted to expose a user's TOTP secret.
                // To avoid abuse this check is enforced.
                if(user.HasRegisteredMFA)
                {
                    // Note: TOTPSecret is cleared so it wouldn't get leaked.
                    user.TOTPSecret = null;
                }

                return Ok(user);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(User user)
        {
            // Note: AddUser() is an endpoint that exposes a user's TOTP secret.
            // To avoid abuse, be it unecessary, this check is enforced.
            if(!_dbContext.Users.Any(u => u.AccountId == user.AccountId))
            {
                // Generate TOTP secret based on account id and hash key.
                user.TOTPSecret = await GenerateUserTOTPSecretAsync(user.AccountId, _options.TOTPHashKey);

                _dbContext.Users.Add(user);
                await _dbContext.SaveChangesAsync();
                
                return Ok(user);
            }
            else
            {
                return BadRequest("User already exists.");
            }
        }

        [HttpPut]
        [Route("totp/verify")]
        public async Task<IActionResult> VerifyTOTPCode(TOTPRequest request)
        {
            User user = _dbContext.Users.SingleOrDefault(u => u.Id == request.RequestorId);
            if(user is null)
                return NotFound("User does not exist.");

            byte[] totpSecret = Base32Encoding.ToBytes(user.TOTPSecret);
            Totp totp = new Totp(totpSecret);

            VerificationWindow window = new VerificationWindow(previous: 1, future: 1);
            bool isValid = totp.VerifyTotp(request.Code, out var _, window);
            if(isValid)
            {
                MFAToken token = await MFAToken.GenerateAsync(user.AccountId, user.TOTPSecret);
                return Ok(token);
            }
            else
            {
                return BadRequest("Code is invalid.");
            }
        }

        [HttpPut]
        [Route("mfatoken/validate")]
        public async Task<IActionResult> ValidateMFAToken(MFAToken token)
        {
            Guid identity = token.Identity;
            User user = _dbContext.Users.SingleOrDefault(u => u.AccountId == identity);

            if(user is null)
                return NotFound();

            await token.ValidateAsync(user.TOTPSecret);
            return Ok(token);
        }

        [HttpPut]
        [Route("mfa/register")]
        public async Task<IActionResult> TagRegisteredMFA(User user)
        {
            user.HasRegisteredMFA = true;
            user.EnableMFA = true;
            _dbContext.Users.Update(user);
            
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        private static async Task<string> GenerateUserTOTPSecretAsync(Guid accountId, string hashKey)
        {
            // refer: https://stackoverflow.com/questions/11743160/how-do-i-encode-and-decode-a-base64-string
            string plainText = accountId.ToString();
            byte[] plainTextBytes = System.Text.Encoding.UTF8.GetBytes($"{hashKey}.{plainText}.{hashKey}");
            
            using (SHA256 sha256 = SHA256.Create())
            {
                Stream stream = new MemoryStream(plainTextBytes);
                byte[] hashValue = await sha256.ComputeHashAsync(stream);

                return Base32Encoding.ToString(hashValue);
            }
        }
    }
}