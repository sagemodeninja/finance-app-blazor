using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
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
        public async Task<IActionResult> GetById()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public async Task<IActionResult> GetByAccountId(Guid accountId)
        {
            try {
                return Ok();
            } catch {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(User user)
        {
            try
            {
                // Checks if user already exist.
                if(_dbContext.Users.Any(u => u.AccountId == user.AccountId)) {
                    user = _dbContext.Users.Single(u => u.AccountId == user.AccountId);
                    return Ok(user);
                } else {
                    // Generate TOTP secret based on account id and secret key.
                    string totpSecret = await GenerateUserTOTPSecretAsync(user.AccountId, _options.TOTPHashKey);
                    user.TOTPSecret = totpSecret;

                    _dbContext.Users.Add(user);
                    await _dbContext.SaveChangesAsync();
                    return Ok(user);
                }
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("mfatoken/generate/{identity:guid}")]
        public async Task<IActionResult> GenerateMFAToken(Guid identity)
        {
            MFAToken token = await MFAToken.GenerateAsync(identity.ToString(), _options.MFATokenHashKey);
            return Ok(token);
        }

        [HttpPut]
        [Route("mfatoken/validate")]
        public async Task<IActionResult> ValidateMFAToken(MFAToken token)
        {
            await token.ValidateAsync(_options.MFATokenHashKey);
            return Ok(token);
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

                return Convert.ToBase64String(hashValue);
            }
        }
    }
}