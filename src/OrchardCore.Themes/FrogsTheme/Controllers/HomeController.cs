using Microsoft.AspNetCore.Mvc;

namespace FrogsNetwork.FrogsTheme.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
