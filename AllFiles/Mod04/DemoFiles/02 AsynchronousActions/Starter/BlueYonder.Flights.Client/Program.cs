
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BlueYonder.Flights.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Run Client Application");
            var directoryPath = Environment.CurrentDirectory + @"\Image\orderedList7.png";
            string url = "http://localhost:5000/api/passenger/updatephoto";

            using (FileStream imageFile = File.OpenRead(directoryPath))
            {
                HttpResponseMessage httpResponse = await UploadImageAsync(imageFile, url);
                Console.WriteLine($"Http Respone");
                Console.WriteLine($"IsSuccess = {httpResponse.IsSuccessStatusCode}");
                Console.WriteLine($"Status Code = {httpResponse.StatusCode}");
            }
        }

        public static async Task<HttpResponseMessage> UploadImageAsync(Stream image,string url)
        {
            var requestContent = new MultipartFormDataContent();
            var imageContent = new StreamContent(image);
            imageContent.Headers.ContentType =
                MediaTypeHeaderValue.Parse("image/jpeg");
            requestContent.Add(imageContent, "file", "image.jpg");
            using (var client = new HttpClient())
            {
                return await client.PutAsync(url, requestContent);
            }
        }
    }
}
