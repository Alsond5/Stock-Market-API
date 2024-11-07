using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockMarket.Dtos.User;

namespace StockMarket.Dtos.Balance
{
    public class BalanceDTO
    {
        public int BalanceId { get; set; }
        public int UserId { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public decimal Amount { get; set; } = 0;
        
    }
}