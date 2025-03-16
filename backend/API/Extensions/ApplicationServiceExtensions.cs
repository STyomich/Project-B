using System.Reflection;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using Application.Helpers;
using Application.Interfaces;
using Application.Services.RegistrationPlateService;
using Application.Services.CarImageService;
using Application.Services.Identity;
using Application.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Application.Services.UserService;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("Frontend",
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:3000", "https://localhost:3000")
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials();
                    });
            });

            var connectionString = builder.Configuration.GetConnectionString(builder.Environment.IsDevelopment() ? "DevelopmentConnection" : "ProductionConnection");

            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
            services.AddFluentValidationAutoValidation();
            services.AddAutoMapper(typeof(MappingProfiles).GetTypeInfo().Assembly);
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(RegisterUser).Assembly);
            });
            services.AddControllers(opt =>
            {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                opt.Filters.Add(new AuthorizeFilter(policy));
            });
            services.AddTransient<IRegistrationPlateService, RegistrationPlateService>();
            services.AddHttpContextAccessor();
            services.AddTransient<ICarImageService, CarImageService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<UserRepository>();

            return services;
        }
    }
}