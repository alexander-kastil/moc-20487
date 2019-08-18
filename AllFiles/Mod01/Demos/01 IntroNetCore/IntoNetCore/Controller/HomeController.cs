using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace IntroNetCore
{
    public class HomeController : Controller
    {
        private AppConfig config { get; set; }

        public HomeController(IOptions<AppConfig> cfg)
        {
            config = cfg.Value;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewBag.AppName = config.ApplicationName;
            return View();
        }
    }
}
