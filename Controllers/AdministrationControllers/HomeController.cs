using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProLab.Models;
using ProLab.Models.DataModels;
using ProLab.Models.ViewModels;

namespace ProLab.Controllers.AdministrationControllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SIKHockeyCorner()
        {
            return View();
        }
        public IActionResult Cobras()
        {
            return View();
        }
        public IActionResult SRHL()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Hockey()
        {
            return View();
        }
        [Authorize(Roles = "Admin, NBS")]
        public IActionResult DWKLinks()
        {
            return View();
        }

        [Authorize(Roles = "Admin, NBS")]
        public IActionResult NBSLinks()
        {
            return View();
        }
        
        [Authorize(Roles = "Admin, NBS")]
        public IActionResult Tools()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Health()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult HockeyFemman()
        {
            return View();
        }

        [Authorize(Roles = "Admin, NBS")]
        public IActionResult NBS()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Projects()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Links()
        {
            return View();
        }
        [Authorize(Roles = "Peter")]
        public IActionResult Peter()
        {
            return View();
        }        

        [Authorize(Roles = "SuperUser")]
        public IActionResult SuperUser()
        {
            return View();
        }
        [Authorize(Roles = "Daif")]
        public IActionResult Daif()
        {
            return View();
        }
        [Authorize(Roles = "Referee")]
        public IActionResult Referee()
        {
            return View();
        }
        [Authorize(Roles = "Referee, Daif")]
        public IActionResult SIFLinks()
        {
            return View();
        }
        [Authorize(Roles = "Regus")]
        public IActionResult RegusLinks()
        {
            return View();
        }
        [Authorize(Roles = "Regus")]
        public IActionResult Regus()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult FitnessLinks()
        {
            return View();
        }
        [Authorize(Roles = "Anchors")]
        public IActionResult Anchors()
        {
            return View();
        }
        [Authorize(Roles = "Anchors, Daif")]
        public IActionResult AnchorsLinks()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult Cups()
        {
            return View();
        }
        public IActionResult JoleTaxi()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
