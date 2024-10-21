using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockMarket.Data.Repositories;

namespace StockMarket.Services
{
    public class ConfigServices(ISystemRepository configRepository) : ISystemServices
    {
        private readonly ISystemRepository _configRepository = configRepository;

        public async Task<string?> GetConfigValueAsync(string key)
        {
            var config = await _configRepository.GetConfigByKeyAsync(key);
            return config?.Value;
        }

        public async Task<bool> SetConfigValueAsync(string key, string value)
        {
            var config = await _configRepository.GetConfigByKeyAsync(key);

            if (config == null)
            {
                config = new Models.System { Key = key, Value = value };
                await _configRepository.AddConfigAsync(config);
            }
            else
            {
                config.Value = value;
                await _configRepository.UpdateConfigAsync(config);
            }

            return true;
        }

        public async Task<bool> DeleteConfigValueAsync(string key)
        {
            var config = await _configRepository.GetConfigByKeyAsync(key);

            if (config == null) return false;

            await _configRepository.DeleteConfigAsync(config.Id);

            return true;
        }
    }
}