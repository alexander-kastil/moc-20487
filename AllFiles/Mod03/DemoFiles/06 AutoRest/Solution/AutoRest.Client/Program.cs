using System;
using System.Collections.Generic;
using AutoRest.Sdk;
using AutoRest.Sdk.Models;

namespace AutoRest.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            MyAPI client = new MyAPI(new Uri("http://localhost:5000"));
            IList<Destination> destinationList = client.ApiDestinationsGet();

            Console.WriteLine("All Destination");
            foreach (Destination destination in destinationList)
            {
                Console.WriteLine($"{destination.CityName} - {destination.Airport}");
            }
            // ReadKey used that the console will not close when the code end to run.
            Console.ReadKey();
        }
    }
}
