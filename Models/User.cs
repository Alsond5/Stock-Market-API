using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarket.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public int RoleId { get; set; } = 1;
        public Role? Role { get; set; }

        public Balance? Balance { get; set; }

        public Portfolio? Portfolio { get; set; }

        public ICollection<Alert> Alerts { get; set; } = new List<Alert>();
    }
}