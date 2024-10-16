using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StockMarket.Models;

namespace StockMarket.Data.Repositories
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly StockMarketDBContext _context;

        public PortfolioRepository(StockMarketDBContext context) {
            _context = context;
        }

        public async Task<List<Portfolio>> GetAllPortfoliosAsync() {
            return await _context.Portfolios.ToListAsync();
        }

        public async Task<Portfolio?> GetPortfolioByIdAsync(int id) {
            return await _context.Portfolios.FindAsync(id);
        }

        public async Task<Portfolio?> GetPortfolioByUserIdAsync(int userId) {
            return await _context.Portfolios.FirstOrDefaultAsync(p => p.UserId == userId);
        }

        public async Task<Portfolio?> AddPortfolioAsync(Portfolio portfolio) {
            await _context.Portfolios.AddAsync(portfolio);
            await _context.SaveChangesAsync();

            return portfolio;
        }

        public async Task<Portfolio?> UpdatePortfolioAsync(Portfolio portfolio) {
            _context.Portfolios.Update(portfolio);
            await _context.SaveChangesAsync();

            return portfolio;
        }
    }
}