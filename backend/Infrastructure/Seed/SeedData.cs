using Core.Domain.Entities;
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
        public static async Task SeedCarTopics(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<DataContext>();

            // Check if the car topics already exist
            if (!context.CarTopics.Any())
            {
                // Seed the car topics
                context.CarTopics.AddRange(new List<CarTopic>
                {
                    new CarTopic { CarName = "Toyota", CarModel = "Corolla", CarYear = "2021", Description = "A reliable and fuel-efficient compact car.", ImageLogoUrl = "https://i.pinimg.com/736x/83/00/65/83006546a378b74d8f3bd54836fcf0da.jpg" },
                    new CarTopic { CarName = "Ferrari", CarModel = "458 GTB", CarYear = "2020", Description = "A high-performance sports car with a powerful V8 engine.", ImageLogoUrl = "https://www.citypng.com/public/uploads/preview/hd-ferrari-logo-transparent-background-701751694773105xaxoflrdiu.png" },
                    new CarTopic { CarName = "Lamborghini", CarModel = "Countach", CarYear = "2019", Description = "An iconic supercar known for its sharp angles and powerful V12 engine.", ImageLogoUrl = "https://upload.wikimedia.org/wikipedia/uk/1/1d/Lamborghini_Logo1.png" },
                    new CarTopic { CarName = "Ford", CarModel = "Mustang", CarYear = "2021", Description = "A classic American muscle car with a modern twist.", ImageLogoUrl = "https://w7.pngwing.com/pngs/592/644/png-transparent-ford-logo-ford-motor-company-car-ford-mustang-chrysler-ford-logo-icon-miscellaneous-emblem-trademark-thumbnail.png" },
                    new CarTopic { CarName = "Chevrolet", CarModel = "Camaro", CarYear = "2020", Description = "A stylish and powerful sports car with a range of engine options.", ImageLogoUrl = "https://www.citypng.com/public/uploads/preview/hd-chevrolet-logo-emblem-png-701751694713490invi8kyhpy.png" },
                    new CarTopic { CarName = "BMW", CarModel = "M3", CarYear = "2019", Description = "A high-performance sedan with a perfect blend of luxury and sportiness.", ImageLogoUrl = "https://i.pinimg.com/1200x/9b/98/fe/9b98fe7973d0cb009e68fee1a586417a.jpg" },
                    new CarTopic { CarName = "Mercedes-Benz", CarModel = "C-Class", CarYear = "2021", Description = "A luxury sedan with advanced technology and comfort features.", ImageLogoUrl = "https://c0.klipartz.com/pngpicture/803/525/gratis-png-mercedes-benz-logo-audi-car-bmw-mercedes-benz-luxury-mercedes-benz-logo.png" },
                });
            }

            // Save changes asynchronously
            await context.SaveChangesAsync();
        }
    }
}