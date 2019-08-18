using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using CustomFiltersAndFormatters.Models;
using Microsoft.AspNetCore.Mvc;

namespace CustomFiltersAndFormatters.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassengerController : ControllerBase
    {
        private Value[] _values = new Value[] {
            new Value{Id =0, Name = "Zero", Thumbnail=@"Images\orderedlist0.png"},
            new Value{Id =1, Name = "One", Thumbnail=@"Images\orderedlist1.png"},
            new Value{Id =2, Name = "Two", Thumbnail=@"Images\orderedlist2.png"},
            new Value{Id =3, Name = "Three", Thumbnail=@"Images\orderedlist3.png"},
            new Value{Id =4, Name = "Four", Thumbnail=@"Images\orderedlist4.png"},
            new Value{Id =5, Name = "Five", Thumbnail=@"Images\orderedlist5.png"},
            new Value{Id =6, Name = "Six", Thumbnail=@"Images\orderedlist6.png"},
            new Value{Id =7, Name = "Seven", Thumbnail=@"Images\orderedlist7.png"},
            new Value{Id =8, Name = "Eight", Thumbnail=@"Images\orderedlist8.png"},
            new Value{Id =9, Name = "Nine", Thumbnail=@"Images\orderedlist9.png"}
        };


        [HttpGet("Photo/{id}")]
        public Value GetPhoto(int id)
        {
            return _values[id];
        }
  }
}
