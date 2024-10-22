using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockMarket.Attributes;
using StockMarket.Data.Repositories;
using StockMarket.Dtos.Holding;
using StockMarket.Services;

namespace StockMarket.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController(IStockServices stockServices, IHoldingServices holdingServices, IPortfolioRepository portfolioRepository) : ControllerBase
    {
        private readonly IStockServices _stockServices = stockServices;
        private readonly IHoldingServices _holdingServices = holdingServices;
        private readonly IPortfolioRepository _portfolioRepository = portfolioRepository;

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

        [HttpPut("buy")]
        [Authorize]
        public async Task<IActionResult> BuyStock([FromBody] BuySellRequestDTO buyRequest) {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return BadRequest("400");

            var holding = await _holdingServices.Buy(buyRequest, int.Parse(userId));

            if (holding == null) return BadRequest("400");

            return Ok(holding);
        }

        [HttpPut("sell")]
        [Authorize]
        public async Task<IActionResult> SellStock([FromBody] BuySellRequestDTO sellRequest) {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return BadRequest("400");

            var holding = await _holdingServices.Sell(sellRequest, int.Parse(userId));

            if (holding == null) return BadRequest("400");

            return Ok(holding);
        }

        [HttpGet("holdings")]
        [Authorize]
        public async Task<IActionResult> GetHoldings() {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return BadRequest("400");

            var portfolio = await _portfolioRepository.GetPortfolioByUserIdAsync(int.Parse(userId));
            if (portfolio == null) return BadRequest("400");

            var holdings = await _holdingServices.GetHoldingByPortfolioIdAsync(portfolio.PortfolioId);

            return Ok(holdings);
        }

        [HttpGet("holding/{stockId}")]
        [Authorize]
        public async Task<IActionResult> GetHolding(int stockId) {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return BadRequest("400");

            var holding = await _holdingServices.GetHoldingByPortfolioIdAndStockIdAsync(int.Parse(userId), stockId);

            return Ok(holding);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetStockById(int id) {
            var stock = await _stockServices.GetStockByIdAsync(id);

            if (stock == null) return NotFound("404");

            return Ok(stock);
        }
    }
}