using System;
using System.Collections.Generic;
using DAL.Models;

namespace DAL.Database
{
    public static class DbInitializer
    {
        public static void Initialize(MyDbContext context)
        {
            if (context.Database.EnsureCreated())
            {
                // Code to create initial data
                Seed(context);
            }
        }

        private static void Seed(MyDbContext context)
        {
            // Create list with dummy travelers 
            List<Traveler> travelerList = new List<Traveler>
            {
                new Traveler(){ Name = "Jon Due", Email = "jond@outlook.com"},
                new Traveler(){ Name = "Jon Due2", Email = "jond2@outlook.com"},
                new Traveler(){ Name = "Jon Due3", Email = "jond3@outlook.com"}
            };

            // Create list with dummy bookings 
            List<Booking> bookingList = new List<Booking>
            {
                new Booking()
                {
                    DateCreated = DateTime.Now,
                    CheckIn = DateTime.Now,
                    CheckOut = DateTime.Now.AddDays(2),
                    Guests = 2,
                    Paid = true,
                    Traveler = travelerList[0]
                },
                new Booking()
                {
                    DateCreated = DateTime.Now.AddDays(3),
                    CheckIn = DateTime.Now.AddDays(5),
                    CheckOut = DateTime.Now.AddDays(8),
                    Guests = 3,
                    Paid = true,
                    Traveler = travelerList[1]
                },
                new Booking()
                {
                    DateCreated = DateTime.Now.AddDays(-10),
                    CheckIn = DateTime.Now.AddDays(10),
                    CheckOut = DateTime.Now.AddDays(11),
                    Guests = 1,
                    Paid = false,
                    Traveler = travelerList[2]
                }
            };

            // Create list with dummy rooms
            List<Room> roomList = new List<Room>
            {
                new Room(){ Number = 10, Price = 300},
                new Room(){ Number = 20, Price = 200},
                new Room(){ Number = 30, Price = 100}
            };

            roomList[0].Bookings.Add(bookingList[0]);
            roomList[1].Bookings.Add(bookingList[1]);
            roomList[1].Bookings.Add(bookingList[2]);

            Hotel hotel = new Hotel()
            {
                Name = "Azure Hotel",
                Address = "Cloud",
                Rooms = roomList
            };

            // Insert the dummy data to the database
            context.Travelers.AddRange(travelerList);
            context.Bookings.AddRange(bookingList);
            context.Rooms.AddRange(roomList);
            context.Hotels.Add(hotel);

            context.SaveChanges();
        }
    }
}