using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockMarket.Data.Repositories;
using StockMarket.Dtos.Stock;
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

        public async Task<Stock?> GetStockByIdAsync(int stockId)
        {
            var stock = await _stockRepository.GetStockByIdAsync(stockId);

            return stock;
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

        public async Task<Stock?> ActivateStockAsync(int stockId) {
            var stock = await _stockRepository.ActivateStockAsync(stockId);

            if (stock == null) return null;

            return stock;
        }

        public async Task<IEnumerable<Stock>> ActivateAllStocksAsync() {
            var stock = await _stockRepository.ActivateAllStocksAsync();

            return stock;
        }

        public async Task<Stock?> UpdateStockQuantityAsync(int stockId, UpdateStockQuantityRequestDTO request) {
            var stock = await _stockRepository.GetStockByIdAsync(stockId);
            if (stock == null) return null;

            stock.Quantity = request.Quantity;

            var updatedStock = await _stockRepository.UpdateStockAsync(stockId, stock);

            return updatedStock;
        }
    }
}