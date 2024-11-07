using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockMarket.Data.Repositories;
using StockMarket.Dtos.Balance;

namespace StockMarket.Services
{
    public class BalanceServices(IBalanceRepository balanceRepository) : IBalanceServices
    {
        private readonly IBalanceRepository _balanceRepository = balanceRepository;

        public async Task<List<BalanceDTO>> GetBalances()
        {
            var balances = await _balanceRepository.GetAllBalancesAsync();
            return balances.Select(balance => new BalanceDTO
            {
                BalanceId = balance.BalanceId,
                Amount = balance.Amount,
                UserId = balance.UserId,
                Username = balance.User?.Username ?? string.Empty,
                Email = balance.User?.Email ?? string.Empty
            }).ToList();
        }
    }
}