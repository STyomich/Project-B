using Core.Domain.IdentityEntities;
using Core.Enums;
using Infrastructure.DbContext;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Seed
{
    public static class SeedData
    {
        public static async Task SeedRoles(IServiceProvider serviceProvider)
        {

            var context = serviceProvider.GetRequiredService<DataContext>();

            // Check if the roles already exist
            if (!context.Roles.Any(r => r.Name == "User"))
            {
                // Seed the "User" role
                context.Roles.Add(new Role { Name = "User" });
            }

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                // Seed the "Admin" role
                context.Roles.Add(new Role { Name = "Admin" });
            }

            // Save changes asynchronously
            await context.SaveChangesAsync();
        }
    }
}