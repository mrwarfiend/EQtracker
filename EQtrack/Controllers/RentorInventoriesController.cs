using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Authorization;
using EQtrack.Models;

namespace EQtrack.Controllers
{
    public class RentorInventoriesController : Controller
    {
        private readonly ModelsContext _context;
        //added
        private readonly IHttpContextAccessor _contextAccessor;

        public RentorInventoriesController(ModelsContext context, IHttpContextAccessor accessor)
        {
            _context = context;
            _contextAccessor = accessor;
        }

        // GET: RentorInventories
        [Authorize]
        //public async Task<IActionResult> Index()
        public IActionResult Index()
        {
            
            ViewBag.quantity = 0;
            List<RentorInventory> list = new List<RentorInventory>();
            foreach (RentorInventory C in _context.RentorInventories.Include(e => e.Tools).ToList())
            {
                //prod to Tools
                if (C.userId == _contextAccessor.HttpContext.User.Identity.Name)
                {
                    list.Add(C);
                    //ViewBag.subtotal += MathF.Round(cart.subtotal, 2);
                    //ViewBag.tax += MathF.Round(cart.tax, 2);
                    //ViewBag.total += MathF.Round(cart.total, 2);
                    ViewBag.quantity += C.count;
                }
            }
            
            return View(list);

            /*
            var modelsContext = _context.RentorInventories.Include(r => r.Tools);
            return View(await modelsContext.ToListAsync());
            */


        }

        //products to 
        //Shopping -> renting
        public async Task<IActionResult> Renting()
        {
            //List<tool> prods = new List<tool>();
            List<inventory> prods = new List<inventory>();
            foreach (inventory prod in _context.Inventories.Include(e => e.Tool).ToList())
            {
                foreach (tool inv in _context.Tools.ToList())
                {
                    //prod is inventory , inv is tools
                    if (prod.Tool.id == inv.id)
                    {
                        if (prod.Count > 0)
                        {
                            prods.Add(prod);
                        }
                        //going to need to add some stuff.
                    }
                }
            }
            ViewBag.categ = new SelectList(_context.Inventories.ToList(), "id", "name");
            return View(prods);
        }

        public async Task<IActionResult> RentingSearch(tool p)
        {
            List<tool> prods = new List<tool>();
            foreach (tool prod in _context.Tools.Include(e => e.Categ).ToList())
            {
                foreach (inventory inv in _context.Inventories.ToList())
                {
                    if (prod.id == inv.toolID)
                    {
                        if (inv.Count > 0)
                        {
                            if (p.Categ == prod.Categ)
                            {
                                prods.Add(prod);
                            }
                        }
                    }
                }
            }
            if (prods.Count == 0)
            {
                tool prod = new tool();
                prod.name = "NoneFound";
                prods.Add(prod);
                ViewBag.categ = new SelectList(_context.Categories.ToList(), "id", "name");
                return View(prods);
            }
            ViewBag.categ = new SelectList(_context.Categories.ToList(), "id", "name");
            return View(prods);
        }

        // GET: RentorInventories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.RentorInventories == null)
            {
                return NotFound();
            }

            var rentorInventory = await _context.RentorInventories
                .Include(r => r.Tools)
                .FirstOrDefaultAsync(m => m.id == id);
            if (rentorInventory == null)
            {
                return NotFound();
            }

            return View(rentorInventory);
        }

        // GET: RentorInventories/Create
        public IActionResult Create()
        {
            ViewData["toolId"] = new SelectList(_context.Tools, "id", "name");
            return View();
        }

        // POST: RentorInventories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,userId,toolId,count,timeStamp,check")] RentorInventory rentorInventory)
        {
            if (ModelState.IsValid)
            {
                rentorInventory.timeStamp = DateTime.Now;
                _context.Add(rentorInventory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["toolId"] = new SelectList(_context.Tools, "id", "name", rentorInventory.toolId);
            return View(rentorInventory);
        }

        // GET: RentorInventories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.RentorInventories == null)
            {
                return NotFound();
            }

            var rentorInventory = await _context.RentorInventories.FindAsync(id);
            if (rentorInventory == null)
            {
                return NotFound();
            }
            ViewData["toolId"] = new SelectList(_context.Tools, "id", "name", rentorInventory.toolId);
            return View(rentorInventory);
        }

        // POST: RentorInventories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,userId,toolId,count,timeStamp,check")] RentorInventory rentorInventory)
        {
            if (id != rentorInventory.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    rentorInventory.timeStamp = DateTime.Now;
                    _context.Update(rentorInventory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RentorInventoryExists(rentorInventory.id))
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
            ViewData["toolId"] = new SelectList(_context.Tools, "id", "name", rentorInventory.toolId);
            return View(rentorInventory);
        }

        // GET: RentorInventories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.RentorInventories == null)
            {
                return NotFound();
            }

            var rentorInventory = await _context.RentorInventories
                .Include(r => r.Tools)
                .FirstOrDefaultAsync(m => m.id == id);
            if (rentorInventory == null)
            {
                return NotFound();
            }

            return View(rentorInventory);
        }

        // POST: RentorInventories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.RentorInventories == null)
            {
                return Problem("Entity set 'ModelsContext.RentorInventories'  is null.");
            }
            var rentorInventory = await _context.RentorInventories.FindAsync(id);
            if (rentorInventory != null)
            {
                _context.RentorInventories.Remove(rentorInventory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        //--//
        /**
        public async Task<IActionResult> DeleteMult()
        {
            List<Cart> list = new List<Cart>();
            foreach (Cart cart in _context.RentorInventories.Include(e => e.prod).ToList())
            {
                if (cart.userId == _contextAccessor.HttpContext.User.Identity.Name)
                {
                    list.Add(cart);
                }
            }
            return View(list);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteMult(List<RentorInventory> l)
        {
            foreach (RentorInventory c in l)
            {
                if (c.check == true)
                {
                    _context.Inventories.Remove(c);
                    _context.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }

        //
        */
        private bool RentorInventoryExists(int id)
        {
          return (_context.RentorInventories?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
