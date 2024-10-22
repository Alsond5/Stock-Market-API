using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarket.Dtos.Alert
{
    public class AlertDTO
    {
        public int Id { get; set; }
        public int StockId { get; set; }
        public string StockSymbol { get; set; } = string.Empty;
        public string StockName { get; set; } = string.Empty;
        public decimal LowerLimit { get; set; }
        public decimal UpperLimit { get; set; }
    }
}