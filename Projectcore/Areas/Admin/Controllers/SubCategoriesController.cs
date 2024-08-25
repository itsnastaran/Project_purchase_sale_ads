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
    public class SubCategoriesController : BaseAdminController
    {
        private readonly ApplicationDbContext _context;

        public SubCategoriesController(ApplicationDbContext context)
            : base(context)
        {
            _context = context;
        }
        
       
        public async Task<IActionResult> Index()
        {
            
            var applicationDbContext = _context.SubCategories.Include(s => s._SubCategory_Categories);
            return View(await applicationDbContext.ToListAsync());
        }

        
        public async Task<IActionResult> Details(int? id)
        {
            
            if (id == null || _context.SubCategories == null)
            {
                return NotFound();
            }

            var subCategory = await _context.SubCategories
                .Include(s => s._SubCategory_Categories)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (subCategory == null)
            {
                return NotFound();
            }

            return View(subCategory);
        }

        
        public IActionResult Create()
        {
            
            ViewData["CategoryID"] = new SelectList(_context.Category, "ID", "Title");
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,CategoryID")] SubCategory subCategory)
        {
            
            
            try
            {
                _context.Add(subCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewData["CategoryID"] = new SelectList(_context.Category, "ID", "Title", subCategory.CategoryID);
                return View(subCategory);
            }
        }

      
        public async Task<IActionResult> Edit(int? id)
        {
            
            if (id == null || _context.SubCategories == null)
            {
                return NotFound();
            }

            var subCategory = await _context.SubCategories.FindAsync(id);
            if (subCategory == null)
            {
                return NotFound();
            }
            ViewData["CategoryID"] = new SelectList(_context.Category, "ID", "Title", subCategory.CategoryID);
            return View(subCategory);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,CategoryID")] SubCategory subCategory)
        {
            
            if (id != subCategory.ID)
            {
                return NotFound();
            }

            try
            {
                _context.Update(subCategory);
                await _context.SaveChangesAsync();
            }
            catch
            {

                ViewData["CategoryID"] = new SelectList(_context.Category, "ID", "Title", subCategory.CategoryID);
                return View(subCategory);
            }
            return RedirectToAction(nameof(Index));
        }

        
        public async Task<IActionResult> Delete(int? id)
        {
            
            if (id == null || _context.SubCategories == null)
            {
                return NotFound();
            }

            var subCategory = await _context.SubCategories
                .Include(s => s._SubCategory_Categories)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (subCategory == null)
            {
                return NotFound();
            }

            return View(subCategory);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            
            if (_context.SubCategories == null)
            {
                return Problem("Entity set 'ApplicationDbContext.SubCategories'  is null.");
            }
            var subCategory = await _context.SubCategories.FindAsync(id);
            if (subCategory != null)
            {
                _context.SubCategories.Remove(subCategory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubCategoryExists(int id)
        {
          return (_context.SubCategories?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
