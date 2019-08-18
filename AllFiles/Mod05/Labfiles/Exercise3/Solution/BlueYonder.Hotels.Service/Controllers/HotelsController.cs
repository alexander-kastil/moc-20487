using Microsoft.AspNetCore.Mvc;
using BlueYonder.Hotels.Service.Database;
using System.Linq;
using System.Collections.Generic;
using BlueYonder.Hotels.Service.Models;

namespace BlueYonder.Hotels.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Hotel> GetAllHotels()
        {
            using (var hotelsContext = new HotelsContext())
            {
                var hotels = hotelsContext.Hotels.ToList();
                return hotels;
            }
        }
    }
}

