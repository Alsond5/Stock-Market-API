using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace StockMarket.Data.Repositories
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Models.Transaction>> GetAllTransactionsAsync();
        Task<Models.Transaction?> GetTransactionByIdAsync(int transactionId);
        Task<IEnumerable<Models.Transaction>> GetTransactionsByUserIdAsync(int userId);
        Task<IEnumerable<Models.Transaction>> GetTransactionsByStockSymbolAsync(string stockSymbol);
        Task<IEnumerable<Models.Transaction>> GetTransactionsByPortfolioIdAsync(int portfolioId);
        Task<IEnumerable<Models.Transaction>> GetTransactionsByUserIdAndStockSymbolAsync(int userId, string stockSymbol);
        Task<IEnumerable<Models.Transaction>> GetTransactionsByDateRangeForStockSymbolAsync(DateTime startDate, DateTime endDate, string stockSymbol);
        Task<IEnumerable<Models.Transaction>> GetTransactionsByDateRangeAsync(DateTime startDate, DateTime endDate, int userId);
        Task<IEnumerable<Models.Transaction>> GetTransactionsByDateRangeAndStockAsync(DateTime startDate, DateTime endDate, string stockSymbol, int userId);
        Task<Models.Transaction?> AddTransactionAsync(Models.Transaction transaction);
        Task<Models.Transaction?> UpdateTransactionAsync(Models.Transaction transaction);
        Task<Models.Transaction?> DeleteTransactionAsync(int transactionId);
        Task<IEnumerable<Models.Transaction>> DeleteAllTransactionsAsync();
    }
}