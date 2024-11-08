using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockMarket.Attributes;
using StockMarket.Dtos.Coupon;
using StockMarket.Dtos.System;
using StockMarket.Dtos.User;
using StockMarket.Services;

namespace StockMarket.Controllers
{
    [Route("api/admin")]
    [ApiController]
    [Authorize]
    public class AdminController(IUserServices userServices, ISystemServices systemServices) : ControllerBase
    {
        private readonly IUserServices _userServices = userServices;
        private readonly ISystemServices _systemServices = systemServices;

        [HttpGet("system/commission")]
        [RoleAuthorize(2)]
        public async Task<IActionResult> GetCommission() {
            var commission = await _systemServices.GetConfigValueAsync("commission");

            return Ok(commission ?? "0");
        }

        [HttpPut("system/commission")]
        [RoleAuthorize(2)]
        public async Task<IActionResult> UpdateCommission([FromBody] UpdateCommissionRequestDTO commission) {
            var updatedCommission = await _systemServices.SetConfigValueAsync("commission", commission.Commission.ToString());
            if (updatedCommission == false) return BadRequest("400");

            return Ok(updatedCommission);
        }

        [HttpGet("system/balance")]
        [RoleAuthorize(2)]
        public async Task<IActionResult> GetSystemBalance() {
            var balance = await _systemServices.GetConfigValueAsync("systemBalance");

            return Ok(balance ?? "0");
        }
    }
}