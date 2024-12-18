﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StockMarket.Data;

#nullable disable

namespace StockMarket.Migrations
{
    [DbContext(typeof(StockMarketDBContext))]
    partial class StockMarketDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("StockMarket.Models.Alert", b =>
                {
                    b.Property<int>("AlertId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AlertId"));

                    b.Property<decimal>("LowerLimit")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("StockId")
                        .HasColumnType("int");

                    b.Property<decimal>("UpperLimit")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<bool>("WasTriggeredAbove")
                        .HasColumnType("bit");

                    b.Property<bool>("WasTriggeredBelow")
                        .HasColumnType("bit");

                    b.HasKey("AlertId");

                    b.HasIndex("StockId");

                    b.HasIndex("UserId");

                    b.ToTable("Alerts");
                });

            modelBuilder.Entity("StockMarket.Models.Balance", b =>
                {
                    b.Property<int>("BalanceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BalanceId"));

                    b.Property<decimal>("Amount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(18,2)")
                        .HasDefaultValue(0m);

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("BalanceId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Balances");
                });

            modelBuilder.Entity("StockMarket.Models.Coupon", b =>
                {
                    b.Property<int>("CouponId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CouponId"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ExpiryDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsReedemed")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.HasKey("CouponId");

                    b.ToTable("Coupons");
                });

            modelBuilder.Entity("StockMarket.Models.History", b =>
                {
                    b.Property<int>("HistoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("HistoryId"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("StockId")
                        .HasColumnType("int");

                    b.HasKey("HistoryId");

                    b.HasIndex("StockId");

                    b.ToTable("Histories");
                });

            modelBuilder.Entity("StockMarket.Models.Holding", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("PortfolioId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("StockId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PortfolioId");

                    b.HasIndex("StockId");

                    b.ToTable("Holdings");
                });

            modelBuilder.Entity("StockMarket.Models.Portfolio", b =>
                {
                    b.Property<int>("PortfolioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PortfolioId"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("TotalStockQuantity")
                        .HasColumnType("int");

                    b.Property<int>("TotalStocks")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("PortfolioId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Portfolios");
                });

            modelBuilder.Entity("StockMarket.Models.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleId"));

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("StockMarket.Models.Stock", b =>
                {
                    b.Property<int>("StockId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StockId"));

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Quantity")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(10000);

                    b.Property<string>("StockName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StockSymbol")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StockId");

                    b.ToTable("Stocks");
                });

            modelBuilder.Entity("StockMarket.Models.System", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("System");
                });

            modelBuilder.Entity("StockMarket.Models.Transaction", b =>
                {
                    b.Property<int>("TransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TransactionId"));

                    b.Property<decimal>("Commission")
                        .HasColumnType("decimal(10,2)");

                    b.Property<decimal>("PricePerUnit")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("StockId")
                        .HasColumnType("int");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("TransactionType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("TransactionId");

                    b.HasIndex("StockId");

                    b.HasIndex("UserId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("StockMarket.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("StockMarket.Models.Alert", b =>
                {
                    b.HasOne("StockMarket.Models.Stock", "Stock")
                        .WithMany("Alerts")
                        .HasForeignKey("StockId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StockMarket.Models.User", "User")
                        .WithMany("Alerts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Stock");

                    b.Navigation("User");
                });

            modelBuilder.Entity("StockMarket.Models.Balance", b =>
                {
                    b.HasOne("StockMarket.Models.User", "User")
                        .WithOne("Balance")
                        .HasForeignKey("StockMarket.Models.Balance", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("StockMarket.Models.History", b =>
                {
                    b.HasOne("StockMarket.Models.Stock", "Stock")
                        .WithMany("Histories")
                        .HasForeignKey("StockId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Stock");
                });

            modelBuilder.Entity("StockMarket.Models.Holding", b =>
                {
                    b.HasOne("StockMarket.Models.Portfolio", "Portfolio")
                        .WithMany("Holdings")
                        .HasForeignKey("PortfolioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StockMarket.Models.Stock", "Stock")
                        .WithMany("Holdings")
                        .HasForeignKey("StockId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Portfolio");

                    b.Navigation("Stock");
                });

            modelBuilder.Entity("StockMarket.Models.Portfolio", b =>
                {
                    b.HasOne("StockMarket.Models.User", "User")
                        .WithOne("Portfolio")
                        .HasForeignKey("StockMarket.Models.Portfolio", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("StockMarket.Models.Transaction", b =>
                {
                    b.HasOne("StockMarket.Models.Stock", "Stock")
                        .WithMany("Transactions")
                        .HasForeignKey("StockId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StockMarket.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Stock");

                    b.Navigation("User");
                });

            modelBuilder.Entity("StockMarket.Models.User", b =>
                {
                    b.HasOne("StockMarket.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("StockMarket.Models.Portfolio", b =>
                {
                    b.Navigation("Holdings");
                });

            modelBuilder.Entity("StockMarket.Models.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("StockMarket.Models.Stock", b =>
                {
                    b.Navigation("Alerts");

                    b.Navigation("Histories");

                    b.Navigation("Holdings");

                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("StockMarket.Models.User", b =>
                {
                    b.Navigation("Alerts");

                    b.Navigation("Balance")
                        .IsRequired();

                    b.Navigation("Portfolio")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
