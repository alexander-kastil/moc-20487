using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DemoController.Controllers {
    [Route ("api/[controller]")]
    // [Authorize]
    public class DemoController : ControllerBase {
        public DemoController () { }

        // GET http://localhost:5000/api/demo
        [AllowAnonymous]
        [HttpGet ("")]
        public ActionResult<string> Gets () {
            return "No Auth needed here";
        }

        // GET http://localhost:5000/api/demo/getSecure
        [HttpGet ("getSecure")]
        public ActionResult<string> GetSecure () {
            return "Auth needed here";
        }

    }
}