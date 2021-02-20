using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Web.Resource;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using FinanceApp.Server.Data;
using FinanceApp.Server.Models;

namespace FinanceApp.Server.Controllers 
{

    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase 
    {
        private readonly FinanceAppContext _dbContext;

        public CategoryController(FinanceAppContext context)
        {
            _dbContext = context;
        }

        static readonly string[] scopeRequiredByApi = new string[] { "api.access" };

        [HttpGet]
        public async Task<IActionResult> GetAllAsync() {
            HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
            
            var accounts = await _dbContext.Accounts.ToListAsync();
            return Ok(accounts);
        }
    }
}