using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockMarket.Attributes;
using StockMarket.Models;
using StockMarket.Services;

namespace StockMarket.Controllers
{
    [Route("api/transactions")]
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
            if (transaction == null) return NotFound();

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

        [HttpGet("download")]
        [Authorize]
        public async Task<ActionResult> DownloadTransactions([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var user = User.FindFirst(ClaimTypes.NameIdentifier);
            if (user == null) return Unauthorized();

            var userId = int.Parse(user.Value);

            var transactions = await _transactionServices.GetTransactionsByDateRangeAsync(startDate, endDate, userId);

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Transactions");

            worksheet.Cell(1, 1).Value = "TransactionId";
            worksheet.Cell(1, 2).Value = "UserId";
            worksheet.Cell(1, 3).Value = "StockId";
            worksheet.Cell(1, 4).Value = "StockSymbol";
            worksheet.Cell(1, 5).Value = "StockName";
            worksheet.Cell(1, 6).Value = "StockPrice";
            worksheet.Cell(1, 7).Value = "TransactionDate";
            worksheet.Cell(1, 8).Value = "TransactionType";
            worksheet.Cell(1, 9).Value = "TransactionQuantity";
            worksheet.Cell(1, 10).Value = "TransactionPrice";

            var row = 2;

            foreach (var transaction in transactions)
            {
                worksheet.Cell(row, 1).Value = transaction.TransactionId;
                worksheet.Cell(row, 2).Value = transaction.User.UserId;
                worksheet.Cell(row, 3).Value = transaction.Stock!.StockId;
                worksheet.Cell(row, 4).Value = transaction.Stock.StockSymbol;
                worksheet.Cell(row, 5).Value = transaction.Stock.StockName;
                worksheet.Cell(row, 6).Value = transaction.Stock.Price;
                worksheet.Cell(row, 7).Value = transaction.TransactionDate;
                worksheet.Cell(row, 8).Value = transaction.TransactionType;
                worksheet.Cell(row, 9).Value = transaction.Quantity;
                worksheet.Cell(row, 10).Value = transaction.PricePerUnit;

                row++;
            }

            worksheet.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var content = stream.ToArray();
            
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "transactions.xlsx");
        }
    }
}