using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockMarket.Data.Repositories;

namespace StockMarket.Services
{
    public class SystemServices(ISystemRepository systemRepository) : ISystemServices
    {
        private readonly ISystemRepository _systemRepository = systemRepository;

        public async Task<string?> GetConfigValueAsync(string key)
        {
            var config = await _systemRepository.GetConfigByKeyAsync(key);
            return config?.Value;
        }

        public async Task<bool> SetConfigValueAsync(string key, string value)
        {
            var config = await _systemRepository.GetConfigByKeyAsync(key);

            if (config == null)
            {
                config = new Models.System { Key = key, Value = value };
                await _systemRepository.AddConfigAsync(config);
            }
            else
            {
                config.Value = value;
                await _systemRepository.UpdateConfigAsync(config);
            }

            return true;
        }

        public async Task<bool> DeleteConfigValueAsync(string key)
        {
            var config = await _systemRepository.GetConfigByKeyAsync(key);

            if (config == null) return false;

            await _systemRepository.DeleteConfigAsync(config.Id);

            return true;
        }
    }
}