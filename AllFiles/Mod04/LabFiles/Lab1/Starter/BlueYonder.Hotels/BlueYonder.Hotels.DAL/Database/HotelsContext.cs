using Microsoft.EntityFrameworkCore;
using BlueYonder.Hotels.DAL.Models;

namespace BlueYonder.Hotels.DAL.Database
{
    public class HotelsContext : DbContext
    {
        public DbSet<Traveler> Travelers { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Hotel> Hotels { get; set; }

        private void InitialDBContext()
        {
            DbInitializer.Initialize(this);
        }

        public HotelsContext(DbContextOptions<HotelsContext> options = null)
                : base()
        {
            InitialDBContext();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=.\SQLExpress;Database=Mod4Lab1DB;Trusted_Connection=True;");
            }
        }
    }
}