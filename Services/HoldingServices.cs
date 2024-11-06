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
        ISystemServices systemServices,
        ITransactionServices transactionServices
    ) : IHoldingServices
    {
        private readonly IHoldingRepository _holdingRepository = holdingRepository;
        private readonly IStockRepository _stockRepository = stockRepository;
        private readonly IPortfolioRepository _portfolioRepository = portfolioRepository;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ISystemServices _systemServices = systemServices;
        private readonly ITransactionServices _transactionServices = transactionServices;

        public async Task<IEnumerable<HoldingDTO>> GetAllHoldingsAsync()
        {
            var holdings = await _holdingRepository.GetAllHoldingsAsync();
            if (holdings == null) return [];

            var holdingDTOs = holdings.Select(holding => new HoldingDTO
            {
                HoldingId = holding.Id,
                Quantity = holding.Quantity,
                PortfolioId = holding.PortfolioId,
                Stock = new Dtos.Stock.StockDTO
                {
                    StockId = holding.StockId,
                    StockSymbol = holding.Stock!.StockSymbol,
                    StockName = holding.Stock.StockName,
                    StockQuantity = holding.Stock.Quantity,
                    Price = holding.Stock.Price
                }
            });

            return holdingDTOs;
        }

        public async Task<HoldingDTO?> GetHoldingByIdAsync(int userId, int holdingId)
        {
            var holding = await _holdingRepository.GetHoldingByIdAsync(userId, holdingId);

            return holding == null ? null : new HoldingDTO
            {
                HoldingId = holding.Id,
                Quantity = holding.Quantity,
                PortfolioId = holding.PortfolioId,
                Stock = new Dtos.Stock.StockDTO
                {
                    StockId = holding.StockId,
                    StockSymbol = holding.Stock!.StockSymbol,
                    StockName = holding.Stock.StockName,
                    StockQuantity = holding.Stock.Quantity,
                    Price = holding.Stock.Price
                }
            };
        }

        public async Task<IEnumerable<HoldingDTO>> GetHoldingByPortfolioIdAsync(int portfolioId)
        {
            var holdings = await _holdingRepository.GetHoldingByPortfolioIdAsync(portfolioId);

            return holdings.Select(holding => new HoldingDTO
            {
                HoldingId = holding.Id,
                Quantity = holding.Quantity,
                PortfolioId = holding.PortfolioId,
                Stock = new Dtos.Stock.StockDTO
                {
                    StockId = holding.StockId,
                    StockSymbol = holding.Stock!.StockSymbol,
                    StockName = holding.Stock.StockName,
                    StockQuantity = holding.Stock.Quantity,
                    Price = holding.Stock.Price
                }
            });
        }

        public async Task<HoldingDTO?> GetHoldingByPortfolioIdAndStockIdAsync(int portfolioId, int stockId)
        {
            var holding = await _holdingRepository.GetHoldingByPortfolioIdAndStockIdAsync(portfolioId, stockId);

            return holding == null ? null : new HoldingDTO
            {
                HoldingId = holding.Id,
                Quantity = holding.Quantity,
                PortfolioId = holding.PortfolioId,
                Stock = new Dtos.Stock.StockDTO
                {
                    StockId = holding.StockId,
                    StockSymbol = holding.Stock!.StockSymbol,
                    StockName = holding.Stock.StockName,
                    StockQuantity = holding.Stock.Quantity,
                    Price = holding.Stock.Price
                }
            };
        }

        public async Task<HoldingDTO?> Buy(BuySellRequestDTO buyRequest, int userId)
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
                portfolio.TotalStocks += 1;
            }
            else
            {
                holding.Quantity += buyRequest.Quantity;

                await _holdingRepository.UpdateHoldingAsync(holding.Id, holding);
            }

            user.Balance.Amount -= requiredAmount;
            await _userRepository.UpdateUserAsync(user);

            portfolio.TotalStockQuantity += buyRequest.Quantity;
            await _portfolioRepository.UpdatePortfolioAsync(portfolio);

            stock.Quantity -= buyRequest.Quantity;
            await _stockRepository.UpdateStockAsync(stock.StockId, stock);
            
            var systemBalance = (await _systemServices.GetConfigValueAsync("systemBalance")) ?? "0";
            
            var systemBalanceAmount = decimal.Parse(systemBalance);
            systemBalanceAmount += commission;
            await _systemServices.SetConfigValueAsync("systemBalance", systemBalanceAmount.ToString());

            var transaction = new Transaction
            {
                StockId = stock.StockId,
                UserId = user.UserId,
                Quantity = buyRequest.Quantity,
                Commission = commission,
                TransactionType = "buy",
                TransactionDate = DateTime.Now,
                PricePerUnit = stock.Price
            };

            await _transactionServices.CreateTransactionAsync(transaction);

            return new HoldingDTO
            {
                HoldingId = holding.Id,
                Quantity = holding.Quantity,
                PortfolioId = holding.PortfolioId,
                Stock = new Dtos.Stock.StockDTO
                {
                    StockId = holding.StockId,
                    StockSymbol = holding.Stock!.StockSymbol,
                    StockName = holding.Stock.StockName,
                    StockQuantity = holding.Stock.Quantity,
                    Price = holding.Stock.Price
                }
            };
        }

        public async Task<HoldingDTO?> Sell(BuySellRequestDTO sellRequest, int userId)
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
                await _holdingRepository.DeleteHoldingAsync(holding.Id);
                portfolio.TotalStocks -= 1;
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

            var transaction = new Transaction
            {
                StockId = stock.StockId,
                UserId = user.UserId,
                Quantity = sellRequest.Quantity,
                Commission = commission,
                TransactionType = "sell",
                TransactionDate = DateTime.Now,
                PricePerUnit = stock.Price
            };

            await _transactionServices.CreateTransactionAsync(transaction);


            return new HoldingDTO
            {
                HoldingId = holding.Id,
                Quantity = holding.Quantity,
                PortfolioId = holding.PortfolioId,
                Stock = new Dtos.Stock.StockDTO
                {
                    StockId = holding.StockId,
                    StockSymbol = holding.Stock!.StockSymbol,
                    StockName = holding.Stock.StockName,
                    StockQuantity = holding.Stock.Quantity,
                    Price = holding.Stock.Price
                }
            };
        }
    }
}