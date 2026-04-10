using Microsoft.EntityFrameworkCore;
using LimpopoTourDestinations.Models.Domain;

namespace LimpopoTourDestinations.Data
{
    public class TourDbContext : DbContext
    {
        public TourDbContext(DbContextOptions<TourDbContext> options)
            : base(options)
        {
        }

        public DbSet<Tour> Tours { get; set; }
        public DbSet<Guide> Guides { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Tour>()
                .Property(t => t.Price)
                .HasPrecision(18, 2);
        }
    }
}