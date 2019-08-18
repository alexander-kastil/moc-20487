
using Microsoft.EntityFrameworkCore;
using InMemory.Dal.Models;

namespace InMemory.Dal.Database
{
    public class SchoolContext : DbContext
    {
        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }
        public virtual DbSet<Course> Courses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().HasMany(x => x.Students);
            modelBuilder.Entity<Student>().HasMany(x => x.Courses);
        }
    }
}