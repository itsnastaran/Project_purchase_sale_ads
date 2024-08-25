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
    public class PhotoGalleriesController : BaseAdminController
    {
        private readonly ApplicationDbContext _context;

        public PhotoGalleriesController(ApplicationDbContext context)
            : base(context)
        {
            _context = context;
        }
        
        
        public async Task<IActionResult> Index()
        {
            
            var applicationDbContext = _context.PhotoGalleries.Include(p => p._PhotoGallery_SaleAd);
            return View(await applicationDbContext.ToListAsync());
        }

        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PhotoGalleries == null)
            {
                return NotFound();
            }

            var photoGallery = await _context.PhotoGalleries
                .Include(p => p._PhotoGallery_SaleAd)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (photoGallery == null)
            {
                return NotFound();
            }

            return View(photoGallery);
        }

       
        public IActionResult Create()
        {
            ViewData["SaleAdId"] = new SelectList(_context.SaleAds, "ID", "Brand");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,SaleAdId")] PhotoGallery photoGallery)
        {
            if (ModelState.IsValid)
            {
                _context.Add(photoGallery);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SaleAdId"] = new SelectList(_context.SaleAds, "ID", "Brand", photoGallery.SaleAdId);
            return View(photoGallery);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PhotoGalleries == null)
            {
                return NotFound();
            }

            var photoGallery = await _context.PhotoGalleries.FindAsync(id);
            if (photoGallery == null)
            {
                return NotFound();
            }
            ViewData["SaleAdId"] = new SelectList(_context.SaleAds, "ID", "Brand", photoGallery.SaleAdId);
            return View(photoGallery);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,SaleAdId")] PhotoGallery photoGallery)
        {
            if (id != photoGallery.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(photoGallery);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhotoGalleryExists(photoGallery.ID))
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
            ViewData["SaleAdId"] = new SelectList(_context.SaleAds, "ID", "Brand", photoGallery.SaleAdId);
            return View(photoGallery);
        }

      
        public async Task<IActionResult> Delete(int? id)
        {
            
            if (id == null || _context.PhotoGalleries == null)
            {
                return NotFound();
            }

            var photoGallery = await _context.PhotoGalleries
                .Include(p => p._PhotoGallery_SaleAd)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (photoGallery == null)
            {
                return NotFound();
            }

            return View(photoGallery);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
           
            if (_context.PhotoGalleries == null)
            {
                return Problem("Entity set 'ApplicationDbContext.PhotoGalleries'  is null.");
            }
            var photoGallery = await _context.PhotoGalleries.FindAsync(id);
            if (photoGallery != null)
            {
                _context.PhotoGalleries.Remove(photoGallery);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PhotoGalleryExists(int id)
        {
          return (_context.PhotoGalleries?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
