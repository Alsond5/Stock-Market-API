using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockMarket.Models;

namespace StockMarket.Data.Repositories
{
    public interface IHoldingRepository
    {
        Task<IEnumerable<Holding>> GetAllHoldingsAsync();
        Task<Holding?> GetHoldingByIdAsync(int holdingId);
        Task<IEnumerable<Holding>> GetHoldingByPortfolioIdAsync(int portfolioId);
        Task<Holding?> GetHoldingByPortfolioIdAndStockIdAsync(int portfolioId, int stockId);
        Task<Holding?> CreateHoldingAsync(Holding holding);
        Task<Holding?> UpdateHoldingAsync(int holdingId, Holding holding);
        Task<bool> DeleteHoldingAsync(int holdingId);
    }
}