using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarket.Dtos.Holding
{
    public class HoldingDTO
    {
        public int Quantity { get; set; }
        public int UserId { get; set; }
        public decimal Balance { get; set; }
        public int PortfolioId { get; set; }
        public int StockId { get; set; }
    }
}