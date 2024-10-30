using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockMarket.Models;

namespace StockMarket.Data.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly StockMarketDBContext _context;

        public StockRepository(StockMarketDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Stock>> GetAllStocksAsync()
        {
            return await Task.FromResult(_context.Stocks);
        }

        public async Task<Stock?> GetStockByIdAsync(int stockId)
        {
            return await Task.FromResult(_context.Stocks.FirstOrDefault(stock => stock.StockId == stockId));
        }

        public async Task<Stock?> GetStockBySymbolAsync(string symbol)
        {
            return await Task.FromResult(_context.Stocks.FirstOrDefault(stock => stock.StockSymbol == symbol));
        }

        public async Task<Stock?> CreateStockAsync(Stock stock)
        {
            await _context.Stocks.AddAsync(stock);
            await _context.SaveChangesAsync();

            return stock;
        }

        public async Task<Stock?> DeactivateStockAsync(int stockId)
        {
            var stock = await GetStockByIdAsync(stockId);

            if (stock == null) return null;

            stock.IsActive = false;

            await _context.SaveChangesAsync();

            return stock;
        }

        public async Task<IEnumerable<Stock>> DeactivateAllStocksAsync()
        {
            var stocks = _context.Stocks;

            foreach (var stock in stocks)
            {
                stock.IsActive = false;
            }

            await _context.SaveChangesAsync();

            return stocks;
        }

        public async Task<Stock?> ActivateStockAsync(int stockId)
        {
            var stock = await GetStockByIdAsync(stockId);

            if (stock == null) return null;

            stock.IsActive = true;

            await _context.SaveChangesAsync();

            return stock;
        }

        public async Task<IEnumerable<Stock>> ActivateAllStocksAsync()
        {
            var stocks = _context.Stocks;

            foreach (var stock in stocks)
            {
                stock.IsActive = true;
            }

            await _context.SaveChangesAsync();

            return stocks;
        }

        public async Task<Stock?> UpdateStockAsync(int stockId, Stock stock)
        {
            var _stock = await GetStockByIdAsync(stockId);

            if (_stock == null) return null;

            _stock.StockSymbol = stock.StockSymbol;
            _stock.StockName = stock.StockName;
            _stock.Price = stock.Price;
            _stock.IsActive = stock.IsActive;

            await _context.SaveChangesAsync();

            return _stock;
        }

        public async Task<bool> DeleteStockAsync(int stockId)
        {
            var stock = await GetStockByIdAsync(stockId);

            if (stock == null) return false;

            _context.Stocks.Remove(stock);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}