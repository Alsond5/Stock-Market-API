using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StockMarket.Dtos.User;
using StockMarket.Services;

namespace StockMarket.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController(IUserServices userServices) : ControllerBase
    {
        private readonly IUserServices _userServices = userServices;

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserRequestDTO user) {
            var createdUser = await _userServices.CreateUserAsync(user, User);

            if (createdUser == null) return BadRequest("400");

            return Ok(createdUser);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequest) {
            var token = await _userServices.LoginAsync(loginRequest);

            if (token == null) return BadRequest("400");

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(tokenString);
        }
    }
}