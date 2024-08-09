using ConfigurationExample.Options;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.Configure<WeatherApiOptions>(builder.Configuration.GetSection("weatherapi"));

var app = builder.Build();

app.UseStaticFiles();
app.MapControllers();
app.UseRouting();

/*#pragma warning disable ASP0014 // Suggest using top level route registrations
app.UseEndpoints(endpoints =>
{
    endpoints.Map("/", async context =>
    {
        //await context.Response.WriteAsync(app.Configuration["MyKey"]);
        await context.Response.WriteAsync(app.Configuration.GetValue<string>("mykey") + "\n");
        await context.Response.WriteAsync(app.Configuration.GetValue<int>("x", 10).ToString());
    });
});
#pragma warning restore ASP0014 // Suggest using top level route registrations*/

app.Run();
