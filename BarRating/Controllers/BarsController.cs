using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using BarRating.Data;
using BarRating.Models;

namespace BarRating.Controllers
{
    [Authorize]
    public class BarsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BarsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Bars
        public async Task<IActionResult> Index()
        {
            return _context.Bar != null ?
                        View(await _context.Bar.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Bar'  is null.");
        }

        // GET: Bars
        public async Task<IActionResult> IndexSearch(string name)
        {
            return _context.Bar != null ?
                        View(await _context.Bar.Where(x => x.Name == name).ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Bar'  is null.");
        }

        // GET: Bars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Bar == null)
            {
                return NotFound();
            }

            var bar = await _context.Bar
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bar == null)
            {
                return NotFound();
            }

            return View(bar);
        }

        // GET: Bars/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bars/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,ImageUrl")] Bar bar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bar);
        }

        // GET: Bars/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Bar == null)
            {
                return NotFound();
            }

            var bar = await _context.Bar.FindAsync(id);
            if (bar == null)
            {
                return NotFound();
            }
            return View(bar);
        }

        // POST: Bars/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,ImageUrl")] Bar bar)
        {
            if (id != bar.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BarExists(bar.Id))
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
            return View(bar);
        }

        // GET: Bars/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Bar == null)
            {
                return NotFound();
            }

            var bar = await _context.Bar
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bar == null)
            {
                return NotFound();
            }

            return View(bar);
        }

        // POST: Bars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Bar == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Bar'  is null.");
            }
            var bar = await _context.Bar.FindAsync(id);
            if (bar != null)
            {
                _context.Bar.Remove(bar);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BarExists(int id)
        {
            return (_context.Bar?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
