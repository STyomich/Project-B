using Core.Domain.Entities;
using Core.Domain.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DbContext
{
    public class DataContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Car> Cars { get; set; }
        public DbSet<CarImage> CarImages { get; set; }
        public DbSet<CarTopic> CarTopics { get; set; }
        public DbSet<CarDocuments> CarDocuments { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<OrganizationPin> OrganizationPins { get; set; }
        public DbSet<RegistrationPlate> RegistrationPlates { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Car>()
                .HasMany(c => c.CarImages)
                .WithOne(ci => ci.Car)
                .HasForeignKey(ci => ci.CarId);
            builder.Entity<CarImage>()
                .HasOne(c => c.Car)
                .WithMany(ci => ci.CarImages)
                .HasForeignKey(ci => ci.CarId);
            builder.Entity<CarTopic>()
                .HasMany(ct => ct.Cars)
                .WithOne(c => c.CarTopic)
                .HasForeignKey(car => car.CarTopicId);
            builder.Entity<CarDocuments>()
                .HasOne(cd => cd.Car)
                .WithOne(c => c.CarDocuments)
                .HasForeignKey<CarDocuments>(cd => cd.CarId);
            builder.Entity<Organization>()
                .HasMany(o => o.OrganizationPins)
                .WithOne(op => op.Organization)
                .HasForeignKey(op => op.OrganizationId);
            builder.Entity<OrganizationPin>(x => x.HasKey(op => new { op.OrganizationId, op.UserId }));
            builder.Entity<RegistrationPlate>()
                .HasOne(rp => rp.Car)
                .WithOne(c => c.RegistrationPlate)
                .HasForeignKey<RegistrationPlate>(rp => rp.CarId);
        }
    }
}