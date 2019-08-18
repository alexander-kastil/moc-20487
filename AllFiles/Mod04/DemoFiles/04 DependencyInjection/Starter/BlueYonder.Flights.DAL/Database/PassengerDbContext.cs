using Microsoft.EntityFrameworkCore;
using BlueYonder.Flights.DAL.Models;

namespace BlueYonder.Flights.DAL.Database
{
    public class PassengerDbContext : DbContext
    {
        public DbSet<Passenger> Passengers { get; set; }
        
        public PassengerDbContext()
                : base()
        {
            DbInitializer.Initialize(this);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=Mod4Demo1DB;Trusted_Connection=True;");
            }
        }
    }
}