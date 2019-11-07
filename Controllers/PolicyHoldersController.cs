using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PersonalPropertyApp.Models;

namespace PersonalPropertyApp.Controllers
{
    public class PolicyHoldersController : Controller
    {
        private readonly InsuranceAppContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public PolicyHoldersController(InsuranceAppContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: PolicyHolders
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var currentEmail = user.Email;
            return View(await _context.PolicyHolder.Where(u => u.Email == currentEmail).ToListAsync());
        }

        // GET: PolicyHolders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var policyHolder = await _context.PolicyHolder
                .FirstOrDefaultAsync(m => m.Userid == id);
            if (policyHolder == null)
            {
                return NotFound();
            }

            return View(policyHolder);
        }

        // GET: PolicyHolders/Create
        //public IActionResult Create()
        //{
            //return View();
        //}

        // POST: PolicyHolders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Userid,Firstname,Lastname,Email,Address")] PolicyHolder policyHolder)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(policyHolder);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(policyHolder);
        //}

        // GET: PolicyHolders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var policyHolder = await _context.PolicyHolder.FindAsync(id);
            if (policyHolder == null)
            {
                return NotFound();
            }
            return View(policyHolder);
        }

        // POST: PolicyHolders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Userid,Firstname,Lastname,Email,Address")] PolicyHolder policyHolder)
        {
            if (id != policyHolder.Userid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(policyHolder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PolicyHolderExists(policyHolder.Userid))
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
            return View(policyHolder);
        }

        // GET: PolicyHolders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var policyHolder = await _context.PolicyHolder
                .FirstOrDefaultAsync(m => m.Userid == id);
            if (policyHolder == null)
            {
                return NotFound();
            }

            return View(policyHolder);
        }

        // POST: PolicyHolders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var policyHolder = await _context.PolicyHolder.FindAsync(id);
            _context.PolicyHolder.Remove(policyHolder);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PolicyHolderExists(int id)
        {
            return _context.PolicyHolder.Any(e => e.Userid == id);
        }
    }
}
