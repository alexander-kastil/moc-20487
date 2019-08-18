using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlueYonder.Flights.DAL.Models;
using BlueYonder.Flights.DAL.Repository;
using Microsoft.AspNetCore.Mvc;


namespace BlueYonder.Flights.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassengerController : ControllerBase
    {
        private readonly PassengerRepository _passengerRepository;

        public PassengerController()
        {
            _passengerRepository = new PassengerRepository();
        }

        // GET api/passenger
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Passenger>>> Get()
        {
            IEnumerable<Passenger> passengers = await _passengerRepository.GetAllPassengers();
            return Ok(passengers);
        }

        // GET api/passenger/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Passenger>> Get(int id)
        {
            Passenger passenger = await _passengerRepository.GetPassenger(id);
            return Ok(passenger);
        }

        // POST api/passenger
        [HttpPost]
        public async Task<ActionResult<Passenger>> Post([FromBody] Passenger newPassenger)
        {
            Passenger passenger = await _passengerRepository.Add(newPassenger);
            return Ok(passenger);
        }

        // PUT api/passenger/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Passenger>> Put([FromBody] Passenger updatePassenger)
        {
            Passenger passenger = await _passengerRepository.Update(updatePassenger);
            return Ok(passenger);
        }

        // DELETE api/passenger/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _passengerRepository.Delete(id);
        }
    }
}
