using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using Event_manager_API.Entities;
using Microsoft.Extensions.Options;
using System.Drawing;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Event_manager_API
{
#pragma warning disable CS1591
    public class ApplicationDbContext : IdentityDbContext
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

            //ENTITY WITH TWO RELATIONSHIPS TO SAME TABLE
            modelBuilder.Entity<Follow>()
                .HasOne(x => x.User)
                .WithMany(x => x.Following)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Follow>()
                .HasOne(x => x.Admin)
                .WithMany(x => x.Followers)
                .HasForeignKey(x => x.AdminId)
                .OnDelete(DeleteBehavior.ClientSetNull); ;


            //DISABLE CASCADE DELETING
            modelBuilder.Entity<Favourite>()
                .HasOne<User>(s => s.User)
                .WithMany(f => f.Favourites)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Form>()
                .HasOne<User>(s => s.User)
                .WithMany(f => f.FormResponses)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ticket>()
                .HasOne<Event>(s => s.Event)
                .WithMany(f => f.Tickets)
                .HasForeignKey(u => u.EventId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ticket>()
                .HasOne<User>(s => s.User)
                .WithMany(f => f.Tickets)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Restrict);


            //DECIMAL FIELDS
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
#pragma warning restore CS1591
}
