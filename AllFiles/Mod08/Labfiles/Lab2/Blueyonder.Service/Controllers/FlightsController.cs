using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blueyonder.Service.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Blueyonder.Service.Controllers
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
       
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return _flightsRepository.GetFlightCode(id);
        }
     
    }
}
