using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockMarket.Data;
using StockMarket.Services;

namespace StockMarket.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;

        public UserController(IUserServices userServices) {
            _userServices = userServices;
        }

        [Authorize]
        [HttpGet("@me")]
        public async Task<IActionResult> GetCurrentUser() {
            if (User.Identity?.Name == null) return BadRequest("400");

            var user = await _userServices.GetUserByUsernameOrEmailAsync(User.Identity.Name);

            return Ok(user);
        }

        [Authorize]
        [HttpGet("@me/balance")]
        public async Task<IActionResult> GetCurrentUserBalance() {
            if (User.Identity?.Name == null) return BadRequest("400");

            var balance = await _userServices.GetBalanceAsync(User);

            return Ok(balance);
        }
    }
}