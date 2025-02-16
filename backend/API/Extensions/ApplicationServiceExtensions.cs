using System.Reflection;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using Application.Helpers;
using Application.Interfaces;
using Application.Services.RegistrationPlateService;

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
            services.AddAutoMapper(typeof(MappingProfiles).GetTypeInfo().Assembly);
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddTransient<IRegistrationPlateService, RegistrationPlateService>();

            return services;
        }
    }
}