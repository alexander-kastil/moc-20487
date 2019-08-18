using System;
using DAL.Database;
using DAL.Models;
using System.Linq;
using System.Collections.Generic;

namespace DatabaseTester
{
    class Program
    {
        static void Main(string[] args)
        {
            using (MyDbContext context = new MyDbContext())
            {
                DbInitializer.Initialize(context);

                Hotel hotel = context.Hotels.FirstOrDefault();
                Console.WriteLine($"hotel name: {hotel.Name}");

                Console.WriteLine("Rooms:");
                foreach (Room room in context.Rooms.ToList())
                    Console.WriteLine($"room number: {room.Number}, Price: {room.Price}");

                Console.WriteLine("Travelers:");
                foreach (Traveler traveler in context.Travelers.ToList())
                    Console.WriteLine($"traveler name: {traveler.Name}, email: {traveler.Email} ");
            }
        }
    }
}
