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
    public class FavortiesController : BaseAdminController
    {
        private readonly ApplicationDbContext _context;

        public FavortiesController(ApplicationDbContext context)
            : base(context)
        {
            _context = context;
        }
        
       
        public async Task<IActionResult> Index()
        {
            
            var applicationDbContext = _context.Favorties.Include(f => f._Favorties_SaleAdId);
            
            return View(await applicationDbContext.ToListAsync());
        }

      
        public async Task<IActionResult> Details(int? id)
        {
            
            if (id == null || _context.Favorties == null)
            {
                return NotFound();
            }

            var favorties = await _context.Favorties
                .Include(f => f._Favorties_SaleAdId)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (favorties == null)
            {
                return NotFound();
            }

            return View(favorties);
        }

       
        public IActionResult Create()
        {

            ViewData["SaleAdId"] = new SelectList(_context.SaleAds, "ID", "SaleAdId");
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,UserID,SaleAdId")] Favorties favorties)
        {
            if (ModelState.IsValid)
            {
                _context.Add(favorties);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SaleAdId"] = new SelectList(_context.SaleAds, "ID", "Brand", favorties.SaleAdId);
            return View(favorties);
        }

       
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Favorties == null)
            {
                return NotFound();
            }

            var favorties = await _context.Favorties.FindAsync(id);
            if (favorties == null)
            {
                return NotFound();
            }
            ViewData["SaleAdId"] = new SelectList(_context.SaleAds, "ID", "Brand", favorties.SaleAdId);
            return View(favorties);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Favorties favorties)
        {
            if (id != favorties.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(favorties);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FavortiesExists(favorties.ID))
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
            ViewData["SaleAdId"] = new SelectList(_context.SaleAds, "ID", "Brand", favorties.SaleAdId);
            return View(favorties);
        }

       
        public async Task<IActionResult> Delete(int? id)
        {
            
            if (id == null || _context.Favorties == null)
            {
                return NotFound();
            }

            var favorties = await _context.Favorties
                .Include(f => f._Favorties_SaleAdId)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (favorties == null)
            {
                return NotFound();
            }

            return View(favorties);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            
            if (_context.Favorties == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Favorties'  is null.");
            }
            var favorties = await _context.Favorties.FindAsync(id);
            if (favorties != null)
            {
                _context.Favorties.Remove(favorties);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FavortiesExists(int id)
        {
          return (_context.Favorties?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
