using BlueYonder.Hotels.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlueYonder.Hotels.DAL.Repository
{
    public interface IHotelBookingRepository
    {
        IEnumerable<Room> GetAvaliabileByDate(DateTime date);
        IEnumerable<Reservation> GetAllReservation();
        Task DeleteReservation(int reservationId);
    }
}
