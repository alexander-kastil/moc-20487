using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using BlueYonder.Flights.DAL.Models;

namespace BlueYonder.Flights.DAL.Database
{
    public class FlightsContext : DbContext
    {
        public DbSet<Flight> Flights { get; set; }

        public FlightsContext(DbContextOptions options) : base(options)
        {
            DbInitializer.Initialize(this);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=.\sqlexpress;Database=Mod7Lab3DB;Trusted_Connection=True;");
            }
        }

    }
}
