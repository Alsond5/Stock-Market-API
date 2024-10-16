using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarket.Models
{
    public class Balance
    {
        public int BalanceId { get; set; }
        public int Amount { get; set; }
        
        public int UserId { get; set; }
        public User? User { get; set; }
    }
}