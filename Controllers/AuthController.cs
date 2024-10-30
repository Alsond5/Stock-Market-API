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
    public class AuthController(IAuthServices authServices) : ControllerBase
    {
        private readonly IAuthServices _authServices = authServices;

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserRequestDTO user) {
            var createdUser = await _authServices.RegisterAsync(user);
            if (createdUser == null) return BadRequest("400");

            return Ok(createdUser);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequest) {
            var token = await _authServices.LoginAsync(loginRequest);
            if (token == null) return BadRequest("400");

            return Ok(token);
        }
    }
}