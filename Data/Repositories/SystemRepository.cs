using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StockMarket.Models;

namespace StockMarket.Data.Repositories
{
    public class SystemRepository(StockMarketDBContext context) : ISystemRepository
    {
        private readonly StockMarketDBContext _context = context;

        public async Task<List<Models.System>> GetAllConfigsAsync()
        {
            return await _context.System.ToListAsync();
        }

        public async Task<Models.System?> GetConfigByIdAsync(int id)
        {
            return await _context.System.FirstOrDefaultAsync(config => config.Id == id);
        }

        public async Task<Models.System?> GetConfigByKeyAsync(string key)
        {
            return await _context.System.FirstOrDefaultAsync(config => config.Key == key);
        }

        public async Task<Models.System?> AddConfigAsync(Models.System config)
        {
            await _context.System.AddAsync(config);
            await _context.SaveChangesAsync();

            return config;
        }

        public async Task<Models.System?> UpdateConfigAsync(Models.System config)
        {
            _context.System.Update(config);
            await _context.SaveChangesAsync();

            return config;
        }

        public async Task<bool> DeleteConfigAsync(int id)
        {
            var config = await GetConfigByIdAsync(id);

            if (config == null) return false;

            _context.System.Remove(config);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}