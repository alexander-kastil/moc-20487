using System;
using DAL.Database;
using DAL.Models;
using System.Linq;
using System.Collections.Generic;
using DAL.Repository;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DatabaseTester
{
    class Program
    {
        private static DbContextOptions<MyDbContext> _options =
           new DbContextOptionsBuilder<MyDbContext>()
               //.UseSqlite(@"Data Source = [Repository Root]\Allfiles\Mod02\LabFiles\Lab2\Database\SqliteHotel.db")
               .UseSqlite(@"Data Source = G:\Classes\20487D\Allfiles\Mod02\LabFiles\Lab2\Database\SqliteHotel.db")
               .Options;

        static async Task Main(string[] args)
        {

               using (MyDbContext context = new MyDbContext(_options))
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
