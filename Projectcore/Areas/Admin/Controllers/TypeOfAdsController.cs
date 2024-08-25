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
    public class TypeOfAdsController : BaseAdminController
    {
        private readonly ApplicationDbContext _context;

        public TypeOfAdsController(ApplicationDbContext context)
            : base(context)
        {
            _context = context;
        }
        
       
        public async Task<IActionResult> Index()
        {
           
            return _context.TypeOfAds != null ? 
                          View(await _context.TypeOfAds.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.TypeOfAds'  is null.");
        }

       
        public async Task<IActionResult> Details(int? id)
        {
            
            if (id == null || _context.TypeOfAds == null)
            {
                return NotFound();
            }

            var typeOfAd = await _context.TypeOfAds
                .FirstOrDefaultAsync(m => m.ID == id);
            if (typeOfAd == null)
            {
                return NotFound();
            }

            return View(typeOfAd);
        }

        
        public IActionResult Create()
        {
            
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title")] TypeOfAd typeOfAd)
        {
            
            TypeOfAd typeAd = new TypeOfAd();
            typeAd.ID = typeOfAd.ID;
            typeAd.Title = typeOfAd.Title;
            _context.TypeOfAds.Add(typeAd);
            var res = _context.SaveChanges();
            if (res > 0)
            {
                return RedirectToAction(nameof(Index));
            }
            
            return View(typeOfAd);
        }

       
        public async Task<IActionResult> Edit(int? id)
        {
            
            if (id == null || _context.TypeOfAds == null)
            {
                return NotFound();
            }

            var typeOfAd = await _context.TypeOfAds.FindAsync(id);
            if (typeOfAd == null)
            {
                return NotFound();
            }
            return View(typeOfAd);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TypeOfAd typeOfAd)
        {
            
            if (id != typeOfAd.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(typeOfAd);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TypeOfAdExists(typeOfAd.ID))
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
            return View(typeOfAd);
        }

        
        public async Task<IActionResult> Delete(int? id)
        {
            
            if (id == null || _context.TypeOfAds == null)
            {
                return NotFound();
            }

            var typeOfAd = await _context.TypeOfAds
                .FirstOrDefaultAsync(m => m.ID == id);
            if (typeOfAd == null)
            {
                return NotFound();
            }

            return View(typeOfAd);
        }

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            
            if (_context.TypeOfAds == null)
            {
                return Problem("Entity set 'ApplicationDbContext.TypeOfAds'  is null.");
            }
            var typeOfAd = await _context.TypeOfAds.FindAsync(id);
            if (typeOfAd != null)
            {
                _context.TypeOfAds.Remove(typeOfAd);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TypeOfAdExists(int id)
        {
          return (_context.TypeOfAds?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
