using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using UsingLINQtoEntity.Database;
using UsingLINQtoEntity.Models;

namespace UsingLINQtoEntity
{
    class Program
    {
        static void Main(string[] args)
        {
            using(var context = new SchoolContext())
            {
                DbInitializer.Initialize(context);
                
                 // Getting the courses list from the database
                var courses = (from c in context.Courses
                               select c).ToList();
                // Writing the courses list to the console
                foreach (var course in courses)
                {
                    // For each course, writing the students list to the console
                    Console.WriteLine($"Course: {course.Name}");
                    foreach (var student in course.Students)
                    {
                        Console.WriteLine($"\t Student name: {student.Name}");
                    }
                }

                // Waiting for user input before closing the console window
                Console.ReadLine();

            }
        }
    }
}
