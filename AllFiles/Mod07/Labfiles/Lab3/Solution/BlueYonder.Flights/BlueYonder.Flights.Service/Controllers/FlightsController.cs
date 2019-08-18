using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BlueYonder.Flights.DAL.Repository;
using BlueYonder.Flights.DAL.Models;
using StackExchange.Redis;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace BlueYonder.Flights.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly IFlightsRepository _flightsRepository;
        private readonly IDatabase _redisDB;
        
        public FlightsController(IConnectionMultiplexer connectionMultiplexer, IFlightsRepository flightsRepository)
        {
            _flightsRepository = flightsRepository;
             _redisDB = connectionMultiplexer.GetDatabase();
        }

        [HttpGet]
        public ActionResult<IEnumerable<Flight>> Get()
        {
            return Ok(_flightsRepository.GetAllFlights());
        }

        [HttpGet("{source}/{destination}/{date}")]
        public ActionResult<string> Get(string source,string destination,DateTime date)
        {
            var key = source + destination + date.Date.ToShortDateString();
            
            var cacheResult = _redisDB.StringGet(key);
            if (!cacheResult.HasValue)
            {
                var result = _flightsRepository.GetFlightByDate(source, destination, date);
                if (result == null)
                    return NotFound();
                _redisDB.StringSet(key,JsonConvert.SerializeObject(result));
                return Ok(result);
            }
            Request.HttpContext.Response.Headers.Add("X-Cache","true");
            return Ok(cacheResult.ToString());
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }
    }
}
