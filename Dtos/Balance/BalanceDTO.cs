using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarket.Dtos.Balance
{
    public class BalanceDTO
    {
        public int BalanceId { get; set; }
        public decimal Amount { get; set; } = 0;
    }
}