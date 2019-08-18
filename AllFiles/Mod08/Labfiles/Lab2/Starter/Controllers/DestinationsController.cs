using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlueYonder.Flights.Service.Database;
using BlueYonder.Flights.Service.Models;
using BlueYonder.Flights.Service.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlueYonder.Flights.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DestinationsController : ControllerBase
    {
        private readonly IDestinationRepository _destinationRepository;

        public DestinationsController(IDestinationRepository destinationRepository)
        {
            _destinationRepository = destinationRepository;
        }

        // GET api/destinations
        [HttpGet]
        public  ActionResult<IEnumerable<Destination>> Get()
        {
            return _destinationRepository.GetDestinations();
        }

        
    }
}