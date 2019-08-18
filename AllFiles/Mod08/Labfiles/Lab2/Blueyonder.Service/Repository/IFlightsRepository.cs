using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blueyonder.Service.Repository
{
    public interface IFlightsRepository
    {
        string GetFlightCode(int id);
    }
}
