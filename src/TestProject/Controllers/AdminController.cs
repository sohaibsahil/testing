using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TestProject.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Widgets()
        {
            return View();

        }
        public IActionResult Charts()
        {
            return View();
        }
        public IActionResult Forms()
        {
            return View();
        }
    }
}