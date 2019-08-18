using InMemory.Dal.Models;
using InMemory.Dal.Database;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;

namespace InMemory.Dal.Repository
{
    public class TeacherRepository
    {
        private DbContextOptions<SchoolContext> _options;
        public TeacherRepository(DbContextOptions<SchoolContext> options)
        {
            _options = options;
        }

        public Teacher Add(Teacher teacher)
        {
            try
            {
                using (var context = new SchoolContext(_options))
                {
                    DbInitializer.Initialize(context);
                    var result = context.Teachers.Add(teacher).Entity;
                    context.SaveChanges();
                    return result;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public Teacher Update(Teacher teacher)
        {
            try
            {
                using (var context = new SchoolContext(_options))
                {
                    DbInitializer.Initialize(context);
                    var result = context.Teachers.Update(teacher).Entity;
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