﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HttpClientApplication.Host.Models;
using Microsoft.AspNetCore.Mvc;

namespace HttpClientApplication.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DestinationsController : ControllerBase
    {
        private List<Destination> _destinations;

        public DestinationsController()
        {
            _destinations = new List<Destination>
            {
                new Destination {Id = 1, CityName = "Seattle", Airport = "Sea-Tac"},
                new Destination {Id = 2, CityName = "New-york", Airport = "JFK"},
                new Destination {Id = 3, CityName = "Amsterdam", Airport = "Schiphol"},
                new Destination {Id = 4, CityName = "London", Airport = "Heathrow"},
                new Destination {Id = 5, CityName = "Paris", Airport = "Charles De Gaulle"}
            };
        }

        // GET api/destinations
        [HttpGet]
        public ActionResult<IEnumerable<Destination>> Get()
        {
            return _destinations;
        }
    }
}
