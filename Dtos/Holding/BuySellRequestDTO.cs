using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarket.Dtos.Holding
{
    public class BuySellRequestDTO
    {
        public int StockId { get; set; }
        public int Quantity { get; set; }
    }
}