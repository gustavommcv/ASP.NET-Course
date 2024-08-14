using Service_Contracts;
using Services;
using StockAppWithConfiguration;
using StockAppWithConfiguration.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<IFinnService, FinnhubService>();
builder.Services.AddSingleton<IStocksService, StocksService>();

// appsettings.json - tradingOptions section
builder.Services.Configure<TradingOptions>(builder.Configuration.GetSection("TradingOptions"));

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.MapControllers();

app.Run();
