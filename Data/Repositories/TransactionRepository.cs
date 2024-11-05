using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.EntityFrameworkCore;
using StockMarket.Models;

namespace StockMarket.Data.Repositories
{
    public class TransactionRepository(StockMarketDBContext context) : ITransactionRepository
    {
        private readonly StockMarketDBContext _context = context;

        public async Task<IEnumerable<Models.Transaction>> GetAllTransactionsAsync()
        {
            return await _context.Transactions.Include(t => t.Stock).ToListAsync();
        }

        public async Task<Models.Transaction?> GetTransactionByIdAsync(int transactionId)
        {
            return await _context.Transactions.Include(t => t.Stock).FirstOrDefaultAsync(transaction => transaction.TransactionId == transactionId);
        }

        public async Task<IEnumerable<Models.Transaction>> GetTransactionsByUserIdAsync(int userId)
        {
            return await _context.Transactions.Include(t => t.Stock).Where(transaction => transaction.UserId == userId).ToListAsync();
        }

        public async Task<IEnumerable<Models.Transaction>> GetTransactionsByStockSymbolAsync(string stockSymbol)
        {
            return await _context.Transactions.Include(t => t.Stock).Where(transaction => transaction.Stock!.StockSymbol == stockSymbol).ToListAsync();
        }

        public async Task<IEnumerable<Models.Transaction>> GetTransactionsByPortfolioIdAsync(int portfolioId)
        {
            var portfolio = await _context.Portfolios.FirstOrDefaultAsync(portfolio => portfolio.PortfolioId == portfolioId);
            if (portfolio == null) return [];

            return await _context.Transactions.Include(t => t.Stock).Where(transaction => transaction.UserId == portfolio.UserId).ToListAsync();
        }

        public async Task<IEnumerable<Models.Transaction>> GetTransactionsByUserIdAndStockSymbolAsync(int userId, string stockSymbol)
        {
            return await _context.Transactions.Include(t => t.Stock).Where(transaction => transaction.UserId == userId && transaction.Stock!.StockSymbol == stockSymbol).ToListAsync();
        }

        public async Task<IEnumerable<Models.Transaction>> GetTransactionsByDateRangeForStockSymbolAsync(DateTime startDate, DateTime endDate, string stockSymbol)
        {
            return await _context.Transactions.Include(t => t.Stock).Where(transaction => transaction.TransactionDate >= startDate && transaction.TransactionDate <= endDate && transaction.Stock!.StockSymbol == stockSymbol).ToListAsync();
        }

        public async Task<IEnumerable<Models.Transaction>> GetTransactionsByDateRangeAsync(DateTime startDate, DateTime endDate, int userId)
        {
            return await _context.Transactions.Include(t => t.Stock).Where(transaction => transaction.TransactionDate >= startDate && transaction.TransactionDate <= endDate && transaction.UserId == userId).Include(s => s.Stock).ToListAsync();
        }

        public async Task<IEnumerable<Models.Transaction>> GetTransactionsByDateRangeAndStockAsync(DateTime startDate, DateTime endDate, string stockSymbol, int userId)
        {
            return await _context.Transactions.Include(t => t.Stock).Where(transaction => transaction.TransactionDate >= startDate && transaction.TransactionDate <= endDate && transaction.Stock!.StockSymbol == stockSymbol && transaction.UserId == userId).ToListAsync();
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