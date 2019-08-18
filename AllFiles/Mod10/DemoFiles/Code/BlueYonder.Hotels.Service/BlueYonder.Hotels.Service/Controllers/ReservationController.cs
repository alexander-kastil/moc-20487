using System;
using System.Security.Cryptography;
using System.Text;
using BlueYonder.Hotels.Service.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BlueYonder.Hotels.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "success";
        }

        [HttpPost("sign")]
        public string Sign([FromBody]Reservation value)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(JsonConvert.SerializeObject(value));

            byte[] secretkey = new byte[128];
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(secretkey);
            }

            using (HMACMD5 hmac = new HMACMD5(secretkey))
            {
                return Convert.ToBase64String(hmac.ComputeHash(bytes));
            }
        }
    }
}