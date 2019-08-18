using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BlueYonder.Flights.Service.Models;

namespace BlueYonder.Flights.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        [HttpGet]
        // GET api/flights
        public IEnumerable<Flight> Get()
        {
            return new List<Flight>
            {
                new Flight
                {
                    Id = 1,
                    Origin = "Australia",
                    Destination = "China",
                    FlightNumber = "20487DD",
                    DepartureTime = new DateTime(2018,01,01)
                },
                new Flight
                {
                    Id = 2,
                    Origin = "New-York",
                    Destination = "Paris",
                    FlightNumber = "20487D",
                    DepartureTime = new DateTime(2018,02,02)
                }
            };
        }
    }
}
