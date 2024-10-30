using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockMarket.Dtos.Stock;
using StockMarket.Models;

namespace StockMarket.Services
{
    public interface IStockServices
    {
        Task<IEnumerable<Stock>> GetAllStocksAsync();
        Task<Stock?> GetStockByIdAsync(int stockId);
        Task<Stock?> DeactivateStockAsync(int stockId);
        Task<IEnumerable<Stock>> DeactivateAllStocksAsync();
        Task<Stock?> ActivateStockAsync(int stockId);
        Task<IEnumerable<Stock>> ActivateAllStocksAsync();
        Task<Stock?> UpdateStockQuantityAsync(int stockId, UpdateStockQuantityRequestDTO request);
    }
}