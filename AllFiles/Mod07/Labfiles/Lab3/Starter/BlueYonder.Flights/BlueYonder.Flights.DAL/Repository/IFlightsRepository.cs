using BlueYonder.Flights.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlueYonder.Flights.DAL.Repository
{
    public interface IFlightsRepository
    {
        IEnumerable<Flight> GetFlightByDate(string source, string destination, DateTime date);
        IEnumerable<Flight> GetAllFlights();
    }
}
