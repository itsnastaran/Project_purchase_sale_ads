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
using Projectcore.Controllers;

namespace Projectcore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "mainadmin")]
    public class AboutUsController : BaseAdminController
    {
        private readonly ApplicationDbContext _context;

        public AboutUsController(ApplicationDbContext context)
            :base(context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> Index()
        {
           
              return _context.AboutUs != null ? 
                          View(await _context.AboutUs.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.AboutUs'  is null.");
        }

        public async Task<IActionResult> Details(int? id)
        {
            
            if (id == null || _context.AboutUs == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var aboutUs = await _context.AboutUs
                .FirstOrDefaultAsync(m => m.ID == id);
            if (aboutUs == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(aboutUs);
        }

       
        public IActionResult Create()
        {
            
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,Description,Email,KeyWords,Tel,Mobile,Address")] AboutUs aboutUs)
        {
            
            if (ModelState.IsValid)
            {
                _context.Add(aboutUs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aboutUs);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            
            if (id == null || _context.AboutUs == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var aboutUs = await _context.AboutUs.FindAsync(id);
            if (aboutUs == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(aboutUs);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,Description,Email,KeyWords,Tel,Mobile,Address")] AboutUs aboutUs)
        {
            
            if (id != aboutUs.ID)
            {
                return RedirectToAction(nameof(Index));
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aboutUs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AboutUsExists(aboutUs.ID))
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
            return View(aboutUs);
        }

        
        public async Task<IActionResult> Delete(int? id)
        {
            
            if (id == null || _context.AboutUs == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var aboutUs = await _context.AboutUs
                .FirstOrDefaultAsync(m => m.ID == id);
            if (aboutUs == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(aboutUs);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            
            if (_context.AboutUs == null)
            {
                return Problem("Entity set 'ApplicationDbContext.AboutUs'  is null.");
            }
            var aboutUs = await _context.AboutUs.FindAsync(id);
            if (aboutUs != null)
            {
                _context.AboutUs.Remove(aboutUs);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AboutUsExists(int id)
        {
          return (_context.AboutUs?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
