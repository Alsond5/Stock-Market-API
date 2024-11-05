using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarket.Dtos.User
{
    public class LoginResponseDTO
    {
        public required string Token { get; set; }
        public required string TokenType { get; set; }
        public DateTime Expiration { get; set; }
    }
}