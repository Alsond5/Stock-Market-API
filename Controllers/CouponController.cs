using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockMarket.Attributes;
using StockMarket.Dtos.Coupon;
using StockMarket.Models;
using StockMarket.Services;

namespace StockMarket.Controllers
{
    [Route("api/coupons")]
    [ApiController]
    public class CouponController(ICouponServices couponServices) : ControllerBase
    {
        private readonly ICouponServices _couponServices = couponServices;

        [HttpGet("all")]
        [RoleAuthorize(2)]
        public async Task<IActionResult> GetAllCoupons()
        {
            var coupons = await _couponServices.GetAllCouponsAsync();

            return Ok(coupons);
        }

        [HttpGet("{couponId}")]
        [RoleAuthorize(2)]
        public async Task<IActionResult> GetCoupon(int couponId)
        {
            var coupon = await _couponServices.GetCouponByIdAsync(couponId);
            if (coupon == null) return NotFound("404");

            return Ok(coupon);
        }

        [HttpPut("{couponId}")]
        [RoleAuthorize(2)]
        public async Task<IActionResult> UpdateCoupon(int couponId, [FromBody] UpdateCouponRequestDTO coupon)
        {
            Console.WriteLine(couponId);
            Console.WriteLine(coupon.Amount);
            var updatedCoupon = await _couponServices.UpdateCouponAsync(couponId, coupon);
            if (updatedCoupon == null) return NotFound("404");

            return Ok(updatedCoupon);
        }

        [HttpPost("create")]
        [RoleAuthorize(2)]
        public async Task<IActionResult> CreateCoupon([FromBody] CreateCouponRequestDTO coupon)
        {
            var createdCoupon = await _couponServices.CreateCouponAsync(coupon);

            if (createdCoupon == null) return BadRequest("400");

            return Ok(createdCoupon);
        }

        [HttpDelete("{couponId}")]
        [RoleAuthorize(2)]
        public async Task<IActionResult> DeleteCoupon(int couponId)
        {
            var isDeleted = await _couponServices.DeleteCouponAsync(couponId);
            if (isDeleted == false) return NotFound("404");

            return Ok(isDeleted);
        }

        [HttpPut("redeem")]
        [Authorize]
        public async Task<IActionResult> ReedemCoupon([FromBody] ReedemCouponRequestDTO reedemCoupon)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return BadRequest("400");

            var isReedemed = await _couponServices.ReedemCouponAsync(reedemCoupon.Code, int.Parse(userId));
            if (isReedemed == false) return Conflict("This coupon is already reedemed");

            return Ok(isReedemed);
        }
    }
}