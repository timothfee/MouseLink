using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCWebAPP.Data;
using MVCWebAPP.Models;

namespace MVCWebAPP.Controllers
{
    public class MouseController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<MouseUser> _userManager;

        public MouseController(ApplicationDbContext context, UserManager<MouseUser> userManager )
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            List<Mouse> mice = new List<Mouse>();
            foreach (var item in _context.Mice.Include(m => m.userVote))
            {
                if(item.Rank != null)
                {
                    mice.Add(item);
                }
            }
            List<Mouse> sortedList = mice.OrderBy(x => x.Rank).ToList();
            return View(sortedList);

            
        }
        public async Task<IActionResult> FavoriteMice(MouseSearchViewModel model)
        {
            List<Mouse> mice = new List<Mouse>();

            foreach (var item in _context.Mice)
            {
                mice.Add(item);
            }
            string userId = _userManager.GetUserId(User);
            var mouseUser = _context.Users.Include(m => m.favoriteMice).First(m => m.Id == userId);
            ViewData["mouseUserLoggedIn"] = mouseUser;


            
            return View(mouseUser.favoriteMice.OrderBy(m => m.Rank));
        }
    }
}
