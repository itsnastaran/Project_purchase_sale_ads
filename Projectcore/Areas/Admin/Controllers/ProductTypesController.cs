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
    public class ProductTypesController : BaseAdminController
    {
        private readonly ApplicationDbContext _context;

        public ProductTypesController(ApplicationDbContext context)
            : base(context)
        {
            _context = context;
        }
        
    
        public async Task<IActionResult> Index()
        {
            
              return _context.ProductTypes != null ? 
                          View(await _context.ProductTypes.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.ProductTypes'  is null.");
        }

       
        public async Task<IActionResult> Details(int? id)
        {
            
            if (id == null || _context.ProductTypes == null)
            {
                return NotFound();
            }

            var productType = await _context.ProductTypes
                .FirstOrDefaultAsync(m => m.ID == id);
            if (productType == null)
            {
                return NotFound();
            }

            return View(productType);
        }

      
        public IActionResult Create()
        {
            
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title")] ProductType productType)
        {
            try
            {
                _context.Add(productType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {

                return View(productType);
            }
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            
            if (id == null || _context.ProductTypes == null)
            {
                return NotFound();
            }

            var productType = await _context.ProductTypes.FindAsync(id);
            if (productType == null)
            {
                return NotFound();
            }
            return View(productType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title")] ProductType productType)
        {
            
            
            if (id != productType.ID)
            {
                return NotFound();
            }


            try
            {
                _context.Update(productType);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return View(productType);
            }
            return RedirectToAction(nameof(Index));
        }

        
        public async Task<IActionResult> Delete(int? id)
        {
            
            if (id == null || _context.ProductTypes == null)
            {
                return NotFound();
            }

            var productType = await _context.ProductTypes
                .FirstOrDefaultAsync(m => m.ID == id);
            if (productType == null)
            {
                return NotFound();
            }

            return View(productType);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            
            if (_context.ProductTypes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ProductTypes'  is null.");
            }
            var productType = await _context.ProductTypes.FindAsync(id);
            if (productType != null)
            {
                _context.ProductTypes.Remove(productType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductTypeExists(int id)
        {
          return (_context.ProductTypes?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
