using System;
using System.Collections.Generic;
using BlueYonder.Hotels.DAL.Models;

namespace BlueYonder.Hotels.DAL.Database
{
    public static class DbInitializer
    {
        public static void Initialize(HotelsContext context)
        {
            if (context.Database.EnsureCreated())
            {
                // Code to create initial data
                Seed(context);
            }
        }

        private static void Seed(HotelsContext context)
        {
            // Create list with dummy travelers 
            List<Traveler> travelerList = new List<Traveler>
            {
                new Traveler(){ Name = "Jon Due", Email = "jond@outlook.com"},
                new Traveler(){ Name = "Jon Due2", Email = "jond2@outlook.com"},
                new Traveler(){ Name = "Jon Due3", Email = "jond3@outlook.com"}
            };

            // Create list with dummy reservations 
            List<Reservation> reservationList = new List<Reservation>
            {
               new Reservation()
               {
                   DateCreated = DateTime.Now,
                   CheckIn = DateTime.Now,
                   CheckOut = DateTime.Now.AddDays(2),
                   Guests = 2,
                   Traveler = travelerList[0],
               },
               new Reservation()
               {
                   DateCreated = DateTime.Now.AddDays(3),
                   CheckIn = DateTime.Now.AddDays(5),
                   CheckOut = DateTime.Now.AddDays(8),
                   Guests = 3,
                   Traveler = travelerList[1],
               },
               new Reservation()
               {
                   DateCreated = DateTime.Now.AddDays(-10),
                   CheckIn = DateTime.Now.AddDays(10),
                   CheckOut = DateTime.Now.AddDays(11),
                   Guests = 1,
                   Traveler = travelerList[2],
               }
            };

            // Create list with dummy rooms
            List<Room> roomList = new List<Room>
            {
                new Room(){ Number = 10, Price = 300},
                new Room(){ Number = 20, Price = 200},
                new Room(){ Number = 30, Price = 100}
            };

            reservationList[0].Room = roomList[0];
            reservationList[1].Room = roomList[1];
            reservationList[2].Room = roomList[2];

            Hotel hotel = new Hotel()
            {
                Name = "Azure Hotel",
                Address = "Cloud",
                Rooms = roomList
            };

            // Insert the dummy data to the database
            context.Travelers.AddRange(travelerList);
            context.Reservations.AddRange(reservationList);
            context.Rooms.AddRange(roomList);
            context.Hotels.Add(hotel);

            context.SaveChanges();
        }
    }
}