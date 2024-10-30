using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarket.Dtos.User
{
    public class LoginResponseDTO
    {
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime Expires { get; set; }
    }
}