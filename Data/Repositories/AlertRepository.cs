using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace StockMarket.Data.Repositories
{
    public class AlertRepository(StockMarketDBContext context) : IAlertRepository
    {
        private readonly StockMarketDBContext _context = context;

        public async Task<List<Models.Alert>> GetAlerts(int userId)
        {
            return await _context.Alerts.Where(alert => alert.UserId == userId).Include(s => s.Stock).ToListAsync();
        }

        public async Task<List<Models.Alert>> GetAlerts(int userId, int stockId)
        {
            return await _context.Alerts.Where(alert => alert.UserId == userId && alert.StockId == stockId).Include(s => s.Stock).ToListAsync();
        }

        public async Task<Models.Alert?> GetAlert(int userId, int alertId)
        {
            return await _context.Alerts.Include(s => s.Stock).FirstOrDefaultAsync(alert => alert.UserId == userId && alert.AlertId == alertId);
        }

        public async Task CreateAlert(int userId, int stockId, decimal? lowerPrice, decimal? upperPrice)
        {
            var alert = new Models.Alert
            {
                UserId = userId,
                StockId = stockId,
                LowerLimit = lowerPrice ?? 0,
                UpperLimit = upperPrice ?? 0
            };

            await _context.Alerts.AddAsync(alert);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAlert(int userId, int alertId, decimal? lowerPrice, decimal? upperPrice)
        {
            var alert = await _context.Alerts.FirstOrDefaultAsync(alert => alert.UserId == userId && alert.AlertId == alertId);

            if (alert == null) return;

            alert.LowerLimit = lowerPrice ?? 0;
            alert.UpperLimit = upperPrice ?? 0;

            _context.Alerts.Update(alert);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAlert(int userId, int alertId)
        {
            var alert = await _context.Alerts.FirstOrDefaultAsync(alert => alert.UserId == userId && alert.AlertId == alertId);

            if (alert == null) return;

            _context.Alerts.Remove(alert);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAlerts(int userId, int stockId)
        {
            var alerts = await _context.Alerts.Where(alert => alert.UserId == userId && alert.StockId == stockId).ToListAsync();

            if (alerts.Count == 0) return;

            _context.Alerts.RemoveRange(alerts);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAlerts(int userId)
        {
            var alerts = await _context.Alerts.Where(alert => alert.UserId == userId).ToListAsync();

            if (alerts.Count == 0) return;

            _context.Alerts.RemoveRange(alerts);
            await _context.SaveChangesAsync();
        }
    }
}