using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockMarket.Attributes;
using StockMarket.Models;
using StockMarket.Services;

namespace StockMarket.Controllers
{
    [Route("api/transaction")]
    [ApiController]
    public class TransactionController(ITransactionServices transactionServices) : ControllerBase
    {
        private readonly ITransactionServices _transactionServices = transactionServices;

        [HttpGet("all")]
        [RoleAuthorize(2)]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetAllTransactionsAsync()
        {
            var transactions = await _transactionServices.GetAllTransactionsAsync();
            return Ok(transactions);
        }

        [HttpGet("{transactionId}")]
        [RoleAuthorize(2)]
        public async Task<ActionResult<Transaction>> GetTransactionByIdAsync(int transactionId)
        {
            var transaction = await _transactionServices.GetTransactionByIdAsync(transactionId);
            if (transaction == null)
            {
                return NotFound();
            }
            return Ok(transaction);
        }

        [HttpGet("@me/all")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactionsByUserIdAsync()
        {
            var user = User.FindFirst(ClaimTypes.NameIdentifier);
            if (user == null) return Unauthorized();

            var userId = int.Parse(user.Value);

            var transactions = await _transactionServices.GetTransactionsByUserIdAsync(userId);
            return Ok(transactions);
        }

        [HttpGet("@me/{stockId}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactionsByStockIdAsync(int stockId)
        {
            var user = User.FindFirst(ClaimTypes.NameIdentifier);
            if (user == null) return Unauthorized();

            var userId = int.Parse(user.Value);

            var transactions = await _transactionServices.GetTransactionsByUserIdAndStockIdAsync(userId, stockId);
            return Ok(transactions);
        }

        [HttpGet("range")]
        [RoleAuthorize(2)]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactionsByDateRangeForStockIdAsync([FromQuery] DateTime startDate, [FromQuery] DateTime endDate, [FromQuery] int stockId)
        {
            var transactions = await _transactionServices.GetTransactionsByDateRangeForStockIdAsync(startDate, endDate, stockId);
            return Ok(transactions);
        }

        [HttpGet("@me/range")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactionsByDateRangeAsync([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var user = User.FindFirst(ClaimTypes.NameIdentifier);
            if (user == null) return Unauthorized();

            var userId = int.Parse(user.Value);

            var transactions = await _transactionServices.GetTransactionsByDateRangeAsync(startDate, endDate, userId);
            return Ok(transactions);
        }

        [HttpGet("@me/range/{stockId}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactionsByDateRangeAndStockAsync([FromQuery] DateTime startDate, [FromQuery] DateTime endDate, int stockId)
        {
            var user = User.FindFirst(ClaimTypes.NameIdentifier);
            if (user == null) return Unauthorized();

            var userId = int.Parse(user.Value);

            var transactions = await _transactionServices.GetTransactionsByDateRangeAndStockAsync(startDate, endDate, stockId, userId);
            return Ok(transactions);
        }
    }
}