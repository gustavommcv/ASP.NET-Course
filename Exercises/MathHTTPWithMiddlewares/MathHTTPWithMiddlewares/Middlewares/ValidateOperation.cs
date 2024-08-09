namespace MathHTTPWithMiddlewares.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ValidateOperation
    {
        private readonly RequestDelegate _next;

        public ValidateOperation(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext context)
        {
            var errorMessage = string.Empty;
            var operation = context.Request.Query["operation"][0];

            var firstNumber = int.Parse(context.Request.Query["firstNumber"]);
            var secondNumber = int.Parse(context.Request.Query["secondNumber"]);

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
                context.Response.WriteAsync(errorMessage);
                return Task.CompletedTask;
            }
            context.Response.WriteAsync(result.ToString());
            return _next(context);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ValidateOperationExtensions
    {
        public static IApplicationBuilder UseValidateOperation(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ValidateOperation>();
        }
    }
}
