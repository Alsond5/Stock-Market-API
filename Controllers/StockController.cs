using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockMarket.Attributes;
using StockMarket.Dtos.Stock;
using StockMarket.Services;

namespace StockMarket.Controllers
{
    [Route("api/stocks")]
    [ApiController]
    public class StockController(IStockServices stockServices) : ControllerBase
    {
        private readonly IStockServices _stockServices = stockServices;

        [HttpGet("all")]
        [Authorize]
        public async Task<IActionResult> GetAllStocks() {
            var stocks = await _stockServices.GetAllStocksAsync();

            return Ok(stocks);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetStockById(int id) {
            var stock = await _stockServices.GetStockByIdAsync(id);
            if (stock == null) return NotFound("404");

            return Ok(stock);
        }

        // UPDATE STOCK QUANTITY
        [HttpPut("{id}")]
        [RoleAuthorize(2)]
        public async Task<IActionResult> UpdateStockQuantity(int id, [FromBody] UpdateStockQuantityRequestDTO request) {
            var stock = await _stockServices.UpdateStockQuantityAsync(id, request);
            if (stock == null) return NotFound("404");

            return Ok(stock);
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

        [HttpPut("activate/{id}")]
        [RoleAuthorize(2)]
        public async Task<IActionResult> ActivateStock(int id) {
            var stock = await _stockServices.ActivateStockAsync(id);
            if (stock == null) return NotFound("404");

            return Ok(stock);
        }

        [HttpPut("activate/all")]
        [RoleAuthorize(2)]
        public async Task<IActionResult> ActivateAllStocks() {
            var stocks = await _stockServices.ActivateAllStocksAsync();

            return Ok(stocks);
        }
    }
}