using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockMarket.Data.Repositories;
using StockMarket.Models;

namespace StockMarket.Services
{
    public class TransactionServices(ITransactionRepository transactionRepository) : ITransactionServices
    {
        private readonly ITransactionRepository _transactionRepository = transactionRepository;

        public async Task<IEnumerable<Transaction>> GetAllTransactionsAsync()
        {
            var transactions = await _transactionRepository.GetAllTransactionsAsync();

            return transactions;
        }

        public async Task<Transaction?> GetTransactionByIdAsync(int transactionId)
        {
            var transaction = await _transactionRepository.GetTransactionByIdAsync(transactionId);

            return transaction;
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByUserIdAsync(int userId)
        {
            var transactions = await _transactionRepository.GetTransactionsByUserIdAsync(userId);

            return transactions;
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByStockIdAsync(int stockId)
        {
            var transactions = await _transactionRepository.GetTransactionsByStockIdAsync(stockId);

            return transactions;
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByPortfolioIdAsync(int portfolioId)
        {
            var transactions = await _transactionRepository.GetTransactionsByPortfolioIdAsync(portfolioId);

            return transactions;
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByUserIdAndStockIdAsync(int userId, int stockId)
        {
            var transactions = await _transactionRepository.GetTransactionsByUserIdAndStockIdAsync(userId, stockId);

            return transactions;
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByDateRangeForStockIdAsync(DateTime startDate, DateTime endDate, int stockId)
        {
            var transactions = await _transactionRepository.GetTransactionsByDateRangeForStockIdAsync(startDate, endDate, stockId);

            return transactions;
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByDateRangeAsync(DateTime startDate, DateTime endDate, int userId)
        {
            var transactions = await _transactionRepository.GetTransactionsByDateRangeAsync(startDate, endDate, userId);

            return transactions;
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByDateRangeAndStockAsync(DateTime startDate, DateTime endDate, int stockId, int userId)
        {
            var transactions = await _transactionRepository.GetTransactionsByDateRangeAndStockAsync(startDate, endDate, stockId, userId);

            return transactions;
        }

        public async Task<Transaction?> CreateTransactionAsync(Transaction transaction)
        {
            var newTransaction = await _transactionRepository.AddTransactionAsync(transaction);

            return newTransaction;
        }

        public async Task<Transaction?> UpdateTransactionAsync(Transaction transaction)
        {
            var updatedTransaction = await _transactionRepository.UpdateTransactionAsync(transaction);

            return updatedTransaction;
        }
    }
}