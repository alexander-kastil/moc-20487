
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
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
             // Creating a fictitious teacher names
            List<string> TeacherNames = new List<string>() { "Kari Hensien", "Terry Adams", "Dan Park", "Peter Houston", "Lukas Keller", "Mathew Charles", "John Smith", "Andrew Davis", "Frank Miller", "Patrick Hines" };

            List<string> CourseNames = new List<string>() { "WCF", "WFP", "ASP.NET Core", "Advanced .Net", ".Net Performance", "LINQ", "Entity Frameword","Universal Windows" ,"Microsoft Azure", "Production Debugging" };

            // Generating ten courses
            for (int i = 0; i < 10; i++)
            {
                var teacher = new Teacher() { Name = TeacherNames[i], Salary = 100000 };
                var course = new Course { Name = CourseNames[i], CourseTeacher = teacher, Students = new List<Student>() };

                Random rand = new Random(i);

                // For each course, generating ten students and assigning them to the current course
                for (int j = 0; j < 10; j++)
                {
                    var student = new Student {  Name = "Student_" + j, Grade = rand.Next(40,80)};
                    course.Students.Add(student);
                }
                context.Courses.Add(course);
                context.Teachers.Add(teacher);
            }

            // Defining stored procedure that accepts CourceName and GradeChange as parameters and updates the grade to all the students in the course
            string cmdCreateProcedure = @"CREATE PROCEDURE spUpdateGrades @CourseName nvarchar(30), @GradeChange int
                                        AS
                                        BEGIN
	                                        DECLARE @CourseId int
                                            SELECT @CourseId = CourseId 
                                            FROM Courses 
                                            WHERE  Name = 'ASP.NET Core' 
                                            UPDATE Persons SET Grade = (CASE WHEN (Grade + 10) <= 100 THEN (Grade + 10)
                                            								ELSE 100
                                            								END )
                                            WHERE PersonType = 'Student' 
                                                  AND CourseId = @CourseId
                                            END";
            
            // Creating the stored procedure in database
            context.Database.ExecuteSqlCommand(cmdCreateProcedure);

            // Saving the changes to the database
            context.SaveChanges();
         }
    }
}