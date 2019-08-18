using System;

namespace BlueYonder.Hotels.Service.Models
{
    [Serializable]
    public class Room
    {
        public int RoomId { get; set; }
        public int Number { get; set; }
        public decimal Price { get; set; }
    }
}