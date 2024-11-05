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
            if (transactions == null) return [];

            return transactions.Select(transaction => new TransactionDTO{
                TransactionId = transaction.TransactionId,
                TransactionType = transaction.TransactionType,
                Quantity = transaction.Quantity,
                PricePerUnit = transaction.PricePerUnit,
                Commission = transaction.Commission,
                TransactionDate = transaction.TransactionDate,
                UserId = transaction.UserId,
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
                UserId = transaction.UserId,
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
            if (transactions == null) return [];

            return transactions.Select(transaction => new TransactionDTO{
                TransactionId = transaction.TransactionId,
                TransactionType = transaction.TransactionType,
                Quantity = transaction.Quantity,
                PricePerUnit = transaction.PricePerUnit,
                Commission = transaction.Commission,
                TransactionDate = transaction.TransactionDate,
                UserId = transaction.UserId,
                Stock = new Dtos.Stock.StockDTO{
                    StockId = transaction.Stock!.StockId,
                    StockSymbol = transaction.Stock!.StockSymbol,
                    StockName = transaction.Stock!.StockName,
                    StockQuantity = transaction.Stock!.Quantity,
                    Price = transaction.Stock!.Price,
                },
            });
        }

        public async Task<IEnumerable<TransactionDTO>> GetTransactionsByStockSymbolAsync(string stockSymbol)
        {
            var transactions = await _transactionRepository.GetTransactionsByStockSymbolAsync(stockSymbol);
            if (transactions == null) return [];

            return transactions.Select(transaction => new TransactionDTO{
                TransactionId = transaction.TransactionId,
                TransactionType = transaction.TransactionType,
                Quantity = transaction.Quantity,
                PricePerUnit = transaction.PricePerUnit,
                Commission = transaction.Commission,
                TransactionDate = transaction.TransactionDate,
                UserId = transaction.UserId,
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
            if (transactions == null) return [];

            return transactions.Select(transaction => new TransactionDTO{
                TransactionId = transaction.TransactionId,
                TransactionType = transaction.TransactionType,
                Quantity = transaction.Quantity,
                PricePerUnit = transaction.PricePerUnit,
                Commission = transaction.Commission,
                TransactionDate = transaction.TransactionDate,
                UserId = transaction.UserId,
                Stock = new Dtos.Stock.StockDTO{
                    StockId = transaction.Stock!.StockId,
                    StockSymbol = transaction.Stock!.StockSymbol,
                    StockName = transaction.Stock!.StockName,
                    StockQuantity = transaction.Stock!.Quantity,
                    Price = transaction.Stock!.Price,
                },
            });
        }

        public async Task<IEnumerable<TransactionDTO>> GetTransactionsByUserIdAndStockSymbolAsync(int userId, string stockSymbol)
        {
            var transactions = await _transactionRepository.GetTransactionsByUserIdAndStockSymbolAsync(userId, stockSymbol);
            foreach (var transaction in transactions)
            {
                Console.WriteLine(transaction);
            }
            if (transactions == null) return [];

            return transactions.Select(transaction => new TransactionDTO{
                TransactionId = transaction.TransactionId,
                TransactionType = transaction.TransactionType,
                Quantity = transaction.Quantity,
                PricePerUnit = transaction.PricePerUnit,
                Commission = transaction.Commission,
                TransactionDate = transaction.TransactionDate,
                UserId = transaction.UserId,
                Stock = new Dtos.Stock.StockDTO{
                    StockId = transaction.Stock!.StockId,
                    StockSymbol = transaction.Stock!.StockSymbol,
                    StockName = transaction.Stock!.StockName,
                    StockQuantity = transaction.Stock!.Quantity,
                    Price = transaction.Stock!.Price,
                },
            });
        }

        public async Task<IEnumerable<TransactionDTO>> GetTransactionsByDateRangeForStockSymbolAsync(DateTime startDate, DateTime endDate, string stockSymbol)
        {
            var transactions = await _transactionRepository.GetTransactionsByDateRangeForStockSymbolAsync(startDate, endDate, stockSymbol);
            if (transactions == null) return [];

            return transactions.Select(transaction => new TransactionDTO{
                TransactionId = transaction.TransactionId,
                TransactionType = transaction.TransactionType,
                Quantity = transaction.Quantity,
                PricePerUnit = transaction.PricePerUnit,
                Commission = transaction.Commission,
                TransactionDate = transaction.TransactionDate,
                UserId = transaction.UserId,
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
            if (transactions == null) return [];

            return transactions.Select(transaction => new TransactionDTO{
                TransactionId = transaction.TransactionId,
                TransactionType = transaction.TransactionType,
                Quantity = transaction.Quantity,
                PricePerUnit = transaction.PricePerUnit,
                Commission = transaction.Commission,
                TransactionDate = transaction.TransactionDate,
                UserId = transaction.UserId,
                Stock = new Dtos.Stock.StockDTO{
                    StockId = transaction.Stock!.StockId,
                    StockSymbol = transaction.Stock!.StockSymbol,
                    StockName = transaction.Stock!.StockName,
                    StockQuantity = transaction.Stock!.Quantity,
                    Price = transaction.Stock!.Price,
                },
            });
        }

        public async Task<IEnumerable<TransactionDTO>> GetTransactionsByDateRangeAndStockAsync(DateTime startDate, DateTime endDate, string stockSymbol, int userId)
        {
            var transactions = await _transactionRepository.GetTransactionsByDateRangeAndStockAsync(startDate, endDate, stockSymbol, userId);
            if (transactions == null) return [];

            return transactions.Select(transaction => new TransactionDTO{
                TransactionId = transaction.TransactionId,
                TransactionType = transaction.TransactionType,
                Quantity = transaction.Quantity,
                PricePerUnit = transaction.PricePerUnit,
                Commission = transaction.Commission,
                TransactionDate = transaction.TransactionDate,
                UserId = transaction.UserId,
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
                UserId = transaction.UserId,
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
                UserId = transaction.UserId,
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