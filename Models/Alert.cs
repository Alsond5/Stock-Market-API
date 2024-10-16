using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarket.Models
{
    public class Alert
    {
        public int AlertId { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }

        public int StockId { get; set; }
        public Stock? Stock { get; set; }

        public int LowerLimit { get; set; }
        public int UpperLimit { get; set; }
    }
}