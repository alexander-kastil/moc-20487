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

                // Getting the ASP Course from the courses repository
                Course ASPCourse = (from course in context.Courses
                                    where course.Name == "ASP.NET Core"
                                    select course).Single();

                // Creating two new students
                Student firstStudent = new Student() { Name = "Thomas Andersen" };
                Student secondStudent = new Student() { Name = "Terry Adams" };

                // Adding the students to the WCF course
                ASPCourse.Students.Add(firstStudent);
                ASPCourse.Students.Add(secondStudent);

                // Giving the course teacher a 1000$ raise
                ASPCourse.CourseTeacher.Salary += 1000;

                // Getting a student called Student_1
                Student studentToRemove = ASPCourse.Students.FirstOrDefault((student) => student.Name == "Student_1");

                // Remove a student from the WCF course
                ASPCourse.Students.Remove(studentToRemove);

                context.SaveChanges();

                // Print the course details to the console
                Console.WriteLine(ASPCourse);
                Console.ReadLine();
            }
        }
    }
}
