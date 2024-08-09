using ControllersExample.Controllers;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddTransient<HomeController>(); // add a single controller
builder.Services.AddControllers(); // add all the controller classes as services

var app = builder.Build();

app.UseStaticFiles();

/*
app.UseRouting(); // enable routing
app.UseEndpoints(endpoints =>
    app.MapControllers() // enables the routing for each action method
);
*/
app.MapControllers(); // calls UseRouting() & UseEndpoints

app.Run();
