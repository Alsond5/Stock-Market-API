using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarket.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public string TransactionType { get; set; } = string.Empty;
        public int Quantity { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal PricePerUnit { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Commission { get; set; }
        public DateTime TransactionDate { get; set; } = DateTime.Now;

        public int UserId { get; set; }
        public User? User { get; set; }

        public int StockId { get; set; }
        public Stock? Stock { get; set; }
    }
}