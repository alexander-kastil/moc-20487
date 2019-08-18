using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueYonder.Flights.Service.Repository
{
    public interface IPassangerRepository
    {
        List<string> GetPassangerList();
    }
}
