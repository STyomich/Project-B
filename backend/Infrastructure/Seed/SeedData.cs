using Core.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Seed
{
    public static class SeedData
    {
        public static async Task SeedRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

            string[] roles = Enum.GetNames<RolesEnum>();

            foreach (var roleName in roles)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName.ToString());
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole<Guid>(roleName.ToString()));
                }
            }
        }
    }
}