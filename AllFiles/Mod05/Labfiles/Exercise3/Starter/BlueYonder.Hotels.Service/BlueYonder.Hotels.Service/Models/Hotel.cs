using System;

namespace BlueYonder.Hotels.Service.Models
{
   public class Hotel
   {
     public int Id { get; set; }
     public string HotelName { get; set; }
     public string Address { get; set; }
     public double Stars { get; set; }
     public bool IsFullyBooked { get; set; }
   }
}
