using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StockMarket.Models;

namespace StockMarket.Data.Repositories
{
    public class HoldingRepository(StockMarketDBContext context) : IHoldingRepository
    {
        private readonly StockMarketDBContext _context = context;

        public async Task<IEnumerable<Holding>> GetAllHoldingsAsync()
        {
            return await _context.Holdings.ToListAsync();
        }

        public async Task<Holding?> GetHoldingByIdAsync(int holdingId)
        {
            return await _context.Holdings.FindAsync(holdingId);
        }

        public async Task<IEnumerable<Holding>> GetHoldingByPortfolioIdAsync(int portfolioId)
        {
            return await _context.Holdings.Where(holding => holding.PortfolioId == portfolioId).ToListAsync();
        }

        public async Task<Holding?> GetHoldingByPortfolioIdAndStockIdAsync(int portfolioId, int stockId)
        {
            return await _context.Holdings.FirstOrDefaultAsync(holding => holding.PortfolioId == portfolioId && holding.StockId == stockId);
        }

        public async Task<Holding?> CreateHoldingAsync(Holding holding)
        {
            await _context.Holdings.AddAsync(holding);
            await _context.SaveChangesAsync();

            return holding;
        }

        public async Task<Holding?> UpdateHoldingAsync(int holdingId, Holding holding)
        {
            var existingHolding = await GetHoldingByIdAsync(holdingId);

            if (existingHolding == null) return null;

            existingHolding.Quantity = holding.Quantity;
            existingHolding.StockId = holding.StockId;
            existingHolding.PortfolioId = holding.PortfolioId;

            await _context.SaveChangesAsync();

            return existingHolding;
        }

        public async Task<bool> DeleteHoldingAsync(int holdingId)
        {
            var holding = await GetHoldingByIdAsync(holdingId);

            if (holding == null) return false;

            _context.Holdings.Remove(holding);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}