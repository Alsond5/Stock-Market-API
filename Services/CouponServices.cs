using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockMarket.Data.Repositories;
using StockMarket.Dtos.Coupon;
using StockMarket.Models;

namespace StockMarket.Services
{
    public class CouponServices : ICouponServices
    {
        private readonly ICouponRepository _couponRepository;
        private readonly IUserRepository _userRepository;

        public CouponServices(ICouponRepository couponRepository, IUserRepository userRepository)
        {
            _couponRepository = couponRepository;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<Coupon>> GetAllCouponsAsync()
        {
            var coupons = await _couponRepository.GetAllCouponsAsync();

            return coupons;
        }

        public async Task<bool> ReedemCouponAsync(string code, int userId)
        {
            var coupon = await _couponRepository.GetCouponByCodeAsync(code);
            var user = await _userRepository.GetUserByIdAsync(userId);

            if (coupon == null || user == null) return false;

            if (coupon.IsReedemed) return false;

            coupon.IsReedemed = true;
            user.Balance.Amount += coupon.Amount;

            await _couponRepository.UpdateCouponAsync(coupon.CouponId, coupon);
            await _userRepository.UpdateUserAsync(user);

            return true;
        }

        public async Task<Coupon> CreateCouponAsync(CreateCouponRequestDTO coupon)
        {
            if (coupon.Amount <= 0) return new Coupon();
            if (coupon.ExpiryDate < DateTime.Now) return new Coupon();

            var newCoupon = new Coupon{
                Code = coupon.Code ?? GenerateCouponCode(),
                Amount = coupon.Amount,
                ExpiryDate = coupon.ExpiryDate ?? DateTime.Now.AddDays(7),
            };

            var createdCoupon = await _couponRepository.CreateCouponAsync(newCoupon);

            return createdCoupon ?? new Coupon();
        }

        private static string GenerateCouponCode()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var code = new string(
                Enumerable.Repeat(chars, 10)
                    .Select(s => s[random.Next(s.Length)])
                    .ToArray()
            );

            return code;
        }
    }
}