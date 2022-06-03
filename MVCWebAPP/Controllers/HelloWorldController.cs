using Microsoft.AspNetCore.Mvc;

namespace MVCWebAPP.Controllers
{
    public class HelloWorldController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
