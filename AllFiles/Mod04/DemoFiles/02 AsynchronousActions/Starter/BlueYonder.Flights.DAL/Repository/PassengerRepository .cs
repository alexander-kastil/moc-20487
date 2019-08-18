using BlueYonder.Flights.DAL.Database;
using BlueYonder.Flights.DAL.Models;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;


namespace BlueYonder.Flights.DAL.Repository
{
    public class PassengerRepository : IPassengerRepository
    {
        public async Task<IEnumerable<Passenger>> GetAllPassengers()
        {
            using (PassengerDbContext context = new PassengerDbContext())
            {
                List<Passenger> passengers = await context.Passengers.ToListAsync();
                return passengers;
            }
        }

        public async Task<Passenger> GetPassenger(int passengerId)
        {
            using (PassengerDbContext context = new PassengerDbContext())
            {
                Passenger passenger = await context.Passengers.FirstOrDefaultAsync(b => b.PassengerId == passengerId);
                if (passenger == null)
                    throw new KeyNotFoundException();
                return passenger;
            }
        }

        public async Task<Passenger> Add(Passenger newPassenger)
        {
            using (PassengerDbContext context = new PassengerDbContext())
            {
                Passenger passenger = (await context.Passengers.AddAsync(newPassenger))?.Entity;
                if (passenger == null)
                    throw new TaskCanceledException();
                await context.SaveChangesAsync();
                return passenger;
            }
        }

        public async Task<Passenger> Update(Passenger passengerToUpdate)
        {
            using (PassengerDbContext context = new PassengerDbContext())
            {
                Passenger passenger = context.Passengers.Update(passengerToUpdate)?.Entity;
                await context.SaveChangesAsync();
                return passenger;
            }
        }

        public async Task Delete(int passengerId)
        {
            using (PassengerDbContext context = new PassengerDbContext())
            {
                Passenger booking = context.Passengers.FirstOrDefault(b => b.PassengerId == passengerId);

                if (booking != null)
                {
                    context.Passengers.Remove(booking);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
