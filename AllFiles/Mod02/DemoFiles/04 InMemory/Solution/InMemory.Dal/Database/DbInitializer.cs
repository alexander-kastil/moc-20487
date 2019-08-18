
using System;
using System.Collections.Generic;
using InMemory.Dal.Models;
namespace InMemory.Dal.Database
{
    public static class DbInitializer
    {
        public static void Initialize(SchoolContext context)
        {
            // Code to create initial data
            context.Database.EnsureCreated();

        }


    }
}