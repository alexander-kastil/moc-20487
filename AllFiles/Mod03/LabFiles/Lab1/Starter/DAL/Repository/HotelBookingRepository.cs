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
        public async Task<Booking> GetBooking(int bookingId)
        {
            using (MyDbContext context = new MyDbContext())
            {
                return await context.Bookings.Include(b => b.Traveler).FirstOrDefaultAsync(b => b.BookingId == bookingId);
            }
        }

        public async Task<Booking> Add(Booking newBooking)
        {
            using (MyDbContext context = new MyDbContext())
            {
                    Booking booking = (await context.Bookings.AddAsync(newBooking))?.Entity;
                    await context.SaveChangesAsync();
                    return booking;
            }
        }

        public async Task<Booking> Update(Booking bookingToUpdate)
        {
            using (MyDbContext context = new MyDbContext())
            {
                Booking booking = context.Bookings.Update(bookingToUpdate)?.Entity;
                await context.SaveChangesAsync();
                return booking;
            }
        }

        public async void Delete(int bookingId)
        {
            using (MyDbContext context = new MyDbContext())
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
