using BlueYonder.Hotels.Service.Models;
using BlueYonder.Hotels.Service.Database;

public class DbInitializer
{
    public static void Initialize(HotelsContext context)
    {
          if(context.Database.EnsureCreated())   
          {
            context.Hotels.Add(new Hotel { Id = 1, HotelName = "Leonardo" , Address = "Jones Street 259, Manhattan", IsFullyBooked = false, Stars = 5});
            context.Hotels.Add(new Hotel { Id = 2, HotelName = "Dan", Address = "Bleecker Street 23, Manhattan", IsFullyBooked = true, Stars = 3.5 });
            context.SaveChanges();
          }
    }
}