using Microsoft.AspNetCore.Mvc;

namespace ProLab.Areas.XLab.Controllers
{   
    [Area("XLab")]
    public class XLabController : Controller
    {
        public IActionResult IndexXLab()
        {
            return View();
        }
    }
}