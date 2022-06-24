using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCWebAPP.Data;
using MVCWebAPP.Models;
using MVCWebAPP.Services.Interfaces;

namespace MVCWebAPP.Controllers
{
    public class MiceController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMouseService _mouse;
        private readonly UserManager<MouseUser> _userManager;


        public MiceController(ApplicationDbContext context, IMouseService mouse, UserManager<MouseUser> userManager)
        {
            _context = context;
            _mouse = mouse;
            _userManager = userManager;
        }

        // GET: Mice
        public async Task<IActionResult> Index(MouseSearchViewModel model)
        {
            if (model.Shape == "Any Shape")
            {
                model.Shape = null;
            }
            List<Mouse> mice = await _mouse.GetMiceByPreference(model);

            var mouseUser = await _userManager.GetUserAsync(User);
            ViewData["mouseUserLoggedIn"] = mouseUser;


            return View(mice);
        }
        //This portion of the code is the controller for the voting feature on the website.
        [Authorize]
        public async Task<IActionResult> Favorite(int id)
        {

            var mouse = await _context.Mice.FindAsync(id);
            if (mouse == null)
            {
                return NotFound();
            }
            return View(mouse);
        }
        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Favorite")]
        [Authorize]
        public async Task<IActionResult> PostFavorite(int id)
        {
            var mouseUser = await _userManager.GetUserAsync(User);
            var mouse = _context.Mice.Include(m => m.userVote).Where(m => m.Id == id).First();
            try
            {
                if (mouse.userVote.Contains(mouseUser))
                {
                    mouse.userVote.Remove(mouseUser);
                }
                else
                {
                    mouse.userVote.Add(mouseUser);

                }
                _context.Update(mouse);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MouseExists(mouse.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Mice/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Mice == null)
            {
                return NotFound();
            }

            var mouse = await _context.Mice
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mouse == null)
            {
                return NotFound();
            }

            return View(mouse);
        }

        // GET: Mice/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Mice/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Shape,Weight,IsWireless,Rank,Size,URL")] Mouse mouse)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mouse);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mouse);
        }

        // GET: Mice/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Mice == null)
            {
                return NotFound();
            }

            var mouse = await _context.Mice.FindAsync(id);
            if (mouse == null)
            {
                return NotFound();
            }
            return View(mouse);
        }

        // POST: Mice/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Shape,Weight,IsWireless,Rank,Size,URL")] Mouse mouse)
        {
            if (id != mouse.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mouse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MouseExists(mouse.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(mouse);
        }

        // GET: Mice/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Mice == null)
            {
                return NotFound();
            }

            var mouse = await _context.Mice
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mouse == null)
            {
                return NotFound();
            }

            return View(mouse);
        }

        // POST: Mice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Mice == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Mice'  is null.");
            }
            var mouse = await _context.Mice.FindAsync(id);
            if (mouse != null)
            {
                _context.Mice.Remove(mouse);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MouseExists(int id)
        {
            return (_context.Mice?.Any(e => e.Id == id)).GetValueOrDefault();
        }


    }
}