using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockMarket.Attributes;
using StockMarket.Dtos.User;
using StockMarket.Services;

namespace StockMarket.Controllers
{
    [Route("api/admin")]
    [ApiController]
    [Authorize]
    public class AdminController : ControllerBase
    {
        private readonly IUserServices _userServices;

        public AdminController(IUserServices userServices) {
            _userServices = userServices;
        }

        [HttpGet("users")]
        [RoleAuthorize(2)]
        public async Task<IActionResult> GetUsers() {
            var users = await _userServices.GetUsersAsync();

            return Ok(users);
        }

        [HttpGet("user/{id}")]
        [RoleAuthorize(2)]
        public async Task<IActionResult> GetUserById(int id) {
            var user = await _userServices.GetUserDetailsByUserIdAsync(id);

            if (user == null) return NotFound("404");

            return Ok(user);
        }

        [HttpPut("user/{id}")]
        [RoleAuthorize(2)]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserRequestDTO update) {
            var user = await _userServices.AdminUpdateUserAsync(id, update);

            if (user == null) return NotFound("404");

            return Ok(user);
        }

        [HttpPost("user/create")]
        [RoleAuthorize(2)]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequestDTO user) {
            var createdUser = await _userServices.CreateUserAsync(user, User);

            if (createdUser == null) return BadRequest("400");

            return Ok(createdUser);
        }
    }
}