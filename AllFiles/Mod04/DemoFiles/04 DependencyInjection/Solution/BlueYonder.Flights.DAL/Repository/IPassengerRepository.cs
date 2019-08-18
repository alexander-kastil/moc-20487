using BlueYonder.Flights.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlueYonder.Flights.DAL.Repository
{
    public interface IPassengerRepository
    {
        Task<IEnumerable<Passenger>> GetAllPassengers();

        Task<Passenger> GetPassenger(int passengerId);

        Task<Passenger> Add(Passenger newPassenger);

        Task<Passenger> Update(Passenger passengerToUpdate);

        Task Delete(int passengerId);
    }
}
