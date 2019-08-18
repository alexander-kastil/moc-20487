using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BlueYonder.Flights.Service.Models;
using BlueYonder.Flights.Service.Database;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BlueYonder.Flights.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Flight> GetAllFlights()
        {
            using (var flightContext = new FlightContext())
            {
                var flights = flightContext.Flights.Include(f => f.Travelers).ToList();
                return flights;
            }
        }

        [HttpPost]
        [Route("BookFlight")]
        public void BookFlight(int flightId, [FromBody]IEnumerable<Traveler> travelers)
        {
            using (var flightContext = new FlightContext())
            {
                var flight = flightContext.Flights.FirstOrDefault(f => f.FlightId == flightId);
                if (flight != null)
                {
                    flight.Travelers = travelers.ToList();
                    flightContext.SaveChanges();
                }
            }
        }
    }
}

