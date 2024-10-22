using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarket.Services
{
    public interface IAlertServices
    {
        Task<List<Dtos.Alert.AlertDTO>> GetAlerts(int userId);
        Task<List<Dtos.Alert.AlertDTO>> GetAlerts(int userId, int stockId);
        Task<Dtos.Alert.AlertDTO?> GetAlert(int userId, int alertId);
        Task CreateAlert(int userId, Dtos.Alert.CreateAlertRequestDTO alert);
        Task UpdateAlert(int userId, Dtos.Alert.UpdateAlertRequestDTO alert);
        Task DeleteAlert(int userId, int alertId);
        Task DeleteAlerts(int userId, int stockId);
        Task DeleteAlerts(int userId);
    }
}