using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockMarket.Data.Repositories;
using StockMarket.Dtos.Holding;
using StockMarket.Services;

namespace StockMarket.Controllers
{
    [Route("api/holdings")]
    [ApiController]
    public class HoldingController(IHoldingServices holdingServices, IPortfolioRepository portfolioRepository) : ControllerBase
    {
        private readonly IHoldingServices _holdingServices = holdingServices;
        private readonly IPortfolioRepository _portfolioRepository = portfolioRepository;

        [HttpGet("@me/all")]
        [Authorize]
        public async Task<IActionResult> GetHoldings() {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return BadRequest("400");

            var portfolio = await _portfolioRepository.GetPortfolioByUserIdAsync(int.Parse(userId));
            if (portfolio == null) return BadRequest("400");

            var holdings = await _holdingServices.GetHoldingByPortfolioIdAsync(portfolio.PortfolioId);

            return Ok(holdings);
        }

        [HttpGet("@me/{stockId}")]
        [Authorize]
        public async Task<IActionResult> GetHolding(int stockId) {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return BadRequest("400");

            var portfolio = await _portfolioRepository.GetPortfolioByUserIdAsync(int.Parse(userId));
            if (portfolio == null) return BadRequest("400");

            var holding = await _holdingServices.GetHoldingByPortfolioIdAndStockIdAsync(portfolio.PortfolioId, stockId);

            return Ok(holding);
        }

        [HttpGet("@me/holding/{holdingId}")]
        [Authorize]
        public async Task<IActionResult> GetHoldingById(int holdingId) {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return BadRequest("400");

            var holding = await _holdingServices.GetHoldingByIdAsync(int.Parse(userId), holdingId);
            if (holding == null) return BadRequest("400");

            return Ok(holding);
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
    }
}