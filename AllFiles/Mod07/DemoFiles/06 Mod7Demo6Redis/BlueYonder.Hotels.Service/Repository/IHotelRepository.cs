using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueYonder.Hotels.Service.Repository
{
    public interface IHotelRepository
    {
        List<string> GetHotelList();
        void AddHotel(string hotelName);
    }
}
