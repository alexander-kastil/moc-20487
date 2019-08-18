using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace BlueYonder.Itineraries.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DestinationController : ControllerBase
    {
       
       
        [HttpGet("Attractions/{destination}/{distanceKm}")]
        public async Task<ActionResult<string>> GetAttractions(string destination, double distanceKm)
        {
           
        }

        [HttpGet("StopOvers/{source}/{destination}/{maxDurationHours}")]
        public async Task<ActionResult<List<List<string>>>> GetStopOvers(string source,string destination, int maxDurationHours)
        {
            
        }


      
    }
}
