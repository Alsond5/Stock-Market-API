using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockMarket.Dtos.Transaction;
using StockMarket.Models;

namespace StockMarket.Services
{
    public interface ITransactionServices
    {
        Task<IEnumerable<TransactionDTO>> GetAllTransactionsAsync();
        Task<TransactionDTO?> GetTransactionByIdAsync(int transactionId);
        Task<IEnumerable<TransactionDTO>> GetTransactionsByUserIdAsync(int userId);
        Task<IEnumerable<TransactionDTO>> GetTransactionsByStockIdAsync(int stockId);
        Task<IEnumerable<TransactionDTO>> GetTransactionsByPortfolioIdAsync(int portfolioId);
        Task<IEnumerable<TransactionDTO>> GetTransactionsByUserIdAndStockIdAsync(int userId, int stockId);
        Task<IEnumerable<TransactionDTO>> GetTransactionsByDateRangeForStockIdAsync(DateTime startDate, DateTime endDate, int stockId);
        Task<IEnumerable<TransactionDTO>> GetTransactionsByDateRangeAsync(DateTime startDate, DateTime endDate, int userId);
        Task<IEnumerable<TransactionDTO>> GetTransactionsByDateRangeAndStockAsync(DateTime startDate, DateTime endDate, int stockId, int userId);
        Task<TransactionDTO?> CreateTransactionAsync(Models.Transaction transaction);
        Task<TransactionDTO?> UpdateTransactionAsync(Models.Transaction transaction);
    }
}