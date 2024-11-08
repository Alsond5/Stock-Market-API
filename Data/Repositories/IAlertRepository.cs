using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockMarket.Models;

namespace StockMarket.Data.Repositories
{
    public interface IAlertRepository
    {
        Task<List<Alert>> GetAlerts();
        Task<List<Alert>> GetAlerts(int userId);
        Task<List<Alert>> GetAlerts(int userId, int stockId);
        Task<Alert?> GetAlert(int alertId);
        Task<Alert?> GetAlert(int userId, int alertId);
        Task CreateAlert(int userId, int stockId, decimal? lowerPrice, decimal? upperPrice);
        Task UpdateAlert(int alertId, decimal? lowerPrice, decimal? upperPrice);
        Task UpdateAlert(int userId, int alertId, decimal? lowerPrice, decimal? upperPrice);
        Task DeleteAlert(int userId, int alertId);
        Task DeleteAlerts(int userId, int stockId);
        Task DeleteAlerts(int userId);
    }
}