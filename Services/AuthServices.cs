using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using StockMarket.Data.Repositories;
using StockMarket.Dtos.User;
using StockMarket.Models;

namespace StockMarket.Services
{
    public class AuthServices(IUserRepository userRepository, IHashServices hashServices, IConfiguration configuration) : IAuthServices
    {

        private readonly IUserRepository _userRepository = userRepository;
        private readonly IHashServices _hashServices = hashServices;
        private readonly IConfiguration _configuration = configuration;

        public async Task<UserDTO?> RegisterAsync(CreateUserRequestDTO createUser)
        {
            var _user = await _userRepository.GetUserIfExistingAsync(createUser.Username, createUser.Email);
            if (_user != null) return null;

            createUser.Password = _hashServices.ComputeSha256Hash(createUser.Password);

            var newUser = new User
            {
                Username = createUser.Username,
                Email = createUser.Email,
                Password = createUser.Password,
                RoleId = 1
            };

            var user = await _userRepository.AddUserAsync(newUser);
            if (user == null) return null;

            return new UserDTO
            {
                UserId = user.UserId,
                Username = user.Username,
                Email = user.Email,
                CreatedAt = user.CreatedAt,
                RoleId = user.RoleId
            };
        }

        public async Task<LoginResponseDTO?> LoginAsync(LoginRequestDTO loginRequest) {
            var _user = await _userRepository.GetUserByUsernameOrEmailAsync(loginRequest.UsernameOrEmail);
            if (_user == null) return null;

            if (!_hashServices.VerifyPassword(_user.Password, loginRequest.Password)) {
                return null;
            }

            var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, _user.UserId.ToString()),
                new Claim(ClaimTypes.Name, loginRequest.UsernameOrEmail),
                new Claim(ClaimTypes.Role, _user.RoleId.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"] ?? "WMDPFnnHcJK/7jjwW36YB0mQWoOJzG1ugA/r2FNdCYo="));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            Console.WriteLine(key);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
            );
            
            return new LoginResponseDTO {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                TokenType = "Bearer",
                Expiration = token.ValidTo
            };
        }

        public async Task<string> RefreshTokenAsync(string token) {
            return token;
        }
    }
}