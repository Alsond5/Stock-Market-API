using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarket.Models
{
    public class History
    {
        public int HistoryId { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public int Price { get; set; }

        public int StockId { get; set; }
        public Stock? Stock { get; set; }
    }
}