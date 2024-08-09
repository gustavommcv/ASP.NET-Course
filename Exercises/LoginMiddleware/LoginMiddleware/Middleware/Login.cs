using LoginMiddleware.Data;

namespace LoginMiddleware.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class Login
    {
        private readonly RequestDelegate _next;
        private readonly UserContext _userContext;

        public Login(RequestDelegate next, UserContext userContext)
        {
            _next = next;
            _userContext = userContext;
        }

        public Task Invoke(HttpContext httpContext)
        {
            var containsEmail = httpContext.Request.Query.ContainsKey("email");
            var containsPassword = httpContext.Request.Query.ContainsKey("password");

            var responseString = string.Empty;
            var queryIsIncomplete = false;
            if (!containsEmail)
            {
                responseString += "Invalid input for 'email'\n";
                queryIsIncomplete = true;
            }

            if (!containsPassword)
            {
                responseString += "Invalid input for 'password'\n";
                queryIsIncomplete = true;
            }

            if (queryIsIncomplete)
            {
                httpContext.Response.StatusCode = 400;
                httpContext.Response.WriteAsync(responseString);
                return Task.CompletedTask;
            }

            var emailQuery = httpContext.Request.Query["email"];
            var passwordQuery = httpContext.Request.Query["password"];

            var user = _userContext.CurrentUser;

            if (user.email == emailQuery && user.password == passwordQuery)
            {
                httpContext.Response.WriteAsync("Successful login");
            }
            else
            {
                httpContext.Response.StatusCode = 400;
                httpContext.Response.WriteAsync("Invalid login");
            }

            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseLogin(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<Login>();
        }
    }
}