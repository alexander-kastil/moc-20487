using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThrowHttpResponseException.Host.Models;
using Microsoft.AspNetCore.Mvc;

namespace ThrowHttpResponseException.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DestinationsController : ControllerBase
    {
        private List<Destination> _destinations;

        public DestinationsController()
        {
            _destinations = new List<Destination>();
            _destinations.Add(new Destination { Id = 1, CityName = "Seattle", Airport = "Sea-Tac" });
            _destinations.Add(new Destination { Id = 2, CityName = "New-york", Airport = "JFK" });
            _destinations.Add(new Destination { Id = 3, CityName = "Amsterdam", Airport = "Schiphol" });
            _destinations.Add(new Destination { Id = 4, CityName = "London", Airport = "Heathrow" });
            _destinations.Add(new Destination { Id = 5, CityName = "Paris", Airport = "Charles De Gaulle" });
        }

        // GET api/destinations
        [HttpGet]
        public ActionResult<IEnumerable<Destination>> Get()
        {
            return _destinations;
        }

    }
}