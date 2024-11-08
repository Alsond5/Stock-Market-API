using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockMarket.Attributes;
using StockMarket.Data;
using StockMarket.Dtos.User;
using StockMarket.Services;

namespace StockMarket.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;

        public UserController(IUserServices userServices) {
            _userServices = userServices;
        }

        [HttpGet("all")]
        [RoleAuthorize(2)]
        public async Task<IActionResult> GetUsers() {
            var users = await _userServices.GetUsersAsync();

            return Ok(users);
        }

        [HttpGet("{id}")]
        [RoleAuthorize(2)]
        public async Task<IActionResult> GetUserById(int id) {
            var user = await _userServices.GetUserDetailsByUserIdAsync(id);
            if (user == null) return NotFound("404");

            return Ok(user);
        }

        [HttpPut("{id}")]
        [RoleAuthorize(2)]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserRequestDTO update) {
            Console.WriteLine(update.Username);
            var user = await _userServices.AdminUpdateUserAsync(id, update);
            if (user == null) return NotFound("404");

            return Ok(user);
        }

        [HttpPost("create")]
        [RoleAuthorize(2)]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequestDTO user) {
            var createdUser = await _userServices.CreateUserAsync(user);
            if (createdUser == null) return BadRequest("400");

            return Ok(createdUser);
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