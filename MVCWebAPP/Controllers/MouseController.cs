using Microsoft.AspNetCore.Mvc;

namespace MVCWebAPP.Controllers
{
    public class MouseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
