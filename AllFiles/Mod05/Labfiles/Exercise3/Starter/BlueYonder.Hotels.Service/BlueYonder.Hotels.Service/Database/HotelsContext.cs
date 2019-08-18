using System;
using Microsoft.EntityFrameworkCore;
using BlueYonder.Hotels.Service.Models;

namespace BlueYonder.Hotels.Service.Database
{
    public class HotelsContext : DbContext
    {
        public HotelsContext()
        {
        }
		
        public HotelsContext(DbContextOptions<HotelsContext> options)
        : base(options)
        {
        }
		
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseInMemoryDatabase("HotelsDb");
            }
        }
		
        public DbSet<Hotel> Hotels { get; set; }
    }
}
