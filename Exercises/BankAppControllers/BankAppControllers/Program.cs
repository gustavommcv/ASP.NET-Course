using BankAppControllers.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<IAccountService, AccountService>();

var app = builder.Build();

app.UseRouting();
app.MapControllers();

app.Run();
