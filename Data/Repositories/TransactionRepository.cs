using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.EntityFrameworkCore;

namespace StockMarket.Data.Repositories
{
    public class TransactionRepository(StockMarketDBContext context) : ITransactionRepository
    {
        private readonly StockMarketDBContext _context = context;

        public async Task<IEnumerable<Models.Transaction>> GetAllTransactionsAsync()
        {
            return await _context.Transactions.ToListAsync();
        }

        public async Task<Models.Transaction?> GetTransactionByIdAsync(int transactionId)
        {
            return await _context.Transactions.FirstOrDefaultAsync(transaction => transaction.TransactionId == transactionId);
        }

        public async Task<IEnumerable<Models.Transaction>> GetTransactionsByUserIdAsync(int userId)
        {
            return await _context.Transactions.Where(transaction => transaction.UserId == userId).ToListAsync();
        }

        public async Task<IEnumerable<Models.Transaction>> GetTransactionsByStockIdAsync(int stockId)
        {
            return await _context.Transactions.Where(transaction => transaction.StockId == stockId).ToListAsync();
        }

        public async Task<IEnumerable<Models.Transaction>> GetTransactionsByPortfolioIdAsync(int portfolioId)
        {
            var portfolio = await _context.Portfolios.FirstOrDefaultAsync(portfolio => portfolio.PortfolioId == portfolioId);
            if (portfolio == null) return new List<Models.Transaction>();

            return await _context.Transactions.Where(transaction => transaction.UserId == portfolio.UserId).ToListAsync();
        }

        public async Task<IEnumerable<Models.Transaction>> GetTransactionsByUserIdAndStockIdAsync(int userId, int stockId)
        {
            return await _context.Transactions.Where(transaction => transaction.UserId == userId && transaction.StockId == stockId).ToListAsync();
        }

        public async Task<IEnumerable<Models.Transaction>> GetTransactionsByDateRangeForStockIdAsync(DateTime startDate, DateTime endDate, int stockId)
        {
            return await _context.Transactions.Where(transaction => transaction.TransactionDate >= startDate && transaction.TransactionDate <= endDate && transaction.StockId == stockId).ToListAsync();
        }

        public async Task<IEnumerable<Models.Transaction>> GetTransactionsByDateRangeAsync(DateTime startDate, DateTime endDate, int userId)
        {
            return await _context.Transactions.Where(transaction => transaction.TransactionDate >= startDate && transaction.TransactionDate <= endDate && transaction.UserId == userId).ToListAsync();
        }

        public async Task<IEnumerable<Models.Transaction>> GetTransactionsByDateRangeAndStockAsync(DateTime startDate, DateTime endDate, int stockId, int userId)
        {
            return await _context.Transactions.Where(transaction => transaction.TransactionDate >= startDate && transaction.TransactionDate <= endDate && transaction.StockId == stockId && transaction.UserId == userId).ToListAsync();
        }

        public async Task<Models.Transaction?> AddTransactionAsync(Models.Transaction transaction)
        {
            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();

            return transaction;
        }

        public async Task<Models.Transaction?> UpdateTransactionAsync(Models.Transaction transaction)
        {
            _context.Transactions.Update(transaction);
            await _context.SaveChangesAsync();

            return transaction;
        }

        public async Task<Models.Transaction?> DeleteTransactionAsync(int transactionId)
        {
            var transaction = await GetTransactionByIdAsync(transactionId);

            if (transaction == null) return null;

            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();

            return transaction;
        }

        public async Task<IEnumerable<Models.Transaction>> DeleteAllTransactionsAsync()
        {
            var transactions = await GetAllTransactionsAsync();

            _context.Transactions.RemoveRange(transactions);
            await _context.SaveChangesAsync();

            return transactions;
        }
    }
}