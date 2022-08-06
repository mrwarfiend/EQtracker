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
    public class inventoriesController : Controller
    {
        private readonly ModelsContext _context;

        private readonly IHttpContextAccessor _contextAccessor;

        public inventoriesController(ModelsContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _contextAccessor = contextAccessor;
        }

        // GET: inventories
        public async Task<IActionResult> Index()
        {
            var modelsContext = _context.Inventories.Include(i => i.Tool);
            return View(await modelsContext.ToListAsync());
        }

        public async Task<IActionResult> Checkout()
        {
            var modelsContext = _context.Inventories.Include(i => i.Tool);
            return View(await modelsContext.ToListAsync());
        }
        public async Task<IActionResult> CheckoutFunc(int? id)
        {
            if (id == null || _context.Inventories == null)
            {
                return NotFound();
            }

            var inventory = await _context.Inventories.FindAsync(id);
            if (inventory == null)
            {
                return NotFound();
            }
            ViewData["toolID"] = new SelectList(_context.Tools, "id", "name", inventory.toolID);
            return View(inventory);
        }

        public IActionResult CheckoutFunction(inventory prod)
        {
                Console.WriteLine(prod.Count);
            if (prod.Count > 0)
            {

                Ticket ticket = new Ticket();
                ticket.toolID = prod.toolID;
                ticket.userEmail = _contextAccessor.HttpContext.User.Identity.Name;
                ticket.TimeStamp = DateTime.Now;
                _context.Tickets.Add(ticket);
                _context.SaveChanges();

                RentorInventory ri = new RentorInventory();
                ri.userId = _contextAccessor.HttpContext.User.Identity.Name;
                ri.toolId = prod.toolID;
                ri.count = 1;
                ri.timeStamp = DateTime.Now;
                ri.check = false;
                _context.RentorInventories.Add(ri);
                _context.SaveChanges();

                prod.Count--;
                _context.Update(prod);
                _context.SaveChanges();

            }


            var modelsContext = _context.Inventories.Include(i => i.Tool);
            return RedirectToAction("Checkout");
        }

        // GET: inventories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Inventories == null)
            {
                return NotFound();
            }

            var inventory = await _context.Inventories
                .Include(i => i.Tool)
                .FirstOrDefaultAsync(m => m.id == id);
            if (inventory == null)
            {
                return NotFound();
            }

            return View(inventory);
        }

        // GET: inventories/Create
        public IActionResult Create()
        {
            ViewData["toolID"] = new SelectList(_context.Tools, "id", "name");
            return View();
        }

        // POST: inventories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,name,toolID,Count,flag")] inventory inventory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inventory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["toolID"] = new SelectList(_context.Tools, "id", "name", inventory.toolID);
            return View(inventory);
        }

        // GET: inventories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Inventories == null)
            {
                return NotFound();
            }

            var inventory = await _context.Inventories.FindAsync(id);
            if (inventory == null)
            {
                return NotFound();
            }
            ViewData["toolID"] = new SelectList(_context.Tools, "id", "name", inventory.toolID);
            return View(inventory);
        }

        // POST: inventories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,name,toolID,Count,flag")] inventory inventory)
        {
            if (id != inventory.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inventory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!inventoryExists(inventory.id))
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
            ViewData["toolID"] = new SelectList(_context.Tools, "id", "name", inventory.toolID);
            return View(inventory);
        }

        // GET: inventories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Inventories == null)
            {
                return NotFound();
            }

            var inventory = await _context.Inventories
                .Include(i => i.Tool)
                .FirstOrDefaultAsync(m => m.id == id);
            if (inventory == null)
            {
                return NotFound();
            }

            return View(inventory);
        }

        // POST: inventories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Inventories == null)
            {
                return Problem("Entity set 'ModelsContext.Inventories'  is null.");
            }
            var inventory = await _context.Inventories.FindAsync(id);
            if (inventory != null)
            {
                _context.Inventories.Remove(inventory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool inventoryExists(int id)
        {
            return (_context.Inventories?.Any(e => e.id == id)).GetValueOrDefault();
        }

        public JsonResult FetchCount(int cid)
        {

            //var count = _context.inventory.Where(e => e.categID == cid).Count();
            //var count = _context.Inventories.Where(_context => _context.Id == cid).Count();
            //var count = _context.Inventories?.Count;
            var count = 10;
            //Include(p => p.categ).Where(p => p.categID == id)
            //var count = _context.Inventories?.Where(p => p.id == cid)                .Select(inventory.Count);
            //  var count = new select _context.Inventories
            //Console.WriteLine("Count is " + Count);
            return Json(count);

        }

    }
}
