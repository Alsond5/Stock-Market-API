using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockMarket.Attributes;
using StockMarket.Dtos.Coupon;
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

        [HttpPost("create")]
        [RoleAuthorize(2)]
        public async Task<IActionResult> CreateCoupon([FromBody] CreateCouponRequestDTO coupon)
        {
            var createdCoupon = await _couponServices.CreateCouponAsync(coupon);

            if (createdCoupon == null) return BadRequest("400");

            return Ok(createdCoupon);
        }

        [HttpPut("reedem")]
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