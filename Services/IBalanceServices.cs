using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockMarket.Dtos.Balance;

namespace StockMarket.Services
{
    public interface IBalanceServices
    {
        public Task<List<BalanceDTO>> GetBalances();
    }
}