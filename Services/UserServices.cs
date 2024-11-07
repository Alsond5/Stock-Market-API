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
                Balance = user.Balance.Amount,
                CreatedAt = user.CreatedAt,
                RoleId = user.RoleId,
                RoleName = user.Role!.RoleName,
                PortfolioId = user.Portfolio!.PortfolioId,
                TotalStocks = user.Portfolio.TotalStocks,
                TotalStockQuantity = user.Portfolio.TotalStockQuantity
            });
        }

        public async Task<UserDTO?> CreateUserAsync(CreateUserRequestDTO user) {
            var _user = await _userRepository.GetUserIfExistingAsync(user.Username, user.Email);
            if (_user != null) return null;

            user.Password = _hashServices.ComputeSha256Hash(user.Password);
            
            var newUser = new User {
                Username = user.Username,
                Email = user.Email,
                Password = user.Password,
                CreatedAt = DateTime.Now,
                RoleId = user.RoleId ?? 1,
                Balance = new Balance { Amount = user.Balance ?? 0 }
            };

            var createdUser = await _userRepository.AddUserAsync(newUser);
            if (createdUser == null) return null;

            return new UserDTO {
                UserId = createdUser.UserId,
                Username = createdUser.Username,
                Email = createdUser.Email,
                CreatedAt = createdUser.CreatedAt,
                Balance = createdUser.Balance.Amount,
                RoleId = createdUser.RoleId,
                RoleName = createdUser.Role!.RoleName,
                PortfolioId = createdUser.Portfolio!.PortfolioId,
                TotalStocks = createdUser.Portfolio.TotalStocks,
                TotalStockQuantity = createdUser.Portfolio.TotalStockQuantity
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
                RoleId = user.RoleId,
                RoleName = user.Role!.RoleName,
                PortfolioId = user.Portfolio!.PortfolioId,
                TotalStocks = user.Portfolio.TotalStocks,
                TotalStockQuantity = user.Portfolio.TotalStockQuantity
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
        
        public async Task<UserDTO> GetUserByUsernameOrEmailAsync(string usernameOrEmail) {
            var user = await _userRepository.GetUserByUsernameOrEmailAsync(usernameOrEmail);

            if (user == null) return new UserDTO();

            return new UserDTO {
                UserId = user.UserId,
                Username = user.Username,
                Email = user.Email,
                CreatedAt = user.CreatedAt,
                Balance = user.Balance.Amount,
                RoleId = user.RoleId,
                RoleName = user.Role!.RoleName,
                PortfolioId = user.Portfolio!.PortfolioId,
                TotalStocks = user.Portfolio.TotalStocks,
                TotalStockQuantity = user.Portfolio.TotalStockQuantity
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
                Amount = balance.Amount,
                UserId = balance.UserId,
                Username = balance.User!.Username,
                Email = balance.User!.Email
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
                RoleId = updatedUser.RoleId,
                RoleName = updatedUser.Role!.RoleName,
                PortfolioId = updatedUser.Portfolio!.PortfolioId,
                TotalStocks = updatedUser.Portfolio.TotalStocks,
                TotalStockQuantity = updatedUser.Portfolio.TotalStockQuantity
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
                RoleId = updatedUser.RoleId,
                RoleName = updatedUser.Role!.RoleName,
                PortfolioId = updatedUser.Portfolio!.PortfolioId,
                TotalStocks = updatedUser.Portfolio.TotalStocks,
                TotalStockQuantity = updatedUser.Portfolio.TotalStockQuantity
            };
        }
    }
}