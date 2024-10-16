using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using StockMarket.Dtos.User;
using StockMarket.Models;

namespace StockMarket.Services
{
    public interface IUserServices
    {
        Task<UserDTO?> CreateUserAsync(CreateUserRequestDto user);
        Task<UserDTO?> GetUserByIdAsync(int userId);
        Task<JwtSecurityToken?> LoginAsync(LoginRequestDTO loginRequest);
        Task<UserDTO> GetUserByUsernameOrEmailAsync(string usernameOrEmail);
        Task<UserDTO?> UpdateUserAsync(int userId);
    }
}