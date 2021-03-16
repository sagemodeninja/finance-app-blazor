using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using FinanceApp.Server.Data;
using FinanceApp.Shared.Models;
using FinanceApp.Shared.Enums;

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
            List<AccountVendor> vendors;
            vendors = await _dbContext.AccountVendors.Where(av => av.Status == GenericStatus.Active)
                                                    .ToListAsync();
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

        [HttpDelete]
        [Route("{id:long}")]
        public async Task<IActionResult> DeleteAccountVendor(long id)
        {
            AccountVendor vendor = await _dbContext.AccountVendors.FirstOrDefaultAsync(av => av.Id == id);
            if(vendor is null)
            {
                return NotFound();
            }

            vendor.Status = GenericStatus.Inactive;
            _dbContext.AccountVendors.Update(vendor);
            await _dbContext.SaveChangesAsync();
            
            return Ok();
        }
    }
}