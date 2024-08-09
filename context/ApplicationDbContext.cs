using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
//using secondProject.Migrations;
using secondProject.Models;

namespace secondProject.context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<TravelPackages> TravelPackages { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Booking> Bookings { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // modelBuilder.Entity<Hotel>()
            //     .HasMany(h => h.Reviews)
            //     .WithOne(r => r.Hotel)
            //     .HasForeignKey(r => r.HotelId)
            //     .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<Booking>()
            //    .HasOne(b => b.User)
            //    .WithMany(u => u.Bookings)
            //    .HasForeignKey(b => b.UserId);
            //modelBuilder.Entity<Booking>()
            //    .HasOne(b => b.Hotel)
            //    .WithMany(h => h.Bookings)
            //    .HasForeignKey(b => b.HotelId); 

            //modelBuilder.Entity<Review>()
            //    .HasOne(r => r.User)
            //    .WithMany(u => u.Reviews)
            //    .HasForeignKey(r => r.userId);

            //modelBuilder.Entity<Review>()
            //.HasOne(r => r.Hotel)
            //.WithMany(h => h.Reviews)
            //.HasForeignKey(r => r.HotelId);

        }

    }
}
