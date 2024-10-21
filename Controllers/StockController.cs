using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockMarket.Attributes;
using StockMarket.Services;

namespace StockMarket.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockServices _stockServices;
        
        public StockController(IStockServices stockServices)
        {
            _stockServices = stockServices;
        }

        [HttpGet("all")]
        [Authorize]
        public async Task<IActionResult> GetAllStocks() {
            var stocks = await _stockServices.GetAllStocksAsync();

            return Ok(stocks);
        }

        [HttpPut("deactivate/{id}")]
        [RoleAuthorize(2)]
        public async Task<IActionResult> DeactivateStock(int id) {
            var stock = await _stockServices.DeactivateStockAsync(id);

            if (stock == null) return NotFound("404");

            return Ok(stock);
        }

        [HttpPut("deactivate/all")]
        [RoleAuthorize(2)]
        public async Task<IActionResult> DeactivateAllStocks() {
            var stocks = await _stockServices.DeactivateAllStocksAsync();

            return Ok(stocks);
        }
    }
}