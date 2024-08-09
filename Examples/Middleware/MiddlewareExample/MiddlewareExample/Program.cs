using MiddlewareExample.CustomMiddlewares;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<MyCustomMiddleware>();

var app = builder.Build();

app.UseConventionalMiddleware();

app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("Hello\n");

    await next.Invoke(context);

    await context.Response.WriteAsync("Hello from 1 middleware\n");
});

//app.UseMiddleware<MyCustomMiddleware>();
app.UseMyCustomMiddleware();

// middleware 3
app.Run(async (HttpContext context) =>
{
    await context.Response.WriteAsync("Hello from run\n");
});

app.Run();
