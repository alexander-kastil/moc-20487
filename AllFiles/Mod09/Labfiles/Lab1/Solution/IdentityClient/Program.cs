using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace IdentityClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            HttpClient client = new HttpClient();

            HttpResponseMessage response = await client.PostAsync("https://localhost:5001/api/user/register", new StringContent("{ \"Username\": \"azure@azure.com\", \"Password\": \"Azure123!!\" }", Encoding.UTF8, "application/json"));
            string token = await response.Content.ReadAsStringAsync();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            response = await client.GetAsync("https://localhost:5001/api/values");
            string content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(content);
        }
    }
}
