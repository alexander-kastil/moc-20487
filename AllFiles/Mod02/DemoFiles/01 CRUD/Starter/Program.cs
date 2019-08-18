using System;
using System.Linq;
using CRUD.Database;
using CRUD.Models;

namespace CRUD
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new SchoolContext())
            {
                // Initializing the database and populating seed data
                DbInitializer.Initialize(context);
                
               
            }
        }
    }
}
