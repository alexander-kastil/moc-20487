using Microsoft.EntityFrameworkCore;
using MyFirstEF.Models;

namespace MyFirstEF.Database
{
    public class MyDbContext : DbContext
    {
       public DbSet<Product> Products { get; set; }
       public DbSet<Store> Stores { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { 
            optionsBuilder.UseSqlServer(@"Server=.;Database=MyFirstEF;Trusted_Connection=true;MultipleActiveResultSets=true");
        }
    }

}