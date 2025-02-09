using System.Reflection;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });
            services.AddFluentValidationAutoValidation();

            return services;
        }
    }
}