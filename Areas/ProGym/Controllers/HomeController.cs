using Microsoft.AspNetCore.Mvc;

namespace ProLab.Areas.ProGym.Controllers
{
    [Area("ProGym")]
    public class HomeController : Controller
    {
        public IActionResult IndexProGym()
        {
            return View();
        }
        public IActionResult Links()
        {
            return View();
        }
    }
}
