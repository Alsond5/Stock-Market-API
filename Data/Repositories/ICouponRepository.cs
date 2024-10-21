using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockMarket.Models;

namespace StockMarket.Data.Repositories
{
    public interface ICouponRepository
    {
        Task<IEnumerable<Coupon>> GetAllCouponsAsync();
        Task<Coupon?> GetCouponByIdAsync(int couponId);
        Task<Coupon?> GetCouponByCodeAsync(string code);
        Task<Coupon?> CreateCouponAsync(Coupon coupon);
        Task<Coupon?> DeactivateCouponAsync(int couponId);
        Task<IEnumerable<Coupon>> DeactivateAllCouponsAsync();
        Task<Coupon?> UpdateCouponAsync(int couponId, Coupon coupon);
        Task<bool> DeleteCouponAsync(int couponId);
    }
}