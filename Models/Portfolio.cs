using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarket.Models
{
    public class Portfolio
    {
        public int PortfolioId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        public int UserId { get; set; }
        public User? User { get; set; }

        public ICollection<Holding> Holdings { get; set; } = new HashSet<Holding>();
    }
}