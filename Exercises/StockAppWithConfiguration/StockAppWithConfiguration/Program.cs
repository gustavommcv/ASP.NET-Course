using StockAppWithConfiguration;
using StockAppWithConfiguration.ServiceContracts;
using StockAppWithConfiguration.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IFinnhubService, FinnhubService>();

// appsettings.json - tradingOptions section
builder.Services.Configure<TradingOptions>(builder.Configuration.GetSection("TradingOptions"));

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.MapControllers();

app.Run();
