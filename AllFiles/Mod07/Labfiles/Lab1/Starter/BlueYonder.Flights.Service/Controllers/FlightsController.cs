using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using System.IO;
using BlueYonder.Flights.Service.Repository;

namespace BlueYonder.Flights.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private IPassangerRepository _passangerRepository;

        public FlightsController(IPassangerRepository passangerRepository)
        {
            _passangerRepository = passangerRepository;
        }

        [HttpGet("FinalizeFlight")]
        public async Task<ActionResult> FinalizeFlight()
        {
            
            return Ok();
        }


        private System.IO.MemoryStream GeneratedManifests()
        {
            System.IO.MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            Random random = new Random();
            writer.Write("All Passenger" + Environment.NewLine);
            foreach (var passanger in _passangerRepository.GetPassangerList())
            {
                writer.Write(passanger + Environment.NewLine);
            }
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        
    }
}
