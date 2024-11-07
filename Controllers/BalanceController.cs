using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StockMarket.Attributes;
using StockMarket.Services;

namespace StockMarket.Controllers
{
    [ApiController]
    [Route("api/balances")]
    public class BalanceController(IBalanceServices balanceServices) : ControllerBase
    {
        private readonly IBalanceServices _balanceServices = balanceServices;

        [HttpGet("all")]
        [RoleAuthorize(2)]
        public async Task<IActionResult> GetAllBalances()
        {
            var balances = await _balanceServices.GetBalances();
            if (balances == null) return NotFound("404");
            
            return Ok(balances);
        }
    }
}