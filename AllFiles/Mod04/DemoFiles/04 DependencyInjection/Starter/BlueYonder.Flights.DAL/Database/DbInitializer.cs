using System;
using System.Collections.Generic;
using BlueYonder.Flights.DAL.Models;

namespace BlueYonder.Flights.DAL.Database
{
    public static class DbInitializer
    {
        public static void Initialize(PassengerDbContext context)
        {
            if (context.Database.EnsureCreated())
            {
                // Code to create initial data
                Seed(context);
            }
        }

        private static void Seed(PassengerDbContext context)
        {
            // Create list with dummy passengers 
            List<Passenger> passengerList = new List<Passenger>
            {
                new Passenger(){ Name = "Jon Due", Email = "jond@outlook.com"},
                new Passenger(){ Name = "Jon Due2", Email = "jond2@outlook.com"},
                new Passenger(){ Name = "Jon Due3", Email = "jond3@outlook.com"}
            };
            context.Passengers.AddRange(passengerList);
            context.SaveChanges();
        }
    }
}