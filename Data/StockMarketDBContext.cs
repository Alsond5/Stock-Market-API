using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StockMarket.Models;

namespace StockMarket.Data
{
    public class StockMarketDBContext(DbContextOptions dbContextOptions) : DbContext(dbContextOptions)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<History> Histories { get; set; }
        public DbSet<Alert> Alerts { get; set; }
        public DbSet<Balance> Balances { get; set; }
        public DbSet<Holding> Holdings { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Models.System> System { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(u => u.RoleId)
                .HasDefaultValue(1);

            modelBuilder.Entity<Coupon>()
                .Property(c => c.IsReedemed)
                .HasDefaultValue(false);

            modelBuilder.Entity<Balance>()
                .Property(b => b.Amount)
                .HasDefaultValue(0);

            modelBuilder.Entity<Stock>()
                .Property(s => s.IsActive)
                .HasDefaultValue(true);

            modelBuilder.Entity<Stock>()
                .Property(s => s.Quantity)
                .HasDefaultValue(10000);

            base.OnModelCreating(modelBuilder);
        }
    }
}