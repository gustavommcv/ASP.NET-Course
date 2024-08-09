var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Run(async (HttpContext context) =>
{
    var errorMessage = string.Empty;

    var emptyInput = false;

    if (!context.Request.Query.ContainsKey("firstNumber"))
    {
        errorMessage += "Invalid input for 'firstNumber'\n";
        emptyInput = true;
    }

    if (!context.Request.Query.ContainsKey("secondNumber"))
    {
        errorMessage += "Invalid input for 'secondNumber'\n";
        emptyInput = true;
    }

    if (!context.Request.Query.ContainsKey("operation"))
    {
        errorMessage += "Invalid input for 'operation'\n";
        emptyInput = true;
    }

    if (emptyInput) { 
        context.Response.StatusCode = 400; 
        await context.Response.WriteAsync(errorMessage);
        return; 
    }

    var isValidNumbers = true;
    if (!int.TryParse(context.Request.Query["firstNumber"], out var firstNumber))
    {
        errorMessage += "Invalid number format for 'firstNumber'\n";
        isValidNumbers = false;
    }

    if (!int.TryParse(context.Request.Query["secondNumber"], out var secondNumber))
    {
        errorMessage += "Invalid number format for 'secondNumber'\n";
        isValidNumbers = false;
    }

    if (!isValidNumbers) { context.Response.StatusCode = 400; await context.Response.WriteAsync(errorMessage); return; }

    var operation = context.Request.Query["operation"][0];

    int result = 0;
    var isValidOperation = true;
    switch (operation)
    {
        case "add":
            result = firstNumber + secondNumber; break;
        case "subtract":
            result = firstNumber - secondNumber; break;
        case "multiply":
            result = firstNumber * secondNumber; break;
        case "divide":
            result = firstNumber / secondNumber; break;
        case "module":
            result = firstNumber % secondNumber; break;
        default:
            isValidOperation = false;
            errorMessage += "Invalid input for 'operation'";
            break;
    }

    if (!isValidOperation)
    {
        context.Response.StatusCode = 400;
        await context.Response.WriteAsync(errorMessage);
        return;
    }

    await context.Response.WriteAsync(result.ToString());
});

app.Run();
