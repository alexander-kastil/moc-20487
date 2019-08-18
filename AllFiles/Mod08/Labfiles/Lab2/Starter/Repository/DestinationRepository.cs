using BlueYonder.Flights.Service.Database;
using BlueYonder.Flights.Service.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueYonder.Flights.Service.Repository
{
    public class DestinationRepository : IDestinationRepository
    {
       
        public DestinationRepository()
        {
            
        }

        public List<Destination> GetDestinations()
        {
            try
            {
                using (var context = new DestinationsContext())
                {
                    DbInitializer.Initialize(context);
                    var result = context.Destinations.ToList();
                    return result;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
