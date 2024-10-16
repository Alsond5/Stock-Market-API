using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StockMarket.Models;

namespace StockMarket.Data.Repositories
{
    public class BalanceRepository : IBalanceRepository
    {
        private readonly StockMarketDBContext _stockMarketDBContext;

        public BalanceRepository(StockMarketDBContext stockMarketDBContext) {
            this._stockMarketDBContext = stockMarketDBContext;
        }

        public async Task<List<Balance>> GetAllBalancesAsync() {
            return await _stockMarketDBContext.Balances.ToListAsync();
        }

        public async Task<Balance?> GetBalanceByIdAsync(int id) {
            return await _stockMarketDBContext.Balances.FindAsync(id);
        }

        public async Task<Balance?> GetBalanceByUserIdAsync(int userId) {
            return await _stockMarketDBContext.Balances.FirstOrDefaultAsync(b => b.UserId == userId);
        }

        public async Task<Balance?> AddBalanceAsync(Balance balance) {
            await _stockMarketDBContext.Balances.AddAsync(balance);
            await _stockMarketDBContext.SaveChangesAsync();

            return balance;
        }

        public async Task<Balance?> UpdateBalanceAsync(Balance balance) {
            _stockMarketDBContext.Balances.Update(balance);
            await _stockMarketDBContext.SaveChangesAsync();

            return balance;
        }
    }
}