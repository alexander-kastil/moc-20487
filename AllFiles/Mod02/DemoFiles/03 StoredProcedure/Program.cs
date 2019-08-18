using System;
using System.Data.SqlClient;
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
                
                   // Calculating the average grade for the course
                var averageGradeInCourse = (from c in context.Courses
                                            where c.Name == "ASP.NET Core"
                                            select c.Students.Average(s => s.Grade)).Single();

                Console.WriteLine($"Average grade for the course is {averageGradeInCourse}");
                
                // Adding 10 points to all the students in this course using Stored Procedure called spUpdateGrades, passing the course name and the grade change
                context.Database.ExecuteSqlCommand("spUpdateGrades @CourseName, @GradeChange",
                                                            new SqlParameter("@CourseName", "ASP.NET Core"),
                                                            new SqlParameter("@GradeChange", 10));

                // Calculating the average grade for the course after the grades update
                var averageGradeInCourseAfterGradesUpdate = (from c in context.Courses
                                                             where c.Name == "ASP.NET Core"
                                                             select c.Students.Average(s => s.Grade)).Single();

                Console.WriteLine($"Average grade for the course is after 10 points upgrade is {averageGradeInCourseAfterGradesUpdate}");
                
            }
        }
    }
}
