using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using FinanceApp.Server.Data;
using FinanceApp.Shared.Models;

namespace FinanceApp.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AccountVendorController : ControllerBase
    {
        private readonly FinanceAppContext _dbContext;

        public AccountVendorController(FinanceAppContext dbContext)
        {
            _dbContext = dbContext;
        }

        static readonly string[] scopeRequiredByApi = new string[] { "api.access" };

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<AccountVendor> vendors = await _dbContext.AccountVendors.ToListAsync();
            return Ok(vendors);
        }

        [HttpPost]
        public async Task<IActionResult> AddAccountVendor(AccountVendor vendor)
        {
            _dbContext.AccountVendors.Add(vendor);
            await _dbContext.SaveChangesAsync();
            return Ok(vendor);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAccountVendor(AccountVendor vendor)
        {
            _dbContext.AccountVendors.Update(vendor);
            await _dbContext.SaveChangesAsync();
            return Ok(vendor);
        }
    }
}