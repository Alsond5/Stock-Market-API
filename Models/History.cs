using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarket.Models
{
    public class History
    {
        public int HistoryId { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public int StockId { get; set; }
        public Stock? Stock { get; set; }
    }
}