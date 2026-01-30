using Microsoft.AspNetCore.Mvc;

namespace ProLab.Areas.HockeyData.Controllers
{
    [Area("HockeyData")]
    public class HomeController : Controller
    {
        public IActionResult IndexHockeyData()
        {
            return View();
        }
    }
}
