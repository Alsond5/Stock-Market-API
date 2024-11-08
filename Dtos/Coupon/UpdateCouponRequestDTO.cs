using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarket.Dtos.Coupon
{
    public class UpdateCouponRequestDTO
    {
        public string? Code { get; set; }
        public decimal Amount { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public bool IsReedemed { get; set; }
    }
}