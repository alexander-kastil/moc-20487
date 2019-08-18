using DAL.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlueYonder.Hotels.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage message = await client.GetAsync("http://localhost:5000/api/HotelBooking/1");
                Console.WriteLine("Getting booking:");
                string resultAsString = await message.Content.ReadAsStringAsync();
                Console.WriteLine(resultAsString);

                Booking booking = await message.Content.ReadAsAsync<Booking>();
                booking.Paid = false; 
                
                message = await client.PutAsJsonAsync("http://localhost:5000/api/HotelBooking/1", booking);
                resultAsString = await message.Content.ReadAsStringAsync();
                Console.WriteLine("Booking after update:");
                Console.WriteLine(resultAsString);
            }
        }
    }
}
