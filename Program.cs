using Microsoft.EntityFrameworkCore;
using StockMarket.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<StockMarketDBContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
