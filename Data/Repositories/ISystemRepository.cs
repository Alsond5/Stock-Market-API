using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockMarket.Models;

namespace StockMarket.Data.Repositories
{
    public interface ISystemRepository
    {
        Task<List<Models.System>> GetAllConfigsAsync();
        Task<Models.System?> GetConfigByIdAsync(int id);
        Task<Models.System?> GetConfigByKeyAsync(string key);
        Task<Models.System?> AddConfigAsync(Models.System config);
        Task<Models.System?> UpdateConfigAsync(Models.System config);
        Task<bool> DeleteConfigAsync(int id);
    }
}