using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using HttpClientApplication.Host.Models;
using System.Runtime.Serialization.Json;

namespace HttpClientApplication.Client
{
    class Program
    {
        static  async Task Main(string[] args)
        {
            using (var client = new HttpClient())
            {
                Console.WriteLine("Respone data as JSON");
                HttpResponseMessage message = await client.GetAsync("http://localhost:5000/api/destinations");
                string resultAsString = await message.Content.ReadAsStringAsync();
                Console.WriteLine(resultAsString);

                List<Destination> destinationsResult = await message.Content.ReadAsAsync<List<Destination>>();
                Console.WriteLine("\nAll Destination");
                foreach (Destination destination in destinationsResult)
                {
                    Console.WriteLine($"{destination.CityName} - {destination.Airport}");
                }
                // ReadKey used that the console will not close when the code end to run.
                Console.ReadKey();
            }
        }

       
    }
}
