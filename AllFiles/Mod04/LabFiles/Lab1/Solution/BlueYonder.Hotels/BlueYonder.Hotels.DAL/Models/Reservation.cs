 using System;

    namespace BlueYonder.Hotels.DAL.Models
    {
        public class Reservation
        {
            public int ReservationId { get; set; }
            public Room Room { get; set; }
            public DateTime DateCreated { get; set; }
            public DateTime CheckIn { get; set; }
            public DateTime CheckOut { get; set; }
            public int Guests { get; set; }
            public Traveler Traveler { get; set; }
    
        }
    }