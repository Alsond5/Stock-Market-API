using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockMarket.Models;

namespace StockMarket.Data.Repositories
{
    public class CouponRepository : ICouponRepository
    {
        private readonly StockMarketDBContext _context;

        public CouponRepository(StockMarketDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Coupon>> GetAllCouponsAsync()
        {
            return await Task.FromResult(_context.Coupons);
        }

        public async Task<Coupon?> GetCouponByIdAsync(int couponId)
        {
            return await Task.FromResult(_context.Coupons.FirstOrDefault(coupon => coupon.CouponId == couponId));
        }

        public async Task<Coupon?> GetCouponByCodeAsync(string code)
        {
            return await Task.FromResult(_context.Coupons.FirstOrDefault(coupon => coupon.Code == code));
        }

        public async Task<Coupon?> CreateCouponAsync(Coupon coupon)
        {
            await _context.Coupons.AddAsync(coupon);
            await _context.SaveChangesAsync();

            return coupon;
        }

        public async Task<Coupon?> DeactivateCouponAsync(int couponId)
        {
            var coupon = await GetCouponByIdAsync(couponId);

            if (coupon == null) return null;

            coupon.IsReedemed = false;

            await _context.SaveChangesAsync();

            return coupon;
        }

        public async Task<IEnumerable<Coupon>> DeactivateAllCouponsAsync()
        {
            var coupons = _context.Coupons;

            foreach (var coupon in coupons)
            {
                coupon.IsReedemed = false;
            }

            await _context.SaveChangesAsync();

            return coupons;
        }

        public async Task<Coupon?> UpdateCouponAsync(int couponId, Coupon coupon)
        {
            var couponToUpdate = await GetCouponByIdAsync(couponId);

            if (couponToUpdate == null) return null;

            couponToUpdate.Code = coupon.Code;
            couponToUpdate.Amount = coupon.Amount;
            couponToUpdate.ExpiryDate = coupon.ExpiryDate;
            couponToUpdate.IsReedemed = coupon.IsReedemed;

            await _context.SaveChangesAsync();

            return couponToUpdate;
        }

        public async Task<bool> DeleteCouponAsync(int couponId)
        {
            var coupon = await GetCouponByIdAsync(couponId);

            if (coupon == null) return false;

            _context.Coupons.Remove(coupon);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}