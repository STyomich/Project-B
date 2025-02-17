using API.Extensions;
using Infrastructure.Seed;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);

var app = builder.Build();

app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();

// Seed data.
using (var scope = app.Services.CreateAsyncScope())
{
    var services = scope.ServiceProvider;
    await SeedData.SeedRoles(services);
}

app.Run();
