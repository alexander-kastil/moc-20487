
using System;
using System.Collections.Generic;
using CRUD.Models;
namespace CRUD.Database
{
    public static class  DbInitializer
    {
         public static void Initialize(SchoolContext context)
         {
             // Code to create initial data
            if(context.Database.EnsureCreated())
            {
                // Add data to the database
                Seed(context);
            }

         }

         public static void Seed(SchoolContext context)
         {
            // Creating a fictitious teacher names
            List<string> TeacherNames = new List<string>() { "Kari Hensien", "Terry Adams", "Dan Park", "Peter Houston", "Lukas Keller", "Mathew Charles", "John Smith", "Andrew Davis", "Frank Miller", "Patrick Hines" };

            List<string> CourseNames = new List<string>() { "WCF", "WFP", "ASP.NET Core", "Advanced .Net", ".Net Performance", "LINQ", "Entity Frameword","Universal Windows" ,"Microsoft Azure", "Production Debugging" };
            // Generating ten courses
            for (int i = 0; i < 10; i++)
            {
                var teacher = new Teacher() { Name = TeacherNames[i], Salary = 100000 };
                var course = new Course { Name = CourseNames[i], CourseTeacher = teacher,  Students = new List<Student>()};
                

                Random rand = new Random(i);

                // For each course, generating ten students and assigning them to the current course
                for (int j = 0; j < 10; j++)
                {
                    var student = new Student {  Name = "Student_" + j, Grade = rand.Next(40,90)};
                    course.Students.Add(student);
                }
                
                context.Courses.Add(course);
                context.Teachers.Add(teacher);
            }

            // Saving the changes to the database
            context.SaveChanges();
         }
    }
}