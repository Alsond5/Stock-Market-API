using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarket.Data.Repositories
{
    public interface IAlertRepository
    {
        Task CreateAlert(int userId, int stockId, decimal? lowerPrice, decimal? upperPrice);
        Task DeleteAlert(int userId, int alertId);
        Task DeleteAlerts(int userId, int stockId);
        Task DeleteAlerts(int userId);
        Task<List<Models.Alert>> GetAlerts(int userId);
        Task<List<Models.Alert>> GetAlerts(int userId, int stockId);
        Task<Models.Alert?> GetAlert(int userId, int alertId);
        Task UpdateAlert(int userId, int alertId, decimal? lowerPrice, decimal? upperPrice);
    }
}