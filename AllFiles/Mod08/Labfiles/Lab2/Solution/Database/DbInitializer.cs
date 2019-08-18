using BlueYonder.Flights.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueYonder.Flights.Service.Database
{
    public static class DbInitializer {

        public static void Initialize(DestinationsContext context)
        {
            // Code to create initial data
            if (context.Database.EnsureCreated())
            {
                // Add data to the database
                Seed(context);
            }
        }

        public static void Seed(DestinationsContext context)
        {
            var destinations  = new List<Destination>();
            destinations.Add(new Destination { Id = 1, CityName = "Seattle", Airport = "Sea-Tac" });
            destinations.Add(new Destination { Id = 2, CityName = "New-york", Airport = "JFK" });
            destinations.Add(new Destination { Id = 3, CityName = "Amsterdam", Airport = "Schiphol" });
            destinations.Add(new Destination { Id = 4, CityName = "London", Airport = "Heathrow" });
            destinations.Add(new Destination { Id = 5, CityName = "Paris", Airport = "Charles De Gaulle" });

            context.Destinations.AddRange(destinations);

            // Saving the changes to the database
            context.SaveChanges();
        }
    
    }
}
