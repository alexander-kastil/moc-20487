using Microsoft.EntityFrameworkCore;
using UsingLINQtoEntity.Models;

namespace UsingLINQtoEntity.Database {
    public class SchoolContext : DbContext {
        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }
        public virtual DbSet<Course> Courses { get; set; }

        protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder) {
            // optionsBuilder.UseLazyLoadingProxies().UseSqlServer(@"Server=.\SQLEXPRESS;Database=SchoolDBMod2DemoStoredProcedure;Trusted_Connection=True;");
            optionsBuilder.UseLazyLoadingProxies ().UseSqlServer (@"Server=.;Database=SchoolDBMod2DemoStoredProcedure;Trusted_Connection=True;");
        }

        protected override void OnModelCreating (ModelBuilder modelBuilder) {
            modelBuilder.Entity<Course> ().HasMany (x => x.Students);
            modelBuilder.Entity<Student> ().HasMany (x => x.Courses);

            modelBuilder.Entity<Person> ()
                .HasDiscriminator<string> ("PersonType");

        }
    }
}