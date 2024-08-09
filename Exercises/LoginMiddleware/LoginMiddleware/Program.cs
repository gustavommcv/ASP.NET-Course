using LoginMiddleware.Data;
using LoginMiddleware.Middleware;
using LoginMiddleware.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<UserContext>();

var app = builder.Build();

var user = new User();

var userContext = app.Services.GetRequiredService<UserContext>();
userContext.CurrentUser = new User{ };

app.UseWhen(ctx => ctx.Request.Method == "POST", 
    app =>
    {
        app.UseLogin();
    }
);

app.UseWhen(ctx => ctx.Request.Method == "GET",
    app =>
    {
        app.Use(async (context, next) =>
        {
            await context.Response.WriteAsync("No response");
            await next();
        });
    }
);

app.Run();
