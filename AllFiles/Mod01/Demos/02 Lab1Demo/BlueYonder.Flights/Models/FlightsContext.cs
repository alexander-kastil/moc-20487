    
using Microsoft.EntityFrameworkCore;


namespace Exercise1.Models
{
    public class FlightsContext : DbContext
    {
    public FlightsContext(DbContextOptions<FlightsContext> options): base(options)
    {
    }

    public DbSet<Flight> Flights { get; set; }
    }
}