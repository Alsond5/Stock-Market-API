using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StockMarket.Models
{
    public class Balance
    {
        public int BalanceId { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; } = 0;
        
        public int UserId { get; set; }
        [JsonIgnore]
        public User? User { get; set; }
    }
}