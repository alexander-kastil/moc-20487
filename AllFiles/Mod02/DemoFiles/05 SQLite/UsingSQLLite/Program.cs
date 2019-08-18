using System;
using System.Linq;
using MyFirstEF.Database;
using MyFirstEF.Models;

namespace MyFirstEF
{
    class Program
    {
        static void Main(string[] args)
        {
            using(var context = new MyDbContext())
            {
                DbInitializer.Initialize(context);
                Console.WriteLine("Database created");

                context.Products.Add(new Product {Name = "beer"});
                context.Products.Add(new Product {Name = "wine" });
                context.SaveChanges();

                var wine = context.Products.FirstOrDefault(i => i.Name == "wine");

                Console.WriteLine(wine.Id);
            }

         
        }
    }
}
