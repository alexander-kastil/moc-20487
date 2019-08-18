using System.Collections.Generic;

    namespace DAL.Models
    {
        public class Hotel
        {
            public int HotelId { get; set; }
            public string Name { get; set; }
            public string Address { get; set; }
            public ICollection<Room> Rooms { get; set; }
        }
    }