using System;

namespace RunCPU
{
    [Serializable]
    public class Traveler
    {
        public int TravelerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}