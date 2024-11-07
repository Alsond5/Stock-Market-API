using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockMarket.Attributes;
using StockMarket.Data.Repositories;
using StockMarket.Dtos.Alert;
using StockMarket.Services;

namespace StockMarket.Controllers
{
    [ApiController]
    [Route("api/alerts")]
    public class AlertController(IAlertServices alertServices) : ControllerBase
    {
        private readonly IAlertServices _alertServices = alertServices;

        [HttpGet("all")]
        [RoleAuthorize(2)]
        public async Task<IActionResult> GetAllAlerts()
        {
            var alerts = await _alertServices.GetAlerts();

            return Ok(alerts);
        }

        [HttpGet("@me/alerts")]
        [Authorize]
        public async Task<IActionResult> GetAlerts()
        {
            var user = User.FindFirst(ClaimTypes.NameIdentifier);
            if (user == null) return Unauthorized();

            var userId = int.Parse(user.Value);
            var alerts = await _alertServices.GetAlerts(userId);

            return Ok(alerts);
        }

        [HttpGet("@me/alerts/stock/{stockId}")]
        [Authorize]
        public async Task<IActionResult> GetAlerts(int stockId)
        {
            var user = User.FindFirst(ClaimTypes.NameIdentifier);
            if (user == null) return Unauthorized();

            var userId = int.Parse(user.Value);

            var alert = await _alertServices.GetAlerts(userId, stockId);

            return Ok(alert);
        }

        [HttpGet("@me/alerts/{alertId}")]
        [Authorize]
        public async Task<IActionResult> GetAlert(int alertId)
        {
            var user = User.FindFirst(ClaimTypes.NameIdentifier);
            if (user == null) return Unauthorized();

            var userId = int.Parse(user.Value);

            var alert = await _alertServices.GetAlert(userId, alertId);

            return Ok(alert);
        }

        [HttpPost("@me/alerts")]
        [Authorize]
        public async Task<IActionResult> CreateAlert([FromBody] CreateAlertRequestDTO alert)
        {
            var user = User.FindFirst(ClaimTypes.NameIdentifier);
            if (user == null) return Unauthorized();

            var userId = int.Parse(user.Value);

            await _alertServices.CreateAlert(userId, alert);

            return Ok();
        }

        [HttpPut("@me/alerts")]
        [Authorize]
        public async Task<IActionResult> UpdateAlert([FromBody] UpdateAlertRequestDTO alert)
        {
            var user = User.FindFirst(ClaimTypes.NameIdentifier);
            if (user == null) return Unauthorized();

            var userId = int.Parse(user.Value);

            await _alertServices.UpdateAlert(userId, alert);

            return Ok();
        }

        [HttpDelete("@me/alerts/{alertId}")]
        [Authorize]
        public async Task<IActionResult> DeleteAlert(int alertId)
        {
            var user = User.FindFirst(ClaimTypes.NameIdentifier);
            if (user == null) return Unauthorized();

            var userId = int.Parse(user.Value);

            await _alertServices.DeleteAlert(userId, alertId);

            return Ok();
        }

        [HttpDelete("@me/alerts/stock/{stockId}")]
        [Authorize]
        public async Task<IActionResult> DeleteAlerts(int stockId)
        {
            var user = User.FindFirst(ClaimTypes.NameIdentifier);
            if (user == null) return Unauthorized();

            var userId = int.Parse(user.Value);

            await _alertServices.DeleteAlerts(userId, stockId);

            return Ok();
        }

        [HttpDelete("@me/alerts")]
        [Authorize]
        public async Task<IActionResult> DeleteAlerts()
        {
            var user = User.FindFirst(ClaimTypes.NameIdentifier);
            if (user == null) return Unauthorized();

            var userId = int.Parse(user.Value);

            await _alertServices.DeleteAlerts(userId);

            return Ok();
        }
    }
}