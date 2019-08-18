using System;
using System.Collections.Generic;
using System.Text;
using BlueYonder.Flights.DAL.Models;
using BlueYonder.Flights.DAL.Database;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BlueYonder.Flights.DAL.Repository
{
    public class FlightsRepository : IFlightsRepository
    {
        private DbContextOptions _options = new DbContextOptionsBuilder<FlightsContext>()
                .UseInMemoryDatabase(databaseName: "FlightDB2")
                .Options;

        public IEnumerable<Flight> GetAllFlights()
        {
            using (FlightsContext context = new FlightsContext(_options))
            {
                return context.Flights.ToList();
            }
        }

        public IEnumerable<Flight> GetFlightByDate(string source, string destination, DateTime date)
        {
            using (FlightsContext context = new FlightsContext(_options))
            {
                return context.Flights.Where(f => f.Source == source && f.Destination == destination
                                         && f.DepartureTime.Date == date.Date).ToList();
            }
        }
        
    }
}
