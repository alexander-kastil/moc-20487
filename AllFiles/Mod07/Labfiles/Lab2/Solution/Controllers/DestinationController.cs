using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Gremlin.Net.Driver;
using Gremlin.Net.Structure.IO.GraphSON;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BlueYonder.Itineraries.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DestinationController : ControllerBase
    {
        private GremlinServer _gremlinServer;

        public DestinationController(IConfiguration configuration)
        {
            string authKey =  configuration["Authkey"];
            string hostname = configuration["HostName"];
            string database = "blueyonder";
            string collection = "traveler";
            int port = 443;

            _gremlinServer = new GremlinServer(hostname, port, enableSsl: true,
                                                 username: "/dbs/" + database + "/colls/" + collection,
                                                 password: authKey);
        }
       
        [HttpGet("Attractions/{destination}/{distanceKm}")]
        public async Task<ActionResult<string>> GetAttractions(string destination, double distanceKm)
        {
            string gremlinQuary = $"g.V('{destination}').inE('located-in').has('distance', lt({distanceKm})).outV()";
            using (var client = new GremlinClient(_gremlinServer, new GraphSON2Reader(), new GraphSON2Writer(), GremlinClient.GraphSON2MimeType))
            {
                var result = await client.SubmitAsync<dynamic>(gremlinQuary);
                return JsonConvert.SerializeObject(result); 
            }
        }

        [HttpGet("StopOvers/{source}/{destination}/{maxDurationHours}")]
        public async Task<ActionResult<List<List<string>>>> GetStopOvers(string source,string destination, int maxDurationHours)
        {
            string gremlinQuary = $"g.V('{source}').repeat(outE().inV().simplePath()).until(hasId('{destination}')).path().by('id').by('duration')";

            using (var client = new GremlinClient(_gremlinServer, new GraphSON2Reader(), new GraphSON2Writer(), GremlinClient.GraphSON2MimeType))
            {
                var result = await client.SubmitAsync<dynamic>(gremlinQuary);

                var obj = result.Select(i => i["objects"]).ToList();
                var flights = new List<List<string>>();
                foreach (IEnumerable<dynamic> item in obj)
                {
                    var cities = item.OfType<string>().ToList();
                    var distance = item.OfType<long>().Sum();
                    if(distance < maxDurationHours)
                    {
                        flights.Add(cities);
                    }
                }

                return flights;
            }
        }


      
    }
}
