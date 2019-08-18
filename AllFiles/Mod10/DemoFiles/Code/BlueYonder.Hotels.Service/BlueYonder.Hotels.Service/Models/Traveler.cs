using System;

namespace BlueYonder.Hotels.Service.Models
{
    [Serializable]
    public class Traveler
    {
        public int TravelerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}