using Entities.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Service_Contracts;
using Services;
using StockAppWithConfiguration;
using StockAppWithConfiguration.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IFinnService, FinnhubService>();
builder.Services.AddScoped<IStocksService, StocksService>();

builder.Services.AddDbContext<OrderContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// appsettings.json - tradingOptions section
builder.Services.Configure<TradingOptions>(builder.Configuration.GetSection("TradingOptions"));

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

Rotativa.AspNetCore.RotativaConfiguration.Setup("wwwroot");

app.UseStaticFiles();
app.UseRouting();
app.MapControllers();

app.Run();
