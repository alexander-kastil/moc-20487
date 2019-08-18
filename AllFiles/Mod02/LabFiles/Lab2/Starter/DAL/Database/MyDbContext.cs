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

        private void InitialDBContext()
        {
            DbInitializer.Initialize(this);
        }

        // Default Constructor
        public MyDbContext()
        {
            InitialDBContext();
        }

        // Constructor with options
        public MyDbContext(DbContextOptions<MyDbContext> options)
                : base(options)
        {
            InitialDBContext();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=Mod2Lab2DB;Trusted_Connection=True;");
            }
        }
    }
}