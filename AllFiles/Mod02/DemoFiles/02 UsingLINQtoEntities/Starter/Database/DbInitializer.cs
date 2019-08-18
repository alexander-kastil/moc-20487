
using System.Collections.Generic;
using UsingLINQtoEntity.Models;
namespace UsingLINQtoEntity.Database
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
             // Creating two teacher objects
            var englishTeacher = new Teacher { Name = "Ben Andrews", Salary = 200000, };
            var mathTeacher = new Teacher { Name = "Patrick Hines", Salary = 250000, };

            // Adding the teacher to the Teachers DBSet
            context.Teachers.Add(mathTeacher);
            context.Teachers.Add(englishTeacher);

            // Generating ten courses
            for (int i = 0; i < 10; i++)
            {
                var course = new Course { Name = "Course_" + i, Students = new List<Student>()};
                
                // For each course, generating ten students and assigning them to the current course
                for (int j = 0; j < 10; j++)
                {
                    var student = new Student {  Name = "Student_" + j, };
                    course.Students.Add(student);
                }
                context.Courses.Add(course);
            }

            // Saving the changes to the database
            context.SaveChanges();
         }
    }
}