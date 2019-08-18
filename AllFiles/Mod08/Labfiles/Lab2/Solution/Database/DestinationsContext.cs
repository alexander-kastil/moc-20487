using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlueYonder.Flights.Service.Models;
using Microsoft.EntityFrameworkCore;

namespace BlueYonder.Flights.Service.Database
{
    public class DestinationsContext : DbContext
    {
        public virtual DbSet<Destination> Destinations { get; set; }

        public DestinationsContext()
        {

        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //TODO add connection string
            optionsBuilder.UseSqlServer(@"");
        }
    


    }
}
