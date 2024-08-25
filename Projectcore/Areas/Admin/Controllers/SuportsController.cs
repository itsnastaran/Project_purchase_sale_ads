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
using System.Drawing;
using System.Security.Claims;

namespace Projectcore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "mainadmin")]
    public class SuportsController : BaseAdminController
    {
        private readonly ApplicationDbContext _context;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hosting;
        public SuportsController(ApplicationDbContext context, Microsoft.AspNetCore.Hosting.IHostingEnvironment hosting)
            : base(context)
        {
            _context = context;
            _hosting = hosting;
        }
        
        
        public async Task<IActionResult> Index()
        {
           
              return _context.Suports != null ? 
                          View(await _context.Suports.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Suports'  is null.");
        }

        
        public async Task<IActionResult> Details(int? id)
        {
            
            if (id == null || _context.Suports == null)
            {
                return NotFound();
            }

            var suport = await _context.Suports
                .FirstOrDefaultAsync(m => m.ID == id);
            if (suport == null)
            {
                return NotFound();
            }

            return View(suport);
        }

      
        public IActionResult Create()
        {
            
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,Description")] Suport suport, IFormFile Icon)
        {
            
            var upload = Path.Combine(_hosting.WebRootPath, "Upload/Suport");
            var file = Path.Combine(upload, Icon.FileName);
            using (var f = new FileStream(file, FileMode.Create))
            {
                await Icon.CopyToAsync(f).ConfigureAwait(false);
            }

            suport.Icon = Icon.FileName;
            _context.Add(suport);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

            return View(suport);
          
        }

       
        public async Task<IActionResult> Edit(int? id)
        {
            
            if (id == null || _context.Suports == null)
            {
                return NotFound();
            }

            var suport = await _context.Suports.FindAsync(id);
            if (suport == null)
            {
                return NotFound();
            }
            return View(suport);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,Description,Icon")] Suport suport)
        {
            
            if (id != suport.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(suport);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SuportExists(suport.ID))
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
            return View(suport);
        }

       
        public async Task<IActionResult> Delete(int? id)
        {
            
            if (id == null || _context.Suports == null)
            {
                return NotFound();
            }

            var suport = await _context.Suports
                .FirstOrDefaultAsync(m => m.ID == id);
            if (suport == null)
            {
                return NotFound();
            }

            return View(suport);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            
            if (_context.Suports == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Suports'  is null.");
            }
            var suport = await _context.Suports.FindAsync(id);
            if (suport != null)
            {
                _context.Suports.Remove(suport);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SuportExists(int id)
        {
          return (_context.Suports?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
