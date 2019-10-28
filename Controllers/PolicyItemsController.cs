using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PersonalPropertyApp.Models;

namespace PersonalPropertyApp.Controllers
{
    public class PolicyItemsController : Controller
    {
        private readonly InsuranceAppContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public PolicyItemsController(InsuranceAppContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: PolicyItems
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var currentEmail = user.Email;
            var insuranceAppContext = _context.PolicyItems.Include(p => p.Policy).Include(p => p.User);
            return View(await insuranceAppContext.Where(p => p.User.Email == currentEmail).ToListAsync());
        }

        // GET: PolicyItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var policyItems = await _context.PolicyItems
                .Include(p => p.Policy)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Itemid == id);
            if (policyItems == null)
            {
                return NotFound();
            }

            return View(policyItems);
        }

        // GET: PolicyItems/Create
        public IActionResult Create()
        {
            ViewData["Policyid"] = new SelectList(_context.PolicyDetails, "Policyid", "Policyid");
            ViewData["Userid"] = new SelectList(_context.PolicyHolder, "Userid", "Email");
            return View();
        }

        // POST: PolicyItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Itemid,Itemname,Itemcategory,Itemdescription,Purchaseprice,Purchasedate,Itemimage,Receiptimage,Upc,Userid,Policyid")] PolicyItems policyItems)
        {
            if (ModelState.IsValid)
            {
                _context.Add(policyItems);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Policyid"] = new SelectList(_context.PolicyDetails, "Policyid", "Policyid", policyItems.Policyid);
            ViewData["Userid"] = new SelectList(_context.PolicyHolder, "Userid", "Email", policyItems.Userid);
            return View(policyItems);
        }

        // GET: PolicyItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var policyItems = await _context.PolicyItems.FindAsync(id);
            if (policyItems == null)
            {
                return NotFound();
            }
            ViewData["Policyid"] = new SelectList(_context.PolicyDetails, "Policyid", "Policyid", policyItems.Policyid);
            ViewData["Userid"] = new SelectList(_context.PolicyHolder, "Userid", "Email", policyItems.Userid);
            return View(policyItems);
        }

        // POST: PolicyItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Itemid,Itemname,Itemcategory,Itemdescription,Purchaseprice,Purchasedate,Itemimage,Receiptimage,Upc,Userid,Policyid")] PolicyItems policyItems)
        {
            if (id != policyItems.Itemid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(policyItems);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PolicyItemsExists(policyItems.Itemid))
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
            ViewData["Policyid"] = new SelectList(_context.PolicyDetails, "Policyid", "Policyid", policyItems.Policyid);
            ViewData["Userid"] = new SelectList(_context.PolicyHolder, "Userid", "Email", policyItems.Userid);
            return View(policyItems);
        }

        // GET: PolicyItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var policyItems = await _context.PolicyItems
                .Include(p => p.Policy)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Itemid == id);
            if (policyItems == null)
            {
                return NotFound();
            }

            return View(policyItems);
        }

        // POST: PolicyItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var policyItems = await _context.PolicyItems.FindAsync(id);
            _context.PolicyItems.Remove(policyItems);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PolicyItemsExists(int id)
        {
            return _context.PolicyItems.Any(e => e.Itemid == id);
        }
    }
}
