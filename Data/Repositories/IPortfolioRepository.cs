using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockMarket.Models;

namespace StockMarket.Data.Repositories
{
    public interface IPortfolioRepository
    {
        Task<List<Portfolio>> GetAllPortfoliosAsync();
        Task<Portfolio?> GetPortfolioByIdAsync(int id);
        Task<Portfolio?> GetPortfolioByUserIdAsync(int userId);
        Task<Portfolio?> AddPortfolioAsync(Portfolio portfolio);
        Task<Portfolio?> UpdatePortfolioAsync(Portfolio portfolio);
    }
}