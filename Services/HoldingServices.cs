using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockMarket.Data.Repositories;
using StockMarket.Dtos.Holding;
using StockMarket.Models;

namespace StockMarket.Services
{
    public class HoldingServices(
        IHoldingRepository holdingRepository,
        IStockRepository stockRepository,
        IPortfolioRepository portfolioRepository,
        IUserRepository userRepository,
        ISystemServices systemServices
    ) : IHoldingServices
    {
        private readonly IHoldingRepository _holdingRepository = holdingRepository;
        private readonly IStockRepository _stockRepository = stockRepository;
        private readonly IPortfolioRepository _portfolioRepository = portfolioRepository;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ISystemServices _systemServices = systemServices;

        public async Task<IEnumerable<Holding>> GetAllHoldingsAsync()
        {
            var holdings = await _holdingRepository.GetAllHoldingsAsync();

            return holdings;
        }

        public async Task<IEnumerable<Holding>> GetHoldingByPortfolioIdAsync(int portfolioId)
        {
            var holdings = await _holdingRepository.GetHoldingByPortfolioIdAsync(portfolioId);

            return holdings;
        }

        public async Task<Holding?> GetHoldingByPortfolioIdAndStockIdAsync(int portfolioId, int stockId)
        {
            var holding = await _holdingRepository.GetHoldingByPortfolioIdAndStockIdAsync(portfolioId, stockId);

            return holding;
        }

        public async Task<Holding?> Buy(BuySellRequestDTO buyRequest, int userId)
        {
            var stock = await _stockRepository.GetStockByIdAsync(buyRequest.StockId);
            if (stock == null || stock.IsActive == false) return null;

            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null) return null;

            var requiredAmount = stock.Price * buyRequest.Quantity;
            var commissionRatio = (await _systemServices.GetConfigValueAsync("commission")) ?? "0.05";
            var commission = requiredAmount * decimal.Parse(commissionRatio);

            requiredAmount += commission;

            if (user.Balance.Amount < requiredAmount) return null;

            var portfolio = await _portfolioRepository.GetPortfolioByUserIdAsync(userId);
            if (portfolio == null) return null;

            var holding = await _holdingRepository.GetHoldingByPortfolioIdAndStockIdAsync(portfolio.PortfolioId, stock.StockId);

            if (holding == null)
            {
                holding = new Holding
                {
                    PortfolioId = portfolio.PortfolioId,
                    StockId = stock.StockId,
                    Quantity = buyRequest.Quantity
                };

                await _holdingRepository.CreateHoldingAsync(holding);
            }
            else
            {
                holding.Quantity += buyRequest.Quantity;

                await _holdingRepository.UpdateHoldingAsync(holding.Id, holding);
            }

            user.Balance.Amount -= requiredAmount;
            await _userRepository.UpdateUserAsync(user);

            portfolio.TotalStocks += 1;
            portfolio.TotalStockQuantity += buyRequest.Quantity;
            await _portfolioRepository.UpdatePortfolioAsync(portfolio);

            stock.Quantity -= buyRequest.Quantity;
            await _stockRepository.UpdateStockAsync(stock.StockId, stock);
            
            var systemBalance = (await _systemServices.GetConfigValueAsync("systemBalance")) ?? "0";
            
            var systemBalanceAmount = decimal.Parse(systemBalance);
            systemBalanceAmount += commission;
            await _systemServices.SetConfigValueAsync("systemBalance", systemBalanceAmount.ToString());

            return holding;
        }

        public async Task<Holding?> Sell(BuySellRequestDTO sellRequest, int userId)
        {
            var stock = await _stockRepository.GetStockByIdAsync(sellRequest.StockId);
            if (stock == null || stock.IsActive == false) return null;

            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null) return null;

            var portfolio = await _portfolioRepository.GetPortfolioByUserIdAsync(userId);
            if (portfolio == null) return null;

            var holding = await _holdingRepository.GetHoldingByPortfolioIdAndStockIdAsync(portfolio.PortfolioId, stock.StockId);
            if (holding == null) return null;

            if (holding.Quantity < sellRequest.Quantity) return null;

            var commissionRatio = (await _systemServices.GetConfigValueAsync("commission")) ?? "0.05";

            var gainedAmount = stock.Price * sellRequest.Quantity;
            var commission = gainedAmount * decimal.Parse(commissionRatio);

            gainedAmount -= commission;

            user.Balance.Amount += gainedAmount;
            await _userRepository.UpdateUserAsync(user);

            portfolio.TotalStockQuantity -= sellRequest.Quantity;

            if (holding.Quantity == sellRequest.Quantity)
            {
                portfolio.TotalStocks -= 1;
                await _holdingRepository.DeleteHoldingAsync(holding.Id);
            } else
            {
                holding.Quantity -= sellRequest.Quantity;
                await _holdingRepository.UpdateHoldingAsync(holding.Id, holding);
            }

            await _portfolioRepository.UpdatePortfolioAsync(portfolio);

            stock.Quantity += sellRequest.Quantity;
            await _stockRepository.UpdateStockAsync(stock.StockId, stock);

            var systemBalance = (await _systemServices.GetConfigValueAsync("systemBalance")) ?? "0";
            
            var systemBalanceAmount = decimal.Parse(systemBalance);
            systemBalanceAmount += commission;
            await _systemServices.SetConfigValueAsync("systemBalance", systemBalanceAmount.ToString());

            return holding;
        }
    }
}