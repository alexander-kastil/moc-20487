using System;
using MyFirstEF.Database;

namespace MyFirstEF
{
    class Program
    {
        static void Main(string[] args)
        {
            using(var context = new MyDbContext())
            {
                DbInitializer.Initialize(context);
            }

            Console.WriteLine("Database created");
        }
    }
}
