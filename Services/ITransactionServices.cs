using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarket.Services
{
    public interface ITransactionServices
    {
        Task<IEnumerable<Models.Transaction>> GetAllTransactionsAsync();
        Task<Models.Transaction?> GetTransactionByIdAsync(int transactionId);
        Task<IEnumerable<Models.Transaction>> GetTransactionsByUserIdAsync(int userId);
        Task<IEnumerable<Models.Transaction>> GetTransactionsByStockIdAsync(int stockId);
        Task<IEnumerable<Models.Transaction>> GetTransactionsByPortfolioIdAsync(int portfolioId);
        Task<IEnumerable<Models.Transaction>> GetTransactionsByUserIdAndStockIdAsync(int userId, int stockId);
        Task<IEnumerable<Models.Transaction>> GetTransactionsByDateRangeForStockIdAsync(DateTime startDate, DateTime endDate, int stockId);
        Task<IEnumerable<Models.Transaction>> GetTransactionsByDateRangeAsync(DateTime startDate, DateTime endDate, int userId);
        Task<IEnumerable<Models.Transaction>> GetTransactionsByDateRangeAndStockAsync(DateTime startDate, DateTime endDate, int stockId, int userId);
        Task<Models.Transaction?> CreateTransactionAsync(Models.Transaction transaction);
        Task<Models.Transaction?> UpdateTransactionAsync(Models.Transaction transaction);
    }
}