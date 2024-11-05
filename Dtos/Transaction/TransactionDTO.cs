using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockMarket.Dtos.Stock;
using StockMarket.Dtos.User;

namespace StockMarket.Dtos.Transaction
{
    public class TransactionDTO
    {
        public int TransactionId { get; set; }
        public string TransactionType { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal PricePerUnit { get; set; }
        public decimal Commission { get; set; }
        public DateTime TransactionDate { get; set; } = DateTime.Now;

        public int UserId { get; set; }
        public StockDTO Stock { get; set; } = new StockDTO();
    }
}