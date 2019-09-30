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
    public class ClaimReportsController : Controller
    {
        private readonly InsuranceAppContext _context;

        public ClaimReportsController(InsuranceAppContext context)
        {
            _context = context;
        }

        // GET: ClaimReports
        public async Task<IActionResult> Index()
        {
            var insuranceAppContext = _context.ClaimReport.Include(c => c.Policy).Include(c => c.User);
            return View(await insuranceAppContext.ToListAsync());
        }

        // GET: ClaimReports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var claimReport = await _context.ClaimReport
                .Include(c => c.Policy)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Reportid == id);
            if (claimReport == null)
            {
                return NotFound();
            }

            return View(claimReport);
        }

        // GET: ClaimReports/Create
        public IActionResult Create()
        {
            ViewData["Policyid"] = new SelectList(_context.PolicyDetails, "Policyid", "Policyid");
            ViewData["Userid"] = new SelectList(_context.PolicyHolder, "Userid", "Email");
            return View();
        }

        // POST: ClaimReports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Reportid,Claimreason,Reportdate,Policyid,Userid")] ClaimReport claimReport)
        {
            if (ModelState.IsValid)
            {
                _context.Add(claimReport);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Policyid"] = new SelectList(_context.PolicyDetails, "Policyid", "Policyid", claimReport.Policyid);
            ViewData["Userid"] = new SelectList(_context.PolicyHolder, "Userid", "Email", claimReport.Userid);
            return View(claimReport);
        }

        // GET: ClaimReports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var claimReport = await _context.ClaimReport.FindAsync(id);
            if (claimReport == null)
            {
                return NotFound();
            }
            ViewData["Policyid"] = new SelectList(_context.PolicyDetails, "Policyid", "Policyid", claimReport.Policyid);
            ViewData["Userid"] = new SelectList(_context.PolicyHolder, "Userid", "Email", claimReport.Userid);
            return View(claimReport);
        }

        // POST: ClaimReports/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Reportid,Claimreason,Reportdate,Policyid,Userid")] ClaimReport claimReport)
        {
            if (id != claimReport.Reportid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(claimReport);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClaimReportExists(claimReport.Reportid))
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
            ViewData["Policyid"] = new SelectList(_context.PolicyDetails, "Policyid", "Policyid", claimReport.Policyid);
            ViewData["Userid"] = new SelectList(_context.PolicyHolder, "Userid", "Email", claimReport.Userid);
            return View(claimReport);
        }

        // GET: ClaimReports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var claimReport = await _context.ClaimReport
                .Include(c => c.Policy)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Reportid == id);
            if (claimReport == null)
            {
                return NotFound();
            }

            return View(claimReport);
        }

        // POST: ClaimReports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var claimReport = await _context.ClaimReport.FindAsync(id);
            _context.ClaimReport.Remove(claimReport);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClaimReportExists(int id)
        {
            return _context.ClaimReport.Any(e => e.Reportid == id);
        }
    }
}
