using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarket.Dtos.User
{
    public class UserDetailsDTO
    {
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public int RoleId { get; set; }
        public int? Balance { get; set; }
        public int? PortfolioId { get; set; }
        public int? TotalStocks { get; set; }
        public int? TotalStockQuantity { get; set; }
    }
}