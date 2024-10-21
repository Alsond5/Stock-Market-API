using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockMarket.Models;

namespace StockMarket.Data.Repositories
{
    public interface IStockRepository
    {
        Task<IEnumerable<Stock>> GetAllStocksAsync();
        Task<Stock?> GetStockByIdAsync(int stockId);
        Task<Stock?> GetStockBySymbolAsync(string symbol);
        Task<Stock?> CreateStockAsync(Stock stock);
        Task<Stock?> DeactivateStockAsync(int stockId);
        Task<IEnumerable<Stock>> DeactivateAllStocksAsync();
        Task<Stock?> ActivateStockAsync(int stockId);
        Task<Stock?> ActivateAllStocksAsync();
        Task<Stock?> UpdateStockAsync(int stockId, Stock stock);
        Task<bool> DeleteStockAsync(int stockId);
    }
}