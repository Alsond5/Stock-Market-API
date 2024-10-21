using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarket.Dtos.User
{
    public class UpdateUserRequestDTO
    {
        public string? Username { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string? Password { get; set; } = string.Empty;
        public int? RoleId { get; set; }
        public decimal? Balance { get; set; }
    }
}