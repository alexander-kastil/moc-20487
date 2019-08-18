using InMemory.Dal.Models;
using InMemory.Dal.Database;
using Microsoft.EntityFrameworkCore;
using System;

namespace InMemory.Dal.Repository
{
    public class StudentRepository
    {
        private DbContextOptions<SchoolContext> _options;
        public StudentRepository(DbContextOptions<SchoolContext> options)
        {
            _options = options;
        }

        public Student Add(Student student)
        {
            try
            {
                using (var context = new SchoolContext(_options))
                {
                    DbInitializer.Initialize(context);
                    var result = context.Students.Add(student).Entity;
                    context.SaveChanges();
                    return result;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}