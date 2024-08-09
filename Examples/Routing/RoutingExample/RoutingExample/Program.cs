var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//enable routing
app.UseRouting();

//creating endpoints
#pragma warning disable ASP0014 // Suggest using top level route registrations
app.UseEndpoints(endpoints =>
{
    //add your end points
    endpoints.Map("/files/{filename=sample}.{extension=txt}", async (context) =>
    {
        var fileName = context.Request.RouteValues["filename"].ToString();
        await context.Response.WriteAsync("In files\n" + fileName);
    });

    endpoints.Map("/employee/profile/{employeeName=gustavo}", async (context) =>
    {
        var employeeName = context.Request.RouteValues["employeeName"].ToString();
        await context.Response.WriteAsync("In profile/files\n" + employeeName);
    });

    endpoints.Map("/products/details/{id:int?}", async (context) =>
    {
        if (context.Request.RouteValues.ContainsKey("id")) {
            var id = context.Request.RouteValues["id"].ToString();
            await context.Response.WriteAsync("In products/details\n" + id);
        } else
        {
            await context.Response.WriteAsync("Id not supplied\n");
        }
    });
});
#pragma warning restore ASP0014 // Suggest using top level route registrations

app.Run(async context =>
{
    await context.Response.WriteAsync($"Request received at {context.Request.Path}");
});

app.Run();
