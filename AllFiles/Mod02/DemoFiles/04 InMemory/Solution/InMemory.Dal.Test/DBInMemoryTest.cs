using Microsoft.VisualStudio.TestTools.UnitTesting;
using InMemory.Dal.Database;
using Microsoft.EntityFrameworkCore;
using InMemory.Dal.Models;
using System.Linq;
using System;
using System.Collections.Generic;
using InMemory.Dal.Repository;

namespace InMemory.Dal.Test
{
    [TestClass]
    public class DBInMemoryTest
    {
        private DbContextOptions<SchoolContext> _options =
                new DbContextOptionsBuilder<SchoolContext>()
                    .UseInMemoryDatabase(databaseName: "TestDatabase")
                    .Options;

        private StudentRepository _studentRepository;
        private TeacherRepository _teacherRepository;

        public DBInMemoryTest()
        {
            _studentRepository = new StudentRepository(_options);
            _teacherRepository = new TeacherRepository(_options);
        }

        [TestMethod]
        public void AddStudentTest()
        {
            Student student = new Student { Name = "Kari Hensien" };
            student = _studentRepository.Add(student);

            using (var context = new SchoolContext(_options))
            {
                var result = context.Students.FirstOrDefault((s) => s.PersonId == student.PersonId);
                Assert.IsNotNull(result);
            }
        }

        [TestMethod]
        public void UpdateTeacherSalaryTest()
        {
            Teacher teacher = new Teacher { Name = "Terry Adams", Salary = 10000 };
            teacher = _teacherRepository.Add(teacher);
            teacher.Salary += 10000;
            teacher = _teacherRepository.Update(teacher);

            using (var context = new SchoolContext(_options))
            {
                var result = context.Teachers.FirstOrDefault((s) => s.PersonId == teacher.PersonId);
                Assert.AreEqual(result.Salary, 20000);
            }
        }

    }
}
