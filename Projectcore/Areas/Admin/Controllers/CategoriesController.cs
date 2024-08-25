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
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Projectcore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "mainadmin")]
    public class CategoriesController : BaseAdminController
    {
        private readonly ApplicationDbContext _context;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hosting;
        private readonly UserManager<ApplicationUser> _userManager;
        public CategoriesController(ApplicationDbContext context, Microsoft.AspNetCore.Hosting.IHostingEnvironment hosting, UserManager<ApplicationUser> userManager)
            : base(context)
        {
            _hosting = hosting;
            _context = context;
            _userManager = userManager;
        }


        
        public async Task<IActionResult> Index()
        {
            return _context.Category != null ?
                          View(await _context.Category.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Category'  is null.");
        }

        public async Task<IActionResult> Details(int? id)
        {


            if (id == null || _context.Category == null)
            {
                return NotFound();
            }

            var category = await _context.Category
                .FirstOrDefaultAsync(m => m.ID == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        public IActionResult Create()
        {

            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title")] Category category, IFormFile Icon)
        {
            var upload = Path.Combine(_hosting.WebRootPath, "Upload/Category");
            var file = Path.Combine(upload, Icon.FileName);
            using (var f = new FileStream(file, FileMode.Create))
            {
                await Icon.CopyToAsync(f).ConfigureAwait(false);
            }

            category.Icon = Icon.FileName;
            _context.Add(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

            return View(category);
        }

        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Category.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Category category)
        {

            if (id != category.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.ID))
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
            return View(category);
        }

        public async Task<IActionResult> Delete(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Category
                .FirstOrDefaultAsync(m => m.ID == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var category = await _context.Category.FindAsync(id);
            _context.Category.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return _context.Category.Any(e => e.ID == id);
        }
    }
}
