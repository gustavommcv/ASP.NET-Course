namespace MathHTTPWithMiddlewares.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ValidateInputs
    {
        private readonly RequestDelegate _next;

        public ValidateInputs(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext context)
        {
            var errorMessage = string.Empty;

            var emptyInput = false;

            if (!context.Request.Query.ContainsKey("firstNumber") || context.Request.Query["firstNumber"] == "")
            {
                errorMessage += "Invalid input for 'firstNumber'\n";
                emptyInput = true;
            }

            if (!context.Request.Query.ContainsKey("secondNumber") || context.Request.Query["secondNumber"] == "")
            {
                errorMessage += "Invalid input for 'secondNumber'\n";
                emptyInput = true;
            }

            if (!context.Request.Query.ContainsKey("operation") || context.Request.Query["operation"] == "")
            {
                errorMessage += "Invalid input for 'operation'\n";
                emptyInput = true;
            }

            if (emptyInput)
            {
                context.Response.StatusCode = 400;
                context.Response.WriteAsync(errorMessage);
                return Task.CompletedTask;
            }

            return _next(context);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ValidateInputsExtensions
    {
        public static IApplicationBuilder UseValidateInputs(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ValidateInputs>();
        }
    }
}
