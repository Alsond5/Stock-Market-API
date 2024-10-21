using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarket.Services
{
    public interface ISystemServices
    {
        Task<string?> GetConfigValueAsync(string key);
        Task<bool> SetConfigValueAsync(string key, string value);
        Task<bool> DeleteConfigValueAsync(string key);
    }
}