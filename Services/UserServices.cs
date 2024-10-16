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
    public class UserServices : IUserServices
    {
        private readonly IUserRepository _userRepository;
        private readonly IBalanceRepository _balanceRepository;
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly IHashServices _hashServices;
        private readonly IConfiguration _configuration;

        public UserServices(
            IUserRepository userRepository,
            IBalanceRepository balanceRepository,
            IPortfolioRepository portfolioRepository,
            IHashServices hashServices,
            IConfiguration configuration
        ) {
            _userRepository = userRepository;
            _balanceRepository = balanceRepository;
            _portfolioRepository = portfolioRepository;
            _hashServices = hashServices;
            _configuration = configuration;
        }

        public async Task<UserDTO?> CreateUserAsync(CreateUserRequestDto user) {
            var _user = await _userRepository.GetUserIfExistingAsync(user.Username, user.Email);

            if (_user != null) return null;

            user.Password = _hashServices.ComputeSha256Hash(user.Password);
            
            var newUser = new User {
                Username = user.Username,
                Email = user.Email,
                Password = user.Password,
                CreatedAt = DateTime.Now,
                RoleId = 1
            };

            var createdUser = await _userRepository.AddUserAsync(newUser);

            if (createdUser == null) return null;

            var newBalance = new Balance {
                UserId = createdUser.UserId,
                Amount = 0
            };

            await _balanceRepository.AddBalanceAsync(newBalance);

            var newPortfolio = new Portfolio {
                UserId = createdUser.UserId,
                CreatedAt = DateTime.Now
            };

            await _portfolioRepository.AddPortfolioAsync(newPortfolio);

            return new UserDTO {
                UserId = createdUser.UserId,
                Username = createdUser.Username,
                Email = createdUser.Email,
                CreatedAt = createdUser.CreatedAt,
                RoleId = createdUser.RoleId
            };
        }

        public async Task<UserDTO?> GetUserByIdAsync(int id) {
            var user = await _userRepository.GetUserByIdAsync(id);

            if (user == null) return null;

            return new UserDTO {
                UserId = user.UserId,
                Username = user.Username,
                Email = user.Email,
                CreatedAt = user.CreatedAt,
                RoleId = user.RoleId
            };
        }
        
        public async Task<JwtSecurityToken?> LoginAsync(LoginRequestDTO loginRequest) {
            var _user = await _userRepository.GetUserByUsernameOrEmailAsync(loginRequest.UsernameOrEmail);
            if (_user == null) return null;

            if (_hashServices.VerifyPassword(_user.Password, loginRequest.Password) == false) {
                return null;
            }

            var claims = new[] {
                new Claim(ClaimTypes.Name, loginRequest.UsernameOrEmail)
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
            
            return token;
        }
        
        public async Task<UserDTO> GetUserByUsernameOrEmailAsync(string usernameOrEmail) {
            var user = await _userRepository.GetUserByUsernameOrEmailAsync(usernameOrEmail);

            if (user == null) return new UserDTO();

            return new UserDTO {
                UserId = user.UserId,
                Username = user.Username,
                Email = user.Email,
                CreatedAt = user.CreatedAt,
                RoleId = user.RoleId
            };
        }

        public async Task<UserDTO?> UpdateUserAsync(int id) {
            return null;
        }
    }
}