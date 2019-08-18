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
    }
}
