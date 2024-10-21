using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using StockMarket.Data.Repositories;
using StockMarket.Dtos.Balance;
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
            IStockRepository stockRepository,
            IHashServices hashServices,
            IConfiguration configuration
        ) {
            _userRepository = userRepository;
            _balanceRepository = balanceRepository;
            _portfolioRepository = portfolioRepository;
            _hashServices = hashServices;
            _configuration = configuration;
        }

        public async Task<IEnumerable<UserDTO>> GetUsersAsync() {
            var users = await _userRepository.GetAllUsersAsync();

            return users.Select(user => new UserDTO {
                UserId = user.UserId,
                Username = user.Username,
                Email = user.Email,
                CreatedAt = user.CreatedAt,
                RoleId = user.RoleId
            });
        }

        public async Task<UserDTO?> CreateUserAsync(CreateUserRequestDTO user, ClaimsPrincipal claims) {
            var _user = await _userRepository.GetUserIfExistingAsync(user.Username, user.Email);

            if (_user != null) return null;

            user.Password = _hashServices.ComputeSha256Hash(user.Password);
            
            var newUser = new User {
                Username = user.Username,
                Email = user.Email,
                Password = user.Password,
                CreatedAt = DateTime.Now,
                RoleId = claims.IsInRole("2") ? user.RoleId ?? 1 : 1,
                Balance = claims.IsInRole("2") ? new Balance { Amount = user.Balance ?? 0 } : new Balance { Amount = 0 }
            };

            var createdUser = await _userRepository.AddUserAsync(newUser);

            if (createdUser == null) return null;

            return new UserDTO {
                UserId = createdUser.UserId,
                Username = createdUser.Username,
                Email = createdUser.Email,
                CreatedAt = createdUser.CreatedAt,
                Balance = createdUser.Balance.Amount,
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
                Balance = user.Balance.Amount,
                RoleId = user.RoleId
            };
        }

        public async Task<UserDetailsDTO?> GetUserDetailsByUserIdAsync(int userId) {
            var user = await _userRepository.GetUserByIdAsync(userId);

            if (user == null) return null;
            
            var balance = await _balanceRepository.GetBalanceByUserIdAsync(userId);
            var portfolio = await _portfolioRepository.GetPortfolioByUserIdAsync(userId);

            return new UserDetailsDTO {
                UserId = user.UserId,
                Username = user.Username,
                Email = user.Email,
                CreatedAt = user.CreatedAt,
                RoleId = user.RoleId,
                Balance = balance?.Amount,
                PortfolioId = portfolio?.PortfolioId,
                TotalStocks = portfolio?.TotalStocks,
                TotalStockQuantity = portfolio?.TotalStockQuantity
            };
        }
        
        public async Task<JwtSecurityToken?> LoginAsync(LoginRequestDTO loginRequest) {
            var _user = await _userRepository.GetUserByUsernameOrEmailAsync(loginRequest.UsernameOrEmail);
            if (_user == null) return null;

            if (_hashServices.VerifyPassword(_user.Password, loginRequest.Password) == false) {
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
                Balance = user.Balance.Amount,
                RoleId = user.RoleId
            };
        }

        public async Task<BalanceDTO?> GetBalanceAsync(ClaimsPrincipal claims) {
            var userId = int.Parse(claims.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
            
            if (userId == 0) return null;

            return await GetBalanceAsync(userId);
        }

        public async Task<BalanceDTO?> GetBalanceAsync(int userId) {
            var balance = await _balanceRepository.GetBalanceByUserIdAsync(userId);

            if (balance == null) return null;

            return new BalanceDTO {
                BalanceId = balance.BalanceId,
                Amount = balance.Amount
            };
        }

        public async Task<UserDTO?> UpdateUserAsync(int id, UpdateUserRequestDTO update) {
            var user = await _userRepository.GetUserByIdAsync(id);

            if (user == null) return null;
            
            if (update.Password != null) {
                update.Password = _hashServices.ComputeSha256Hash(update.Password);
                user.Password = update.Password;
            }
            
            if (update.Username != null && update.Email != null) {
                var checkExisting = await _userRepository.GetUserIfExistingAsync(update.Username, update.Email);
                
                if (checkExisting != null) return null;

                user.Username = update.Username;
                user.Email = update.Email;
            }
            else {
                if (update.Username != null) {
                    var checkExisting = await _userRepository.GetUserByUsernameAsync(update.Username);

                    if (checkExisting != null) return null;

                    user.Username = update.Username;
                }

                if (update.Email != null) {
                    var checkExisting = await _userRepository.GetUserByEmailAsync(update.Email);

                    if (checkExisting != null) return null;

                    user.Email = update.Email;
                }
            }

            var updatedUser = await _userRepository.UpdateUserAsync(user);

            if (updatedUser == null) return null;

            return new UserDTO {
                UserId = updatedUser.UserId,
                Username = updatedUser.Username,
                Email = updatedUser.Email,
                CreatedAt = updatedUser.CreatedAt,
                Balance = updatedUser.Balance.Amount,
                RoleId = updatedUser.RoleId
            };
        }

        public async Task<UserDTO?> AdminUpdateUserAsync(int id, UpdateUserRequestDTO update) {
            var user = await _userRepository.GetUserByIdAsync(id);

            if (user == null) return null;
            
            if (update.Password != null) {
                update.Password = _hashServices.ComputeSha256Hash(update.Password);
                user.Password = update.Password;
            }
            
            if (update.Username != null && update.Email != null) {
                var checkExisting = await _userRepository.GetUserIfExistingAsync(update.Username, update.Email);
                
                if (checkExisting != null) return null;

                user.Username = update.Username;
                user.Email = update.Email;
            }
            else {
                if (update.Username != null) {
                    var checkExisting = await _userRepository.GetUserByUsernameAsync(update.Username);

                    if (checkExisting != null) return null;

                    user.Username = update.Username;
                }

                if (update.Email != null) {
                    var checkExisting = await _userRepository.GetUserByEmailAsync(update.Email);

                    if (checkExisting != null) return null;

                    user.Email = update.Email;
                }
            }
            
            user.RoleId = update.RoleId ?? user.RoleId;
            user.Balance.Amount = update.Balance ?? user.Balance.Amount;

            var updatedUser = await _userRepository.UpdateUserAsync(user);

            if (updatedUser == null) return null;

            return new UserDTO {
                UserId = updatedUser.UserId,
                Username = updatedUser.Username,
                Email = updatedUser.Email,
                CreatedAt = updatedUser.CreatedAt,
                Balance = updatedUser.Balance.Amount,
                RoleId = updatedUser.RoleId
            };
        }
    }
}