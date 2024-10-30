using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockMarket.Dtos.Stock;

namespace StockMarket.Dtos.Holding
{
    public class HoldingDTO
    {
        public int HoldingId { get; set; }
        public int Quantity { get; set; }
        public int PortfolioId { get; set; }
        public StockDTO Stock { get; set; } = new StockDTO();
    }
}