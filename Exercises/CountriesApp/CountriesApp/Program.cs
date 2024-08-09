/*var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ICountryService, CountryContext>();

var app = builder.Build();

app.UseRouting();

#pragma warning disable ASP0014 // Suggest using top level route registrations
app.UseEndpoints(endpoints =>
{
    app.MapGet("/countries", httpContext =>
    {
        var context = app.Services.GetRequiredService<ICountryService>();
        var countries = context.GetAll();
        foreach (var country in countries)
        {
            httpContext.Response.WriteAsync(country.Id + ", " + country.Name + "\n");
        }

        return Task.CompletedTask;
    });

    app.MapGet("/countries/{id:int:range(1,100)}", httpContext =>
    {
        var context = app.Services.GetRequiredService<ICountryService>();
        var countries = context.GetAll();

        if (!int.TryParse(httpContext.Request.RouteValues["id"].ToString(), out var id))
        {
            httpContext.Response.StatusCode = 400;
            return httpContext.Response.WriteAsync("[No Country] - Invalid Input");
        }

        if (context.GetById(id) == null) {
            httpContext.Response.StatusCode = 404;
            return httpContext.Response.WriteAsync("[No Country] - Not found");
        }

        return httpContext.Response.WriteAsync(context.GetById(id).Name);
    });

    endpoints.MapGet("/countries/{countryID:min(101)}", async httpContext =>
    {
        httpContext.Response.StatusCode = 400;
        await httpContext.Response.WriteAsync("The CountryID should be between 1 and 100");
    });

    endpoints.MapGet("/countries/{countryID:max(1)}", async httpContext =>
    {
        httpContext.Response.StatusCode = 400;
        await httpContext.Response.WriteAsync("The CountryID should be between 1 and 100");
    });

    endpoints.MapGet("/countries/{countryID:alpha}", async httpContext =>
    {
        httpContext.Response.StatusCode = 400;
        await httpContext.Response.WriteAsync("[No Country] - Invalid Input");
    });
});
#pragma warning restore ASP0014 // Suggest using top level route registrations

app.Run();
*/

using System;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseRouting();

Dictionary<int, string> countries = new Dictionary<int, string>();
countries.Add(1, "United States");
countries.Add(2, "Canada");
countries.Add(3, "United Kingdom");
countries.Add(4, "India");
countries.Add(5, "Japan");


#pragma warning disable ASP0014 // Suggest using top level route registrations
app.UseEndpoints(endpoints => {
    endpoints.Map("/countries", async context => {
        foreach (var item in countries)
        {
            await context.Response.WriteAsync($"{item.Key}, {item.Value}\n");
        }

    });

    endpoints.Map("/countries/{countryID:int:range(1,100)}", async context => {
        int countryIdValue = Convert.ToInt32(context.Request.RouteValues["countryID"]);
        if (!countries.ContainsKey(countryIdValue))
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync($"No country\n"); ;
            return;

        }

        string countryName = countries[countryIdValue];
        await context.Response.WriteAsync($"{countryName}\n");
    });

    endpoints.Map("/countries/{countryID:alpha}", async context => {
        
        context.Response.StatusCode = 400;
        await context.Response.WriteAsync($"The CountryID should be between 1 and 100\n"); ;

    });



});
#pragma warning restore ASP0014 // Suggest using top level route registrations
app.Run();
