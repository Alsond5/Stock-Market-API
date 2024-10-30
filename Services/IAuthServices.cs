using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockMarket.Dtos.User;

namespace StockMarket.Services
{
    public interface IAuthServices
    {
        Task<LoginResponseDTO> LoginAsync(LoginRequestDTO loginRequest);
        Task<UserDTO?> RegisterAsync(CreateUserRequestDTO createUser);
        Task<string> RefreshTokenAsync(string token);
    }
}