using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockMarket.Data.Repositories;

namespace StockMarket.Services
{
    public class AlertServices(IAlertRepository alertRepository) : IAlertServices
    {
        private readonly IAlertRepository _alertRepository = alertRepository;

        public async Task<List<Dtos.Alert.AlertDTO>> GetAlerts(int userId)
        {
            var alerts = await _alertRepository.GetAlerts(userId);

            return alerts.Select(alert => new Dtos.Alert.AlertDTO
            {
                Id = alert.AlertId,
                StockId = alert.StockId,
                LowerLimit = alert.LowerLimit,
                UpperLimit = alert.UpperLimit,
                StockSymbol = alert.Stock?.StockSymbol ?? string.Empty,
                StockName = alert.Stock?.StockName ?? string.Empty
            }).ToList();
        }

        public async Task<List<Dtos.Alert.AlertDTO>> GetAlerts(int userId, int stockId)
        {
            var alerts = await _alertRepository.GetAlerts(userId, stockId);

            return alerts.Select(alert => new Dtos.Alert.AlertDTO
            {
                Id = alert.AlertId,
                StockId = alert.StockId,
                LowerLimit = alert.LowerLimit,
                UpperLimit = alert.UpperLimit,
                StockSymbol = alert.Stock?.StockSymbol ?? string.Empty,
                StockName = alert.Stock?.StockName ?? string.Empty
            }).ToList();
        }

        public async Task<Dtos.Alert.AlertDTO?> GetAlert(int userId, int alertId)
        {
            var alert = await _alertRepository.GetAlert(userId, alertId);

            if (alert == null) return null;

            return new Dtos.Alert.AlertDTO
            {
                Id = alert.AlertId,
                StockId = alert.StockId,
                LowerLimit = alert.LowerLimit,
                UpperLimit = alert.UpperLimit,
                StockSymbol = alert.Stock?.StockSymbol ?? string.Empty,
                StockName = alert.Stock?.StockName ?? string.Empty
            };
        }

        public async Task CreateAlert(int userId, Dtos.Alert.CreateAlertRequestDTO alert)
        {
            await _alertRepository.CreateAlert(userId, alert.StockId, alert.LowerLimit, alert.UpperLimit);
        }

        public async Task UpdateAlert(int userId, Dtos.Alert.UpdateAlertRequestDTO alert)
        {

            await _alertRepository.UpdateAlert(userId, alert.Id, alert.LowerLimit, alert.UpperLimit);
        }

        public async Task DeleteAlert(int userId, int alertId)
        {
            await _alertRepository.DeleteAlert(userId, alertId);
        }

        public async Task DeleteAlerts(int userId, int stockId)
        {
            await _alertRepository.DeleteAlerts(userId, stockId);
        }

        public async Task DeleteAlerts(int userId)
        {
            await _alertRepository.DeleteAlerts(userId);
        }
    }
}