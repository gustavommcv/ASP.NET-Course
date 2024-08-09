namespace MathHTTPWithMiddlewares.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ValidateNumbers
    {
        private readonly RequestDelegate _next;

        public ValidateNumbers(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext context)
        {
            var errorMessage = string.Empty;
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

           if (!isValidNumbers) { 
                context.Response.StatusCode = 400; 
                context.Response.WriteAsync(errorMessage);
                return Task.CompletedTask;
            }

            return _next(context);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ValidateNumbersExtensions
    {
        public static IApplicationBuilder UseValidateNumbers(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ValidateNumbers>();
        }
    }
}
