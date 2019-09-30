using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PersonalPropertyApp.Models;

namespace PersonalPropertyApp.Controllers
{
    public class ClaimItemsController : Controller
    {
        private readonly InsuranceAppContext _context;

        public ClaimItemsController(InsuranceAppContext context)
        {
            _context = context;
        }

        // GET: ClaimItems
        public async Task<IActionResult> Index()
        {
            var insuranceAppContext = _context.ClaimItems.Include(c => c.Item).Include(c => c.Report);
            return View(await insuranceAppContext.ToListAsync());
        }

        // GET: ClaimItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var claimItems = await _context.ClaimItems
                .Include(c => c.Item)
                .Include(c => c.Report)
                .FirstOrDefaultAsync(m => m.Claimitemid == id);
            if (claimItems == null)
            {
                return NotFound();
            }

            return View(claimItems);
        }

        // GET: ClaimItems/Create
        public IActionResult Create()
        {
            ViewData["Itemid"] = new SelectList(_context.PolicyItems, "Itemid", "Itemname");
            ViewData["Reportid"] = new SelectList(_context.ClaimReport, "Reportid", "Reportid");
            return View();
        }

        // POST: ClaimItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Claimitemid,Reportid,Itemid")] ClaimItems claimItems)
        {
            if (ModelState.IsValid)
            {
                _context.Add(claimItems);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Itemid"] = new SelectList(_context.PolicyItems, "Itemid", "Itemname", claimItems.Itemid);
            ViewData["Reportid"] = new SelectList(_context.ClaimReport, "Reportid", "Reportid", claimItems.Reportid);
            return View(claimItems);
        }

        // GET: ClaimItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var claimItems = await _context.ClaimItems.FindAsync(id);
            if (claimItems == null)
            {
                return NotFound();
            }
            ViewData["Itemid"] = new SelectList(_context.PolicyItems, "Itemid", "Itemname", claimItems.Itemid);
            ViewData["Reportid"] = new SelectList(_context.ClaimReport, "Reportid", "Reportid", claimItems.Reportid);
            return View(claimItems);
        }

        // POST: ClaimItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Claimitemid,Reportid,Itemid")] ClaimItems claimItems)
        {
            if (id != claimItems.Claimitemid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(claimItems);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClaimItemsExists(claimItems.Claimitemid))
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
            ViewData["Itemid"] = new SelectList(_context.PolicyItems, "Itemid", "Itemname", claimItems.Itemid);
            ViewData["Reportid"] = new SelectList(_context.ClaimReport, "Reportid", "Reportid", claimItems.Reportid);
            return View(claimItems);
        }

        // GET: ClaimItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var claimItems = await _context.ClaimItems
                .Include(c => c.Item)
                .Include(c => c.Report)
                .FirstOrDefaultAsync(m => m.Claimitemid == id);
            if (claimItems == null)
            {
                return NotFound();
            }

            return View(claimItems);
        }

        // POST: ClaimItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var claimItems = await _context.ClaimItems.FindAsync(id);
            _context.ClaimItems.Remove(claimItems);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClaimItemsExists(int id)
        {
            return _context.ClaimItems.Any(e => e.Claimitemid == id);
        }
    }
}
