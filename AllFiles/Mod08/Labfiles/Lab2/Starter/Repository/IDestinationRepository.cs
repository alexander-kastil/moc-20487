using BlueYonder.Flights.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueYonder.Flights.Service.Repository
{
    public interface IDestinationRepository
    {
         List<Destination> GetDestinations();
    }
}
