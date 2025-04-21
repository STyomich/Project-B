using API.Extensions;
using API.Hubs;
using Infrastructure.Seed;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices(builder);
builder.Services.AddIdentityServices(builder.Configuration);

var app = builder.Build();

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("Frontend");

app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<AuctionBidHub>("/hubs/auctionbid");


// Seed data.
using (var scope = app.Services.CreateAsyncScope())
{
    var services = scope.ServiceProvider;
    await SeedData.SeedRoles(services);
    await SeedData.SeedCarTopics(services);
}

app.Run();
