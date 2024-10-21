using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockMarket.Data.Repositories;
using StockMarket.Models;

namespace StockMarket.Services
{
    public class StockServices : IStockServices
    {
        private readonly IStockRepository _stockRepository;

        public StockServices(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        public async Task<IEnumerable<Stock>> GetAllStocksAsync()
        {
            var stocks = await _stockRepository.GetAllStocksAsync();

            return stocks;
        }

        public async Task<Stock?> DeactivateStockAsync(int stockId) {
            var stock = await _stockRepository.DeactivateStockAsync(stockId);

            if (stock == null) return null;

            return stock;
        }

        public async Task<IEnumerable<Stock>> DeactivateAllStocksAsync() {
            var stocks = await _stockRepository.DeactivateAllStocksAsync();

            return stocks;
        }
    }
}