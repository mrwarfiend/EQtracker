using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Authorization;
using EQtrack.Models;
//using Microsoft.AspNetCore.Authorization;

namespace EQtrack.Controllers
{

    [Authorize]
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
            List<RentorInventory>? list = new List<RentorInventory>();
            foreach (RentorInventory? C in _context.RentorInventories.Include(e => e.Tools).ToList())
            {
                //prod to Tools
                //parcess trhough users, and returns current users selection
                RentorInventory? test = C;
                if (test != null)
                {

                    IHttpContextAccessor _contextAccessor1 = _contextAccessor;
                    if (_contextAccessor1 != null)
                    {
                        if (_contextAccessor1.HttpContext != null &&
                            _contextAccessor1.HttpContext.User != null &&
                            _contextAccessor1.HttpContext.User.Identity != null &&
                            _contextAccessor1.HttpContext.User.Identity.Name != null) {
                            //yes, the above checks are nessisary in order to check for null, and get the warning to stop showing up
                            if (C.userId == _contextAccessor1.HttpContext.User.Identity.Name)
                            {
                                list.Add(C);
                                ViewBag.quantity += C.count;
                            }
                        }

                    }
                }
            }

            return View(list);

            /*
            var modelsContext = _context.RentorInventories.Include(r => r.Tools);
            return View(await modelsContext.ToListAsync());
            */


        }

        //Get Return page
        public async Task<IActionResult> Return(int? id)
        {
            //Console.WriteLine("id is " + id + " \n");
            if (id == null || _context.RentorInventories == null)
            {
                return NotFound();
            }

            //Console.WriteLine("this is line 1  " + " \n");

            var rentorInventory = await _context.RentorInventories.FindAsync(id);
            //check if stuff exists
            //Console.WriteLine("this is line 2  " + " \n");

            if (rentorInventory == null) { return NotFound(); }
            //Console.WriteLine("this is line 3  " + " \n");
            //if (rentorInventory.Tools == null) { return NotFound(); }
            //Console.WriteLine("this is line 4  " + " \n");

            //if (rentorInventory.Tools.name != null) { }
            //else            { return NotFound(); }

            //Console.WriteLine("this is line 5  "  + " \n");
            //, rentorInventory.toolId
            ViewData["toolId"] = new SelectList(_context.Tools, "id", "name");
            tool? t = await _context.Tools.FindAsync(rentorInventory.toolId);
            if (t != null)
            {
                
                //return NotFound(); }


                //Console.WriteLine("rentorInventory.toolId is " + rentorInventory.toolId + " \n");
                //Console.WriteLine("rentorInventory.InventoryId is " + rentorInventory.InventoryId + " \n");
                //Ok, so these command lines get the right stuff. But in the next block everything goes to helll.
                if (t.name != null)
                {
                    ViewData["tool"] = t.name;
                    return View(rentorInventory);
                }
            }
            return NotFound();
        }

        public IActionResult ReturnFunc(RentorInventory ri)
        {
                ReturnTicket rt = new ReturnTicket();
                rt.TimeStamp = DateTime.Now;
                rt.toolID = ri.toolId;
                //Console.WriteLine("Rt.toolID is " + rt.toolID + " \n");
                //Console.WriteLine("ri.check is   " + ri.check + " \n");
                rt.repairNeeded = ri.check;
                //Console.WriteLine("rt.repairNeeded is   " + rt.repairNeeded + " \n");

                //Adds return ticket.
                rt.InventoryId2 = ri.InventoryId;
                //Console.WriteLine("ri.InventoryId is " + ri.InventoryId + " \n");
                //Console.WriteLine("rt.InventoryId2 is " + rt.InventoryId2 + " \n");

                switch (ri.check)
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
                }
                 Console.WriteLine("again ri.check is   " + ri.check + " \n");
 
                //if (ri.InventoryId == null) { Console.WriteLine("ri.InventoryId is null" + " \n"); }
                //if (rt.InventoryId2 == null){ Console.WriteLine("rt.InventoryId2 is null" + " \n"); }

                rt.userEmail = _contextAccessor.HttpContext.User.Identity.Name;
                

                bool sendReturnTicket = false;



                //This needs to be added to.
                //Will send the item to claims for repair.
                //Console.WriteLine("again gain, ri.check is   " + ri.check + " \n");
                if (ri.check)
                {

                    /*
                     bool checkInventoryExists = true;
                    //checks for the inventoryId, wether it is valud or even exists.
                    if (ri.InventoryId != null)
                    {
                    int newInvId = (int)ri.InventoryId;
                    inventory? checkInventory2 = _context.Inventories.Find(newInvId);
                    if (checkInventory2 == null) {checkInventoryExists = false;}

                    }

                    //If inventory does not exist, or never existed, return to rentorinventory index. 
                    if (checkInventoryExists == false)
                    {
                    sendReturnTicket = false;
                    return RedirectToAction("index");
                    }
                    */
                    //You know what, allow the rentor to send items with non exist inventores is fine, lets
                    //the admins figure out what to do with it their instead of having to searh through all the possible
                    //rentor inventories instead.

                    DamagedItem dt = new DamagedItem();
                    //userid
                    dt.userId = _contextAccessor.HttpContext.User.Identity.Name;
                    dt.toolId = ri.toolId;
                    dt.timeStamp = DateTime.Now;

                    dt.Condition = "Bad";
                    dt.repairNeeded = ri.check;
                    dt.InventoryId = ri.InventoryId;
                    dt.AdminId = "NONE";



                    //this is needed because i added foreign key to the Damaageditems.invenoryid
                    //Meaning instead of item segfaults.
                    int? newInvId = dt.InventoryId;
                    //Returns null if not found
                    inventory? checkInventory2 = _context.Inventories.Find(newInvId);
                        if (checkInventory2 == null)
                        {

                        //checkInventoryExists = false;
                        //return NotFound();
                        //return RedirectToAction("index");
                        inventory inv = _context.Inventories.Where(e => e.toolID == ri.toolId).First();
                        dt.InventoryId = inv.id;

                        }

                    _context.DamagedItems.Add(dt);
                    _context.SaveChanges();


                    //_context.Claims.Update(inv);
                    //_context.SaveChanges();
                    rt.repairNeeded = ri.check;
                    //sendReturnTicket = true;
                    //return ticket.
                    _context.Returns.Add(rt);
                    _context.SaveChanges();

                }
                else
                {


                    //too doo
                    bool checkInventoryExists = true;


                    if (ri.InventoryId != null&& ri.InventoryId != 0)
                    {
                        //FindAsync not used                    
                        int newInvId = (int)ri.InventoryId;
                        inventory? checkInventory2 = _context.Inventories.Find(newInvId);
                        if (checkInventory2 == null)
                        {

                            checkInventoryExists = false;
                            //return NotFound();

                        }

                    }


                    //Defaults to first match in case of  0 or null in the database which do not exist.
                    if (ri.InventoryId == 0 || ri.InventoryId == null)
                    {

                        inventory inv = _context.Inventories.Where(e => e.toolID == ri.toolId).First();
                        inv.Count++;
                        _context.Inventories.Update(inv);
                        _context.SaveChanges();
                        sendReturnTicket = true;
                    }
                    else if (checkInventoryExists == false)
                    {
                        //Will need to check if inventoryId exists.
                        sendReturnTicket = false;
                        return RedirectToAction("index");
                    }
                    else {
                        //for anything else
                        //inventory inv = _context.Inventories.Where(e => e.toolID == ri.toolId && e.id == ri.InventoryId).First();
                        inventory inv = _context.Inventories.Where(e => e.id == ri.InventoryId).First();
                        inv.Count++;
                        _context.Inventories.Update(inv);
                        _context.SaveChanges();
                        sendReturnTicket = true;
                    }


                    if (sendReturnTicket == true) {
                        //Now Sends the return on valud executions
                        _context.Returns.Add(rt);
                        _context.SaveChanges();
                    }





                }


                _context.RentorInventories.Remove(ri);
                _context.SaveChanges();
                return RedirectToAction("index");
            }
            /////////////////////////////////////////////////////

            //products to 
            //Shopping -> renting
            //nether this nor the corrisponding views seems needed.
            //removed async from  the declaration async Task<IActionResult>
            //added IActionResult to declaration
            public IActionResult Renting()
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

            //Listed as shoping search in renting views, both this and the function are not yet complete
            //asyinc from declaration, not like this function is ever getting called, or is even working yet.
            public IActionResult RentingSearch(tool p)
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
            public async Task<IActionResult> Create([Bind("id,userId,toolId,count,timeStamp,check,InventoryId")] RentorInventory rentorInventory)
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
            [Authorize(Roles = "admin")]
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
            [Authorize(Roles = "admin")]
            public async Task<IActionResult> Edit(int id, [Bind("id,userId,toolId,count,timeStamp,check,InventoryId")] RentorInventory rentorInventory)
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
            [Authorize(Roles = "admin")]
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
            [Authorize(Roles = "admin")]
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

