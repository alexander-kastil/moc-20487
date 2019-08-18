using System;
using System.Collections.Generic;
using System.Text;

namespace BlueYonder.Flights.DAL.Models
{
    public class Flight
    {
        public int Id { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        public string FlightNumber { get; set; }
        public DateTime DepartureTime { get; set; }
    }
}
