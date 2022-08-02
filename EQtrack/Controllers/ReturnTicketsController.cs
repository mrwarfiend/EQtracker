using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EQtrack.Models;

namespace EQtrack.Controllers
{
    public class ReturnTicketsController : Controller
    {
        private readonly ModelsContext _context;

        public ReturnTicketsController(ModelsContext context)
        {
            _context = context;
        }

        // GET: ReturnTickets
        public async Task<IActionResult> Index()
        {
            var modelsContext = _context.Returns.Include(r => r.Tool2);
            return View(await modelsContext.ToListAsync());
        }

        // GET: ReturnTickets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Returns == null)
            {
                return NotFound();
            }

            var returnTicket = await _context.Returns
                .Include(r => r.Tool2)
                .FirstOrDefaultAsync(m => m.id == id);
            if (returnTicket == null)
            {
                return NotFound();
            }

            return View(returnTicket);
        }

        // GET: ReturnTickets/Create
        public IActionResult Create()
        {
            ViewData["toolID"] = new SelectList(_context.Tools, "id", "name");
            return View();
        }

        // POST: ReturnTickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,TimeStamp,toolID,Condition,repairNeeded")] ReturnTicket returnTicket)
        {
            if (ModelState.IsValid)
            {
                _context.Add(returnTicket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["toolID"] = new SelectList(_context.Tools, "id", "name", returnTicket.toolID);
            return View(returnTicket);
        }

        // GET: ReturnTickets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Returns == null)
            {
                return NotFound();
            }

            var returnTicket = await _context.Returns.FindAsync(id);
            if (returnTicket == null)
            {
                return NotFound();
            }
            ViewData["toolID"] = new SelectList(_context.Tools, "id", "name", returnTicket.toolID);
            return View(returnTicket);
        }

        // POST: ReturnTickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,TimeStamp,toolID,Condition,repairNeeded")] ReturnTicket returnTicket)
        {
            if (id != returnTicket.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(returnTicket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReturnTicketExists(returnTicket.id))
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
            ViewData["toolID"] = new SelectList(_context.Tools, "id", "name", returnTicket.toolID);
            return View(returnTicket);
        }

        // GET: ReturnTickets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Returns == null)
            {
                return NotFound();
            }

            var returnTicket = await _context.Returns
                .Include(r => r.Tool2)
                .FirstOrDefaultAsync(m => m.id == id);
            if (returnTicket == null)
            {
                return NotFound();
            }

            return View(returnTicket);
        }

        // POST: ReturnTickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Returns == null)
            {
                return Problem("Entity set 'ModelsContext.Returns'  is null.");
            }
            var returnTicket = await _context.Returns.FindAsync(id);
            if (returnTicket != null)
            {
                _context.Returns.Remove(returnTicket);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReturnTicketExists(int id)
        {
          return (_context.Returns?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
