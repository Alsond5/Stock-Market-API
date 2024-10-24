using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        Task<Stock?> ActivateAllStocksAsync();
    }
}