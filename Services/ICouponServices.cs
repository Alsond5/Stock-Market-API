using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockMarket.Dtos.Coupon;
using StockMarket.Models;

namespace StockMarket.Services
{
    public interface ICouponServices
    {
        Task<IEnumerable<Coupon>> GetAllCouponsAsync();
        Task<Coupon> GetCouponByIdAsync(int couponId);
        Task<Coupon> CreateCouponAsync(CreateCouponRequestDTO coupon);
        Task<Coupon> UpdateCouponAsync(int couponId, UpdateCouponRequestDTO coupon);
        Task<bool> ReedemCouponAsync(string code, int userId);
        Task<bool> DeleteCouponAsync(int couponId);
    }
}