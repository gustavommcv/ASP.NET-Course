using MathHTTPWithMiddlewares.Middlewares;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseValidateInputs();
app.UseValidateNumbers();
app.UseValidateOperation();

app.Run();
