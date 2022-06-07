﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCWebAPP.Data;
using MVCWebAPP.Models;

namespace MVCWebAPP.Controllers
{
    public class MiceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MiceController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Mice
        public async Task<IActionResult> Index()
        {
              return _context.Mice != null ? 
                          View(await _context.Mice.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Mice'  is null.");
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