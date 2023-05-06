using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using Event_manager_API.Entities;

namespace Event_manager_API
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Event> Event { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Form> Form { get; set; }
        public DbSet<Ticket> Ticket { get; set; }
        public DbSet<Coupon> Coupon { get; set; }
        public DbSet<Favourite> Favourite { get; set; }
        public DbSet<Follow> Follow { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Ticket>()
                .Property(p => p.TicketPrice)
                .HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Coupon>()
                .Property(p => p.DiscountPercentage)
                .HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Event>()
                .Property(p => p.TicketPrice)
                .HasColumnType("decimal(18,2)");
        }

    }
}
