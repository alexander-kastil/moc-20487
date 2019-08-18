using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            if (context.Database.EnsureCreated())
            {
                // Code to create initial data
                Seed(context);
            }
        }

        private static void Seed(ApplicationDbContext context)
        {
            // Create list with dummy users 
            List<IdentityUser> userList = new List<IdentityUser>
            {
                new IdentityUser("JonDue") { Email = "jond@outlook.com", PasswordHash = Convert.ToBase64String(Encoding.ASCII.GetBytes("password1234"))},
            };
            context.Users.AddRange(userList);
            context.SaveChanges();
        }
    }
}