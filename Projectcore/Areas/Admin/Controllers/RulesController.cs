using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain;
using Projectcore.Data;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Projectcore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "mainadmin")]
    public class RulesController : BaseAdminController
    {
        private readonly ApplicationDbContext _context;

        public RulesController(ApplicationDbContext context)
            : base(context)
        {
            _context = context;
        }

        
        
        public async Task<IActionResult> Index()
        {
            
            return _context.Rules != null ? 
                          View(await _context.Rules.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Rules'  is null.");
        }

        
        public async Task<IActionResult> Details(int? id)
        {
            
            if (id == null || _context.Rules == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var rules = await _context.Rules
                .FirstOrDefaultAsync(m => m.ID == id);
            if (rules == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(rules);
        }

        
        public IActionResult Create()
        {
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title")] Rules rules)
        {
            
            if (ModelState.IsValid)
            {
                _context.Add(rules);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rules);
        }

       
        public async Task<IActionResult> Edit(int? id)
        {
            
            if (id == null || _context.Rules == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var rules = await _context.Rules.FindAsync(id);
            if (rules == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(rules);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title")] Rules rules)
        {
            
            if (id != rules.ID)
            {
                return RedirectToAction(nameof(Index));
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rules);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RulesExists(rules.ID))
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(rules);
        }

      
        public async Task<IActionResult> Delete(int? id)
        {
            
            if (id == null || _context.Rules == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var rules = await _context.Rules
                .FirstOrDefaultAsync(m => m.ID == id);
            if (rules == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(rules);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            
            if (_context.Rules == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Rules'  is null.");
            }
            var rules = await _context.Rules.FindAsync(id);
            if (rules != null)
            {
                _context.Rules.Remove(rules);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RulesExists(int id)
        {
          return (_context.Rules?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
