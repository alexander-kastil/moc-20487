using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueYonder.Hotels.Service.Repository
{
    public class HotelRepository : IHotelRepository
    {
        private List<string> _hotelList;

        public HotelRepository()
        {
            _hotelList = new List<string>();

            for (int i = 1; i <= 10; i++)
            {
                _hotelList.Add($"Hotel {i}");
            }
        }

        public void AddHotel(string hotelName)
        {
            _hotelList.Add(hotelName);
        }

        public List<string> GetHotelList()
        {
            return _hotelList;
        }
    }
}
