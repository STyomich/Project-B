using Core.Domain.Entities;
using Core.Domain.IdentityEntities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DbContext
{
    public class DataContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<CarImage> CarImages { get; set; }
        public DbSet<CarTopic> CarTopics { get; set; }
        public DbSet<CarDocuments> CarDocuments { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<OrganizationPin> OrganizationPins { get; set; }
        public DbSet<RegistrationPlate> RegistrationPlates { get; set; }
        public DbSet<AuctionInfo> AuctionInfos { get; set; }
        public DbSet<AuctionBid> AuctionBids { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<UserReaction> UserReactions { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>()
                .HasOne(au => au.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(au => au.RoleId);

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

            builder.Entity<AuctionInfo>()
                .HasMany(ai => ai.AuctionBids)
                .WithOne(ab => ab.AuctionInfo)
                .HasForeignKey(ab => ab.AuctionInfoId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<AuctionBid>()
                .HasOne(ab => ab.User)
                .WithMany()
                .HasForeignKey(ab => ab.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<AuctionInfo>()
                .Property(ai => ai.StartPrice)
                .HasPrecision(18, 2);
            builder.Entity<AuctionInfo>()
                .Property(ai => ai.BuyoutPrice)
                .HasPrecision(18, 2);
            builder.Entity<AuctionBid>(x =>
            {
                x.HasKey(ab => new { ab.AuctionInfoId, ab.UserId });
                x.Property(ab => ab.BidAmount).HasPrecision(18, 4);
            });

            builder.Entity<Post>()
                .HasMany(p => p.UserReactions)
                .WithOne(ur => ur.Post)
                .HasForeignKey(ur => ur.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Post>()
                .HasMany(p => p.Comments)
                .WithOne(c => c.Post)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<UserReaction>()
                .HasKey(ur => new { ur.UserId, ur.PostId });

            builder.Entity<UserReaction>()
                .HasOne(ur => ur.User)
                .WithMany()
                .HasForeignKey(ur => ur.UserId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}