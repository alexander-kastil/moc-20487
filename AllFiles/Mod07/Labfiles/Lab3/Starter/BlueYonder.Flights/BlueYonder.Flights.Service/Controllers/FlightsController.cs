using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BlueYonder.Flights.DAL.Repository;
using BlueYonder.Flights.DAL.Models;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace BlueYonder.Flights.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly IFlightsRepository _flightsRepository;
        
        public FlightsController(IFlightsRepository flightsRepository)
        {
            _flightsRepository = flightsRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Flight>> Get()
        {
            return Ok(_flightsRepository.GetAllFlights());
        }

        [HttpGet("{source}/{destination}/{date}")]
        public ActionResult<string> Get(string source,string destination,DateTime date)
        {
           
            var result = _flightsRepository.GetFlightByDate(source, destination, date);
            return Ok(result);
         }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }
    }
}
