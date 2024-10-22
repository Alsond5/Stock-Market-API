using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockMarket.Dtos.Holding;
using StockMarket.Models;

namespace StockMarket.Services
{
    public interface IHoldingServices
    {
        Task<IEnumerable<Holding>> GetAllHoldingsAsync();
        Task<IEnumerable<Holding>> GetHoldingByPortfolioIdAsync(int portfolioId);
        Task<Holding?> GetHoldingByPortfolioIdAndStockIdAsync(int portfolioId, int stockId);
        Task<HoldingDTO?> Buy(BuySellRequestDTO buyRequest, int userId);
        Task<HoldingDTO?> Sell(BuySellRequestDTO sellRequest, int userId);
    }
}