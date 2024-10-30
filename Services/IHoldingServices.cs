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
        Task<IEnumerable<HoldingDTO>> GetAllHoldingsAsync();
        Task<HoldingDTO?> GetHoldingByIdAsync(int userId, int holdingId);
        Task<IEnumerable<HoldingDTO>> GetHoldingByPortfolioIdAsync(int portfolioId);
        Task<HoldingDTO?> GetHoldingByPortfolioIdAndStockIdAsync(int portfolioId, int stockId);
        Task<HoldingDTO?> Buy(BuySellRequestDTO buyRequest, int userId);
        Task<HoldingDTO?> Sell(BuySellRequestDTO sellRequest, int userId);
    }
}