using DAL.Database;
using DAL.Models;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace DAL.Repository
{
    public class HotelBookingRepository
    {
        private DbContextOptions<MyDbContext> _options;

        public HotelBookingRepository()
        {
            _options = new DbContextOptionsBuilder<MyDbContext>()
                // .UseSqlServer(@"Server=.\SQLEXPRESS;Database=Mod2Lab2DB;Trusted_Connection=True;")
                .UseSqlServer(@"Server=.;Database=Mod2Lab2DB;Trusted_Connection=True;")
                .Options;
        }

        public HotelBookingRepository(DbContextOptions<MyDbContext> options)
        {
            _options = options;
        }

        public async Task<Booking> Add(int travelerId, int roomId, DateTime checkIn, int guest = 1)
        {
            using (MyDbContext context = new MyDbContext(_options))
            {
                Traveler traveler = context.Travelers.FirstOrDefault(t => t.TravelerId == travelerId);
                Room room = context.Rooms.FirstOrDefault(r => r.RoomId == roomId);

                if (traveler != null && room != null)
                {
                    Booking newBooking = new Booking()
                    {
                        DateCreated = DateTime.Now,
                        CheckIn = checkIn,
                        CheckOut = checkIn.AddDays(1),
                        Guests = guest,
                        Paid = false,
                        Traveler = traveler,
                        Room = room
                    };

                    Booking booking = (await context.Bookings.AddAsync(newBooking))?.Entity;
                    await context.SaveChangesAsync();
                    return booking;
                }

                return null;
            }
        }

        public async Task<Booking> Update(Booking bookingToUpdate)
        {
            using (MyDbContext context = new MyDbContext(_options))
            {
                Booking booking = context.Bookings.Update(bookingToUpdate)?.Entity;
                await context.SaveChangesAsync();
                return booking;
            }
        }

        public async void Delete(int bookingId)
        {
            using (MyDbContext context = new MyDbContext(_options))
            {
                Booking booking = context.Bookings.FirstOrDefault(b => b.BookingId == bookingId);

                if (booking != null)
                {
                    context.Bookings.Remove(booking);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
