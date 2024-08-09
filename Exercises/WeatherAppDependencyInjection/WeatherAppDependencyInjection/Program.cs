using Contracts.Interfaces;
using Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<ICitiesService, CitiesRepository>();

var app = builder.Build();

app.MapControllers();
app.UseStaticFiles();

app.Run();
