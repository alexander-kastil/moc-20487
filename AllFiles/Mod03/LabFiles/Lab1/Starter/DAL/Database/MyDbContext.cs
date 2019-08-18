using Microsoft.EntityFrameworkCore;
using DAL.Models;

namespace DAL.Database
{
    public class MyDbContext : DbContext
    {
        public DbSet<Traveler> Travelers { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Hotel> Hotels { get; set; }

        public MyDbContext()
                : base()
        {
            DbInitializer.Initialize(this);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=Mod3Lab1DB;Trusted_Connection=True;");
            }
        }
    }
}