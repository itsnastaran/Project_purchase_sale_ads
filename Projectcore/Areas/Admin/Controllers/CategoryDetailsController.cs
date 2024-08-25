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
    public class CategoryDetailsController : BaseAdminController
    {
        private readonly ApplicationDbContext _context;

        public CategoryDetailsController(ApplicationDbContext context)
            :base(context)
        {
            _context = context;
        }
        

        public async Task<IActionResult> Index()
        {
            
            var applicationDbContext = _context.CategoryDetails.Include(c => c._CategoryDetails_Categories);
            return View(await applicationDbContext.ToListAsync());
        }

        
        public async Task<IActionResult> Details(int? id)
        {
           
            if (id == null || _context.CategoryDetails == null)
            {
                return NotFound();
            }

            var categoryDetails = await _context.CategoryDetails
                .Include(c => c._CategoryDetails_Categories)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (categoryDetails == null)
            {
                return NotFound();
            }

            return View(categoryDetails);
        }

        
        public IActionResult Create()
        {
           
            ViewData["CategoryID"] = new SelectList(_context.Category, "ID", "Title");
            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,CategoryID")]CategoryDetails categoryDetails)
        {
          
            CategoryDetails catDetails = new CategoryDetails();
            catDetails.CategoryID = categoryDetails.CategoryID;
            catDetails.Title=categoryDetails.Title;
            _context.CategoryDetails.Add(catDetails);
            var res=_context.SaveChanges();
            if(res>0)
            {
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryID"] = new SelectList(_context.Category, "ID", "Title", categoryDetails.CategoryID);
            return View(categoryDetails);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            
            if (id == null || _context.CategoryDetails == null)
            {
                return NotFound();
            }

            var categoryDetails = await _context.CategoryDetails.FindAsync(id);
            if (categoryDetails == null)
            {
                return NotFound();
            }
            ViewData["CategoryID"] = new SelectList(_context.Category, "ID", "Title", categoryDetails.CategoryID);
            return View(categoryDetails);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,CategoryID")] CategoryDetails categoryDetails)
        {
            
            
            if (id != categoryDetails.ID)
            {
                return NotFound();
            }


            try
            {
                _context.Update(categoryDetails);
                await _context.SaveChangesAsync();
            }
            catch
            {
                ViewData["CategoryID"] = new SelectList(_context.Category, "ID", "Title", categoryDetails.CategoryID);
                return View(categoryDetails);
            }

            return RedirectToAction(nameof(Index));
        }

        
        public async Task<IActionResult> Delete(int? id)
        {
            
            if (id == null || _context.CategoryDetails == null)
            {
                return NotFound();
            }

            var categoryDetails = await _context.CategoryDetails
                .Include(c => c._CategoryDetails_Categories)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (categoryDetails == null)
            {
                return NotFound();
            }

            return View(categoryDetails);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            
            if (_context.CategoryDetails == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CategoryDetails'  is null.");
            }
            var categoryDetails = await _context.CategoryDetails.FindAsync(id);
            if (categoryDetails != null)
            {
                _context.CategoryDetails.Remove(categoryDetails);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryDetailsExists(int id)
        {
          return (_context.CategoryDetails?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
