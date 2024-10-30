using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockMarket.Data.Repositories;
using StockMarket.Dtos.Transaction;
using StockMarket.Models;

namespace StockMarket.Services
{
    public class TransactionServices(ITransactionRepository transactionRepository) : ITransactionServices
    {
        private readonly ITransactionRepository _transactionRepository = transactionRepository;

        public async Task<IEnumerable<TransactionDTO>> GetAllTransactionsAsync()
        {
            var transactions = await _transactionRepository.GetAllTransactionsAsync();

            return transactions.Select(transaction => new TransactionDTO{
                TransactionId = transaction.TransactionId,
                TransactionType = transaction.TransactionType,
                Quantity = transaction.Quantity,
                PricePerUnit = transaction.PricePerUnit,
                Commission = transaction.Commission,
                TransactionDate = transaction.TransactionDate,
                User = new Dtos.User.UserDTO{
                    UserId = transaction.User!.UserId,
                    Username = transaction.User!.Username,
                    Email = transaction.User!.Email,
                    Balance = transaction.User!.Balance.Amount,
                    CreatedAt = transaction.User!.CreatedAt,
                    RoleId = transaction.User!.RoleId,
                },
                Stock = new Dtos.Stock.StockDTO{
                    StockId = transaction.Stock!.StockId,
                    StockSymbol = transaction.Stock!.StockSymbol,
                    StockName = transaction.Stock!.StockName,
                    StockQuantity = transaction.Stock!.Quantity,
                    Price = transaction.Stock!.Price,
                },
            });
        }

        public async Task<TransactionDTO?> GetTransactionByIdAsync(int transactionId)
        {
            var transaction = await _transactionRepository.GetTransactionByIdAsync(transactionId);

            return transaction == null ? null : new TransactionDTO{
                TransactionId = transaction.TransactionId,
                TransactionType = transaction.TransactionType,
                Quantity = transaction.Quantity,
                PricePerUnit = transaction.PricePerUnit,
                Commission = transaction.Commission,
                TransactionDate = transaction.TransactionDate,
                User = new Dtos.User.UserDTO{
                    UserId = transaction.User!.UserId,
                    Username = transaction.User!.Username,
                    Email = transaction.User!.Email,
                    Balance = transaction.User!.Balance.Amount,
                    CreatedAt = transaction.User!.CreatedAt,
                    RoleId = transaction.User!.RoleId,
                },
                Stock = new Dtos.Stock.StockDTO{
                    StockId = transaction.Stock!.StockId,
                    StockSymbol = transaction.Stock!.StockSymbol,
                    StockName = transaction.Stock!.StockName,
                    StockQuantity = transaction.Stock!.Quantity,
                    Price = transaction.Stock!.Price,
                },
            };
        }

        public async Task<IEnumerable<TransactionDTO>> GetTransactionsByUserIdAsync(int userId)
        {
            var transactions = await _transactionRepository.GetTransactionsByUserIdAsync(userId);

            return transactions.Select(transaction => new TransactionDTO{
                TransactionId = transaction.TransactionId,
                TransactionType = transaction.TransactionType,
                Quantity = transaction.Quantity,
                PricePerUnit = transaction.PricePerUnit,
                Commission = transaction.Commission,
                TransactionDate = transaction.TransactionDate,
                User = new Dtos.User.UserDTO{
                    UserId = transaction.User!.UserId,
                    Username = transaction.User!.Username,
                    Email = transaction.User!.Email,
                    Balance = transaction.User!.Balance.Amount,
                    CreatedAt = transaction.User!.CreatedAt,
                    RoleId = transaction.User!.RoleId,
                },
                Stock = new Dtos.Stock.StockDTO{
                    StockId = transaction.Stock!.StockId,
                    StockSymbol = transaction.Stock!.StockSymbol,
                    StockName = transaction.Stock!.StockName,
                    StockQuantity = transaction.Stock!.Quantity,
                    Price = transaction.Stock!.Price,
                },
            });
        }

        public async Task<IEnumerable<TransactionDTO>> GetTransactionsByStockIdAsync(int stockId)
        {
            var transactions = await _transactionRepository.GetTransactionsByStockIdAsync(stockId);

            return transactions.Select(transaction => new TransactionDTO{
                TransactionId = transaction.TransactionId,
                TransactionType = transaction.TransactionType,
                Quantity = transaction.Quantity,
                PricePerUnit = transaction.PricePerUnit,
                Commission = transaction.Commission,
                TransactionDate = transaction.TransactionDate,
                User = new Dtos.User.UserDTO{
                    UserId = transaction.User!.UserId,
                    Username = transaction.User!.Username,
                    Email = transaction.User!.Email,
                    Balance = transaction.User!.Balance.Amount,
                    CreatedAt = transaction.User!.CreatedAt,
                    RoleId = transaction.User!.RoleId,
                },
                Stock = new Dtos.Stock.StockDTO{
                    StockId = transaction.Stock!.StockId,
                    StockSymbol = transaction.Stock!.StockSymbol,
                    StockName = transaction.Stock!.StockName,
                    StockQuantity = transaction.Stock!.Quantity,
                    Price = transaction.Stock!.Price,
                },
            });
        }

        public async Task<IEnumerable<TransactionDTO>> GetTransactionsByPortfolioIdAsync(int portfolioId)
        {
            var transactions = await _transactionRepository.GetTransactionsByPortfolioIdAsync(portfolioId);

            return transactions.Select(transaction => new TransactionDTO{
                TransactionId = transaction.TransactionId,
                TransactionType = transaction.TransactionType,
                Quantity = transaction.Quantity,
                PricePerUnit = transaction.PricePerUnit,
                Commission = transaction.Commission,
                TransactionDate = transaction.TransactionDate,
                User = new Dtos.User.UserDTO{
                    UserId = transaction.User!.UserId,
                    Username = transaction.User!.Username,
                    Email = transaction.User!.Email,
                    Balance = transaction.User!.Balance.Amount,
                    CreatedAt = transaction.User!.CreatedAt,
                    RoleId = transaction.User!.RoleId,
                },
                Stock = new Dtos.Stock.StockDTO{
                    StockId = transaction.Stock!.StockId,
                    StockSymbol = transaction.Stock!.StockSymbol,
                    StockName = transaction.Stock!.StockName,
                    StockQuantity = transaction.Stock!.Quantity,
                    Price = transaction.Stock!.Price,
                },
            });
        }

        public async Task<IEnumerable<TransactionDTO>> GetTransactionsByUserIdAndStockIdAsync(int userId, int stockId)
        {
            var transactions = await _transactionRepository.GetTransactionsByUserIdAndStockIdAsync(userId, stockId);

            return transactions.Select(transaction => new TransactionDTO{
                TransactionId = transaction.TransactionId,
                TransactionType = transaction.TransactionType,
                Quantity = transaction.Quantity,
                PricePerUnit = transaction.PricePerUnit,
                Commission = transaction.Commission,
                TransactionDate = transaction.TransactionDate,
                User = new Dtos.User.UserDTO{
                    UserId = transaction.User!.UserId,
                    Username = transaction.User!.Username,
                    Email = transaction.User!.Email,
                    Balance = transaction.User!.Balance.Amount,
                    CreatedAt = transaction.User!.CreatedAt,
                    RoleId = transaction.User!.RoleId,
                },
                Stock = new Dtos.Stock.StockDTO{
                    StockId = transaction.Stock!.StockId,
                    StockSymbol = transaction.Stock!.StockSymbol,
                    StockName = transaction.Stock!.StockName,
                    StockQuantity = transaction.Stock!.Quantity,
                    Price = transaction.Stock!.Price,
                },
            });
        }

        public async Task<IEnumerable<TransactionDTO>> GetTransactionsByDateRangeForStockIdAsync(DateTime startDate, DateTime endDate, int stockId)
        {
            var transactions = await _transactionRepository.GetTransactionsByDateRangeForStockIdAsync(startDate, endDate, stockId);

            return transactions.Select(transaction => new TransactionDTO{
                TransactionId = transaction.TransactionId,
                TransactionType = transaction.TransactionType,
                Quantity = transaction.Quantity,
                PricePerUnit = transaction.PricePerUnit,
                Commission = transaction.Commission,
                TransactionDate = transaction.TransactionDate,
                User = new Dtos.User.UserDTO{
                    UserId = transaction.User!.UserId,
                    Username = transaction.User!.Username,
                    Email = transaction.User!.Email,
                    Balance = transaction.User!.Balance.Amount,
                    CreatedAt = transaction.User!.CreatedAt,
                    RoleId = transaction.User!.RoleId,
                },
                Stock = new Dtos.Stock.StockDTO{
                    StockId = transaction.Stock!.StockId,
                    StockSymbol = transaction.Stock!.StockSymbol,
                    StockName = transaction.Stock!.StockName,
                    StockQuantity = transaction.Stock!.Quantity,
                    Price = transaction.Stock!.Price,
                },
            });
        }

        public async Task<IEnumerable<TransactionDTO>> GetTransactionsByDateRangeAsync(DateTime startDate, DateTime endDate, int userId)
        {
            var transactions = await _transactionRepository.GetTransactionsByDateRangeAsync(startDate, endDate, userId);

            return transactions.Select(transaction => new TransactionDTO{
                TransactionId = transaction.TransactionId,
                TransactionType = transaction.TransactionType,
                Quantity = transaction.Quantity,
                PricePerUnit = transaction.PricePerUnit,
                Commission = transaction.Commission,
                TransactionDate = transaction.TransactionDate,
                User = new Dtos.User.UserDTO{
                    UserId = transaction.User!.UserId,
                    Username = transaction.User!.Username,
                    Email = transaction.User!.Email,
                    Balance = transaction.User!.Balance.Amount,
                    CreatedAt = transaction.User!.CreatedAt,
                    RoleId = transaction.User!.RoleId,
                },
                Stock = new Dtos.Stock.StockDTO{
                    StockId = transaction.Stock!.StockId,
                    StockSymbol = transaction.Stock!.StockSymbol,
                    StockName = transaction.Stock!.StockName,
                    StockQuantity = transaction.Stock!.Quantity,
                    Price = transaction.Stock!.Price,
                },
            });
        }

        public async Task<IEnumerable<TransactionDTO>> GetTransactionsByDateRangeAndStockAsync(DateTime startDate, DateTime endDate, int stockId, int userId)
        {
            var transactions = await _transactionRepository.GetTransactionsByDateRangeAndStockAsync(startDate, endDate, stockId, userId);

            return transactions.Select(transaction => new TransactionDTO{
                TransactionId = transaction.TransactionId,
                TransactionType = transaction.TransactionType,
                Quantity = transaction.Quantity,
                PricePerUnit = transaction.PricePerUnit,
                Commission = transaction.Commission,
                TransactionDate = transaction.TransactionDate,
                User = new Dtos.User.UserDTO{
                    UserId = transaction.User!.UserId,
                    Username = transaction.User!.Username,
                    Email = transaction.User!.Email,
                    Balance = transaction.User!.Balance.Amount,
                    CreatedAt = transaction.User!.CreatedAt,
                    RoleId = transaction.User!.RoleId,
                },
                Stock = new Dtos.Stock.StockDTO{
                    StockId = transaction.Stock!.StockId,
                    StockSymbol = transaction.Stock!.StockSymbol,
                    StockName = transaction.Stock!.StockName,
                    StockQuantity = transaction.Stock!.Quantity,
                    Price = transaction.Stock!.Price,
                },
            });
        }

        public async Task<TransactionDTO?> CreateTransactionAsync(Transaction transaction)
        {
            var newTransaction = await _transactionRepository.AddTransactionAsync(transaction);

            return newTransaction == null ? null : new TransactionDTO{
                TransactionId = newTransaction.TransactionId,
                TransactionType = newTransaction.TransactionType,
                Quantity = newTransaction.Quantity,
                PricePerUnit = newTransaction.PricePerUnit,
                Commission = newTransaction.Commission,
                TransactionDate = newTransaction.TransactionDate,
                User = new Dtos.User.UserDTO{
                    UserId = transaction.User!.UserId,
                    Username = transaction.User!.Username,
                    Email = transaction.User!.Email,
                    Balance = transaction.User!.Balance.Amount,
                    CreatedAt = transaction.User!.CreatedAt,
                    RoleId = transaction.User!.RoleId,
                },
                Stock = new Dtos.Stock.StockDTO{
                    StockId = transaction.Stock!.StockId,
                    StockSymbol = transaction.Stock!.StockSymbol,
                    StockName = transaction.Stock!.StockName,
                    StockQuantity = transaction.Stock!.Quantity,
                    Price = transaction.Stock!.Price,
                },
            };
        }

        public async Task<TransactionDTO?> UpdateTransactionAsync(Transaction transaction)
        {
            var updatedTransaction = await _transactionRepository.UpdateTransactionAsync(transaction);

            return updatedTransaction == null ? null : new TransactionDTO{
                TransactionId = updatedTransaction.TransactionId,
                TransactionType = updatedTransaction.TransactionType,
                Quantity = updatedTransaction.Quantity,
                PricePerUnit = updatedTransaction.PricePerUnit,
                Commission = updatedTransaction.Commission,
                TransactionDate = updatedTransaction.TransactionDate,
                User = new Dtos.User.UserDTO{
                    UserId = transaction.User!.UserId,
                    Username = transaction.User!.Username,
                    Email = transaction.User!.Email,
                    Balance = transaction.User!.Balance.Amount,
                    CreatedAt = transaction.User!.CreatedAt,
                    RoleId = transaction.User!.RoleId,
                },
                Stock = new Dtos.Stock.StockDTO{
                    StockId = transaction.Stock!.StockId,
                    StockSymbol = transaction.Stock!.StockSymbol,
                    StockName = transaction.Stock!.StockName,
                    StockQuantity = transaction.Stock!.Quantity,
                    Price = transaction.Stock!.Price,
                },
            };
        }
    }
}