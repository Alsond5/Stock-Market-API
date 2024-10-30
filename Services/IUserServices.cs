using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using StockMarket.Dtos.Balance;
using StockMarket.Dtos.User;
using StockMarket.Models;

namespace StockMarket.Services
{
    public interface IUserServices
    {
        Task<IEnumerable<UserDTO>> GetUsersAsync();
        Task<UserDTO?> GetUserByIdAsync(int userId);
        Task<UserDetailsDTO?> GetUserDetailsByUserIdAsync(int userId);
        Task<UserDTO?> CreateUserAsync(CreateUserRequestDTO user);
        Task<UserDTO> GetUserByUsernameOrEmailAsync(string usernameOrEmail);
        Task<BalanceDTO?> GetBalanceAsync(ClaimsPrincipal claims);
        Task<BalanceDTO?> GetBalanceAsync(int userId);
        Task<UserDTO?> UpdateUserAsync(int userId, UpdateUserRequestDTO update);
        Task<UserDTO?> AdminUpdateUserAsync(int userId, UpdateUserRequestDTO update);
    }
}