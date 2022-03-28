using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mission13DeLange.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Mission13DeLange.Controllers
{
    public class HomeController : Controller
    {
        private IBowlerRepository _context;

        public HomeController(IBowlerRepository temp)
        {
            _context = temp;
        }
        public IActionResult Index()
        {
            return View();
        }

    }
}
