using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarket.Models
{
    public class Coupon
    {
        public int CouponId { get; set; }
        public string Code { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        public DateTime ExpiryDate { get; set; } = DateTime.Now;
        public bool IsReedemed { get; set; }
    }
}