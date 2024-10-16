using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockMarket.Models;

namespace StockMarket.Data.Repositories
{
    public interface IBalanceRepository
    {
        Task<List<Balance>> GetAllBalancesAsync();
        Task<Balance?> GetBalanceByIdAsync(int id);
        Task<Balance?> GetBalanceByUserIdAsync(int userId);
        Task<Balance?> AddBalanceAsync(Balance balance);
        Task<Balance?> UpdateBalanceAsync(Balance balance);
    }
}