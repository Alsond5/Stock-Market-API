using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarket.Models
{
    public class Holding
    {
        public int Id { get; set; }
        public int Quantity { get; set; }

        public int PortfolioId { get; set; }
        public Portfolio? Portfolio { get; set; }

        public int StockId { get; set; }
        public Stock? Stock { get; set; }
    }
}