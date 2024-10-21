using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarket.Dtos.Coupon
{
    public class CreateCouponRequestDTO
    {
        public string? Code { get; set; }
        public decimal Amount { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }
}