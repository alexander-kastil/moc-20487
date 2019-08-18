using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Exercise1.Models;

namespace BlueYonder.Flights.Controllers
{
    [Route("api/[controller]")]
    public class FlightsController : Controller
    {
        private readonly FlightsContext _context;

        public FlightsController(FlightsContext context)
        {
            _context = context;
        }

        // GET https://localhost:5001/api/flights
        [HttpGet]
        public IEnumerable<Flight> Get()
        {
            return _context.Flights.ToList();
        }

        // POST https://localhost:5001/api/flights
        [HttpPost]
        public IActionResult Post([FromBody]Flight flight)
        {
            _context.Flights.Add(flight);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Get), flight.Id);
        }
       
    }
}
