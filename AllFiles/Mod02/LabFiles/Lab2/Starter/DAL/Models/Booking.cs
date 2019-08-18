 using System;

    namespace DAL.Models
    {
        public class Booking
        {
            public int BookingId { get; set; }
            public Room Room { get; set; }
            public DateTime DateCreated { get; set; }
            public DateTime CheckIn { get; set; }
            public DateTime CheckOut { get; set; }
            public int Guests { get; set; }
            public bool Paid { get; set; }
            public Traveler Traveler { get; set; }
        }
    }