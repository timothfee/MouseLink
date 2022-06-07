using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCWebAPP.Data;
using MVCWebAPP.Models;

namespace MVCWebAPP.Controllers
{
    public class MouseController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MouseController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<Mouse> mice = new List<Mouse>();
            foreach (var item in _context.Mice)
            {
                if(item.Rank != null)
                {
                    mice.Add(item);
                }
            }
            List<Mouse> sortedList = mice.OrderBy(x => x.Rank).ToList();
            return View(sortedList);

            
        }
    }
}
