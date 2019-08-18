using System.Collections.Generic;

    namespace BlueYonder.Hotels.DAL.Models
    {
        public class Room
        {
            public int RoomId { get; set; }
            public int Number { get; set; }
            public decimal Price { get; set; }
            public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
        }
    }