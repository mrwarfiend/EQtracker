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
    public class DamagedItemsController : Controller
    {
        private readonly ModelsContext _context;
        private readonly IHttpContextAccessor _contextAccessor;

        public DamagedItemsController(ModelsContext context, IHttpContextAccessor accessor)
        {
            _context = context;
            _contextAccessor = accessor;
        }

        // GET: DamagedItems
        public async Task<IActionResult> Index()
        {
            var modelsContext = _context.DamagedItems.Include(d => d.ReturnToInventory).Include(d => d.Tools);
            return View(await modelsContext.ToListAsync());
        }

        //Get Return page
        public async Task<IActionResult> Return(int? id)
        {
            if (id == null || _context.DamagedItems == null)
            {
                return NotFound();
            }

            var DamagedItem = await _context.DamagedItems.FindAsync(id);
            //check if stuff exists

            if (DamagedItem == null) { return NotFound(); }


            //Console.WriteLine("this is line 5  "  + " \n");
            //, rentorInventory.toolId
            ViewData["toolId"] = new SelectList(_context.Tools, "id", "name");
            tool? t = await _context.Tools.FindAsync(DamagedItem.toolId);
            if (t != null)
            {

                //return NotFound(); }

                //Console.WriteLine("DamagedItem.toolId is " + DamagedItem.toolId + " \n");
                //Console.WriteLine("DamagedItems.InventoryId is " + DamagedItem.InventoryId + " \n");
                //Ok, so these command lines get the right stuff. But in the next block everything goes to hell.
                if (t.name != null)
                {
                    ViewData["tool"] = t.name;
                    return View(DamagedItem);
                }
            }
            return NotFound();
        }

        public IActionResult ReturnFunc(DamagedItem dt)
        {
            if (dt.AdminId != "NONE") {
                //Console.WriteLine("Here is NOne  " + " \n");
                //Console.WriteLine("dt.AdminId  is  " + dt.AdminId + " \n");
                return RedirectToAction("index");

            }

            ReturnTicket rt = new ReturnTicket();
            rt.TimeStamp = DateTime.Now;
            rt.toolID = dt.toolId;

            rt.repairNeeded = dt.repairNeeded;
            rt.InventoryId2 = dt.InventoryId;
            rt.Condition = "Good";
            /*
            switch (dt.check)
            {
                case true:
                    rt.Condition = "Bad";
                    break;
                case false:
                    rt.Condition = "Good";
                    break;
                default:
                    rt.Condition = "Good";
                    break;
            }*/
            //////////////////////////////////
            //if admin id == NONE

            rt.userEmail = _contextAccessor.HttpContext.User.Identity.Name;


            bool sendReturnTicket = false;

            bool checkInventoryExists = true;
            if (dt.InventoryId != null && dt.InventoryId != 0)
            {
                //FindAsync not used                    
                int newInvId = (int)dt.InventoryId;
                inventory? checkInventory2 = _context.Inventories.Find(newInvId);
                if (checkInventory2 == null)
                {

                    checkInventoryExists = false;
                    //return NotFound();

                }

            }

            if (dt.InventoryId == 0 || dt.InventoryId == null)
            {

                inventory inv = _context.Inventories.Where(e => e.toolID == dt.toolId).First();
                inv.Count++;
                _context.Inventories.Update(inv);
                _context.SaveChanges();


                DamagedItem inv2 = _context.DamagedItems.Where(e => e.id == dt.id).First();
                inv2.AdminId = _contextAccessor.HttpContext.User.Identity.Name; 
                inv2.timeStamp2 = DateTime.Now;
                _context.DamagedItems.Update(inv2);
                _context.SaveChanges();
                sendReturnTicket = true;
            }
            else if (checkInventoryExists == false)
            {
                //Will need to check if inventoryId exists.
                sendReturnTicket = false;
                return RedirectToAction("index");
            }
            else
            {
                //for anything else
                //inventory inv = _context.Inventories.Where(e => e.toolID == ri.toolId && e.id == ri.InventoryId).First();
                inventory inv = _context.Inventories.Where(e => e.id == dt.InventoryId).First();
                inv.Count++;
                _context.Inventories.Update(inv);
                _context.SaveChanges();
                sendReturnTicket = true;

                DamagedItem inv2 = _context.DamagedItems.Where(e => e.id == dt.id).First();
                inv2.AdminId = _contextAccessor.HttpContext.User.Identity.Name;
                inv2.timeStamp2 = DateTime.Now;
                _context.DamagedItems.Update(inv2);
                _context.SaveChanges();
                //sendReturnTicket = true;
            }

            if (sendReturnTicket == true)
            {
                //Now Sends the return on valud executions
                _context.Returns.Add(rt);
                _context.SaveChanges();
            }

            //_context.RentorInventories.Remove(dt);
            //_context.SaveChanges();
            return RedirectToAction("index");

        }


            // GET: DamagedItems/Details/5
    public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DamagedItems == null)
            {
                return NotFound();
            }

            var damagedItem = await _context.DamagedItems
                .Include(d => d.ReturnToInventory)
                .Include(d => d.Tools)
                .FirstOrDefaultAsync(m => m.id == id);
            if (damagedItem == null)
            {
                return NotFound();
            }

            return View(damagedItem);
        }

        // GET: DamagedItems/Create
        public IActionResult Create()
        {
            ViewData["InventoryId"] = new SelectList(_context.Inventories, "id", "name");
            ViewData["toolId"] = new SelectList(_context.Tools, "id", "name");
            return View();
        }

        // POST: DamagedItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,userId,toolId,timeStamp,Condition,repairNeeded,InventoryId,AdminId,timeStamp2")] DamagedItem damagedItem)
        {
            if (ModelState.IsValid)
            {
                damagedItem.userId = _contextAccessor.HttpContext.User.Identity.Name; 
                damagedItem.timeStamp = DateTime.Now;
                _context.Add(damagedItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["InventoryId"] = new SelectList(_context.Inventories, "id", "name", damagedItem.InventoryId);
            ViewData["toolId"] = new SelectList(_context.Tools, "id", "name", damagedItem.toolId);
            return View(damagedItem);
        }

        // GET: DamagedItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DamagedItems == null)
            {
                return NotFound();
            }

            var damagedItem = await _context.DamagedItems.FindAsync(id);
            if (damagedItem == null)
            {
                return NotFound();
            }
            ViewData["InventoryId"] = new SelectList(_context.Inventories, "id", "name", damagedItem.InventoryId);
            ViewData["toolId"] = new SelectList(_context.Tools, "id", "name", damagedItem.toolId);
            return View(damagedItem);
        }

        // POST: DamagedItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,userId,toolId,timeStamp,Condition,repairNeeded,InventoryId,AdminId,timeStamp2")] DamagedItem damagedItem)
        {
            if (id != damagedItem.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    damagedItem.userId = _contextAccessor.HttpContext.User.Identity.Name;
                    damagedItem.timeStamp = DateTime.Now;
                    _context.Update(damagedItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DamagedItemExists(damagedItem.id))
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
            ViewData["InventoryId"] = new SelectList(_context.Inventories, "id", "name", damagedItem.InventoryId);
            ViewData["toolId"] = new SelectList(_context.Tools, "id", "name", damagedItem.toolId);
            return View(damagedItem);
        }

        // GET: DamagedItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DamagedItems == null)
            {
                return NotFound();
            }

            var damagedItem = await _context.DamagedItems
                .Include(d => d.ReturnToInventory)
                .Include(d => d.Tools)
                .FirstOrDefaultAsync(m => m.id == id);
            if (damagedItem == null)
            {
                return NotFound();
            }

            return View(damagedItem);
        }

        // POST: DamagedItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DamagedItems == null)
            {
                return Problem("Entity set 'ModelsContext.DamagedItems'  is null.");
            }
            var damagedItem = await _context.DamagedItems.FindAsync(id);
            if (damagedItem != null)
            {
                _context.DamagedItems.Remove(damagedItem);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DamagedItemExists(int id)
        {
          return (_context.DamagedItems?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
