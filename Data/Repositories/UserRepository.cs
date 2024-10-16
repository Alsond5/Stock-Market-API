using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StockMarket.Models;

namespace StockMarket.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly StockMarketDBContext _stockMarketDBContext;

        public UserRepository(StockMarketDBContext stockMarketDBContext) {
            _stockMarketDBContext = stockMarketDBContext;
        }

        public async Task<List<User>> GetAllUsersAsync() {
            return await _stockMarketDBContext.Users.ToListAsync();
        }

        public async Task<User?> GetUserByIdAsync(int id) {
            return await _stockMarketDBContext.Users.FindAsync(id);
        }

        public async Task<User?> GetUserByUsernameAsync(string username) {
            return await _stockMarketDBContext.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User?> GetUserByEmailAsync(string email) {
            return await _stockMarketDBContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetUserByUsernameOrEmailAsync(string usernameOrEmail) {
            return await _stockMarketDBContext.Users.FirstOrDefaultAsync(u => u.Username == usernameOrEmail || u.Email == usernameOrEmail);
        }

        public async Task<User?> GetUserIfExistingAsync(string username, string email) {
            return await _stockMarketDBContext.Users.FirstOrDefaultAsync(u => u.Username == username || u.Email == email);
        }

        public async Task<User?> AddUserAsync(User user) {
            await _stockMarketDBContext.Users.AddAsync(user);
            await _stockMarketDBContext.SaveChangesAsync();

            return user;
        }

        public async Task<User?> UpdateUserAsync(User user) {
            _stockMarketDBContext.Users.Update(user);
            await _stockMarketDBContext.SaveChangesAsync();

            return user;
        }

        public async Task<bool> DeleteUserAsync(int id) {
            var user = await _stockMarketDBContext.Users.FindAsync(id);

            if (user == null) return false;

            _stockMarketDBContext.Users.Remove(user);
            await _stockMarketDBContext.SaveChangesAsync();

            return true;
        }
    }
}