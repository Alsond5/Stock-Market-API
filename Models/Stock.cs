using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarket.Models
{
    public class Stock
    {
        public int StockId { get; set; }
        public string StockSymbol { get; set; } = string.Empty;
        public string StockName { get; set; } = string.Empty;
        public int Quantity { get; set; } = 10000;
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public DateTime LastUpdated { get; set; } = DateTime.Now;

        public ICollection<Transaction> Transactions { get; set; } = new HashSet<Transaction>();

        public ICollection<Holding> Holdings { get; set; } = new HashSet<Holding>();
    
        public ICollection<Alert> Alerts { get; set; } = new List<Alert>();
    
        public ICollection<History> Histories { get; set; } = new List<History>();
    }
}