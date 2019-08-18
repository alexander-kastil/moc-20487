using System;
using System.Collections.Generic;
using System.Text;
using BlueYonder.Flights.DAL.Models;


namespace BlueYonder.Flights.DAL.Database
{
    public static class DbInitializer
    {
        public static void Initialize(FlightsContext context)
        {
            if (context.Database.EnsureCreated())
            {
                // Code to create initial data
                Seed(context);
            }
        }

        private static void Seed(FlightsContext context)
        {
            List<Flight> flightsList = new List<Flight>()
            {
                new Flight
                {
                    Source = "New York",
                    Destination = "Paris",
                    DepartureTime = DateTime.Now.AddHours(2),
                    FlightNumber = "20456US"
                },
                new Flight
                {
                    Source = "New York",
                    Destination = "Paris",
                    DepartureTime = DateTime.Now.AddHours(1),
                    FlightNumber = "20457US"
                },
                new Flight
                {
                    Source = "New York",
                    Destination = "Paris",
                    DepartureTime = DateTime.Now.AddHours(8),
                    FlightNumber = "20459US"
                },
                new Flight
                {
                    Source = "New York",
                    Destination = "Paris",
                    DepartureTime = DateTime.Now.AddHours(5),
                    FlightNumber = "20451US"
                },
                new Flight
                {
                    Source = "Paris",
                    Destination = "Rome",
                    DepartureTime = DateTime.Now.AddHours(2),
                    FlightNumber = "20316Fr"
                },
                new Flight
                {
                    Source = "Paris",
                    Destination = "Rome",
                    DepartureTime = DateTime.Now.AddHours(7),
                    FlightNumber = "20316Fr"
                },
                new Flight
                {
                    Source = "Paris",
                    Destination = "Rome",
                    DepartureTime = DateTime.Now.AddHours(9),
                    FlightNumber = "20318Fr"
                }
            };

            context.AddRange(flightsList);
            context.SaveChanges();
        }
    }
}
