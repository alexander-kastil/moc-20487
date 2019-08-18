using System.Collections.Generic;

    namespace BlueYonder.Hotels.DAL.Models
    {
        public class Hotel
        {
            public int HotelId { get; set; }
            public string Name { get; set; }
            public string Address { get; set; }
            public IEnumerable<Room> Rooms { get; set; }
        }
    }