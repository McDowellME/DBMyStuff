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
    public class PolicyDetailsController : Controller
    {
        private readonly InsuranceAppContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        

        public PolicyDetailsController(InsuranceAppContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: PolicyDetails
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var currentEmail = user.Email;
            var insuranceAppContext = _context.PolicyDetails.Include(p => p.User);
            return View(await insuranceAppContext.Where(p => p.User.Email == currentEmail).ToListAsync());
        }

        // GET: PolicyDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var policyDetails = await _context.PolicyDetails
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Policyid == id);
            if (policyDetails == null)
            {
                return NotFound();
            }

            return View(policyDetails);
        }

        // GET: PolicyDetails/Create
        public IActionResult Create()
        {
            //ViewData["Userid"] = new SelectList(_context.PolicyHolder, "Userid", "Email");            
            return View();
        }

        // POST: PolicyDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Policynickname,Policytype,Userid,Policynumber,Inscompanyname,Inscontactname,Inscompanywebsite,Inscontactphone,Inscontactemail")] PolicyDetails policyDetails)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                var currentEmail = user.Email;
                var newUserId = await _context.PolicyHolder.FirstOrDefaultAsync(p => p.Email == currentEmail);
                
                policyDetails.Userid = newUserId.Userid;
                _context.Add(policyDetails);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["Userid"] = new SelectList(_context.PolicyHolder, "Userid", "Email", policyDetails.Userid);
            return View(policyDetails);
        }

        // GET: PolicyDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var policyDetails = await _context.PolicyDetails.FindAsync(id);
            if (policyDetails == null)
            {
                return NotFound();
            }
            //ViewData["Userid"] = new SelectList(_context.PolicyHolder, "Userid", "Email", policyDetails.Userid);
            return View(policyDetails);
        }

        // POST: PolicyDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Policyid,Userid,Policynumber,Inscompanyid,Inscompanyname,Inscontactname,Inscompanywebsite,Inscontactphone,Inscontactemail,Policynickname,Policytype")] PolicyDetails policyDetails)
        {
            if (id != policyDetails.Policyid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    
                    _context.Update(policyDetails);                    
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PolicyDetailsExists(policyDetails.Policyid))
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
            //ViewData["Userid"] = new SelectList(_context.PolicyHolder, "Userid", "Email", policyDetails.Userid);
            return View(policyDetails);
        }

        // GET: PolicyDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var policyDetails = await _context.PolicyDetails
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Policyid == id);
            if (policyDetails == null)
            {
                return NotFound();
            }

            return View(policyDetails);
        }

        // POST: PolicyDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var policyDetails = await _context.PolicyDetails.FindAsync(id);
            _context.PolicyDetails.Remove(policyDetails);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PolicyDetailsExists(int id)
        {
            return _context.PolicyDetails.Any(e => e.Policyid == id);
        }
    }
}
