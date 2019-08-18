using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlueYonder.Hotels.Service.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace BlueYonder.Hotels.Service.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase {
        private readonly IDatabase _chache;
        private IHotelRepository _hotelRepository;

        public HotelsController (IConnectionMultiplexer connectionMultiplexer, IHotelRepository hotelRepository) {
            _chache = connectionMultiplexer.GetDatabase ();
            _hotelRepository = hotelRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> Get () {
            string key = Request.Path;
            // Get the data from the cache.
            RedisValue result = _chache.StringGet (key);

            if (result.HasValue) {
                // Return data from the cache. 
                Response.Headers.Add ("X-Cache", "true");
                return Ok (JsonConvert.DeserializeObject (result));
            }

            List<string> hotels = _hotelRepository.GetHotelList ();
            // Insert the data to the cache by key. 
            _chache.StringSet (key, JsonConvert.SerializeObject (hotels), new TimeSpan (0, 1, 0));
            return Ok (hotels);
        }

        [HttpPost]
        public ActionResult<string> Post ([FromBody] string hotelname) {
            _hotelRepository.AddHotel (hotelname);

            return Ok ("Your hotel was created");
        }
    }
}