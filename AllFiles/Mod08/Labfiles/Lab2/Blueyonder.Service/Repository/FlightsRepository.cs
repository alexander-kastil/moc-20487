using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blueyonder.Service.Repository
{
    public class FlightsRepository : IFlightsRepository
    {
        public string GetFlightCode(int id)
        {
            return HashFlightCode(id);
        }

        private string HashFlightCode(int id)
        {
            throw new Exception($"Id {id} can't be hash to flight code");
        }

    }
}
