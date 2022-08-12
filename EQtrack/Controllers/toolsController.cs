using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EQtrack.Models;
using Microsoft.AspNetCore.Authorization;

namespace EQtrack.Controllers
{
    [Authorize(Roles = "admin")]
    public class toolsController : Controller
    {
        private readonly ModelsContext _context;

        public toolsController(ModelsContext context)
        {
            _context = context;
        }

        // GET: tools
        public async Task<IActionResult> Index()
        {
            var modelsContext = _context.Tools.Include(t => t.Categ);
            return View(await modelsContext.ToListAsync());
        }

        // GET: tools/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Tools == null)
            {
                return NotFound();
            }

            var tool = await _context.Tools
                .Include(t => t.Categ)
                .FirstOrDefaultAsync(m => m.id == id);
            if (tool == null)
            {
                return NotFound();
            }

            return View(tool);
        }

        // GET: tools/Create
        public IActionResult Create()
        {
            ViewData["categID"] = new SelectList(_context.Categories, "id", "name");
            return View();
        }

        // POST: tools/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,name,categID,flag,desc,image")] tool tool)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tool);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["categID"] = new SelectList(_context.Categories, "id", "name", tool.categID);
            return View(tool);
        }

        public async Task<IActionResult> Upload()
        {
            //effectivivly this just returns to index, and is just a place holder until or even if i get the upload to work
            var modelsContext = _context.Tools.Include(t => t.Categ);
            return View(await modelsContext.ToListAsync());
            //return View();
        }

        // GET: tools/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Tools == null)
            {
                return NotFound();
            }

            var tool = await _context.Tools.FindAsync(id);
            if (tool == null)
            {
                return NotFound();
            }
            ViewData["categID"] = new SelectList(_context.Categories, "id", "name", tool.categID);
            return View(tool);
        }

        // POST: tools/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,name,categID,flag,desc,image")] tool tool)
        {
            if (id != tool.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tool);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!toolExists(tool.id))
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
            ViewData["categID"] = new SelectList(_context.Categories, "id", "name", tool.categID);
            return View(tool);
        }

        // GET: tools/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tools == null)
            {
                return NotFound();
            }

            var tool = await _context.Tools
                .Include(t => t.Categ)
                .FirstOrDefaultAsync(m => m.id == id);
            if (tool == null)
            {
                return NotFound();
            }

            return View(tool);
        }

        // POST: tools/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tools == null)
            {
                return Problem("Entity set 'ModelsContext.Tools'  is null.");
            }
            var tool = await _context.Tools.FindAsync(id);
            if (tool != null)
            {
                _context.Tools.Remove(tool);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool toolExists(int id)
        {
          return (_context.Tools?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
