using Microsoft.AspNetCore.Mvc;

namespace DemoBlogCore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
