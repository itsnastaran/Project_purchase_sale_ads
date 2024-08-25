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
    public class MenusController : BaseAdminController
    {
        private readonly ApplicationDbContext _context;

        public MenusController(ApplicationDbContext context)
            : base(context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> Index()
        {
            
            return _context.Menus != null ? 
                          View(await _context.Menus.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Menus'  is null.");
        }

       
        public async Task<IActionResult> Details(int? id)
        {
            
            if (id == null || _context.Menus == null)
            {
                return NotFound();
            }

            var menu = await _context.Menus
                .FirstOrDefaultAsync(m => m.ID == id);
            if (menu == null)
            {
                return NotFound();
            }

            return View(menu);
        }

        
        public IActionResult Create()
        {
            
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,Link,Status")] Menu menu)
        {
            
            if (ModelState.IsValid)
            {
                _context.Add(menu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(menu);
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            
            if (id == null || _context.Menus == null)
            {
                return NotFound();
            }

            var menu = await _context.Menus.FindAsync(id);
            if (menu == null)
            {
                return NotFound();
            }
            return View(menu);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,Link,Status")] Menu menu)
        {
            
            if (id != menu.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(menu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuExists(menu.ID))
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
            return View(menu);
        }

        
        public async Task<IActionResult> Delete(int? id)
        {
            
            if (id == null || _context.Menus == null)
            {
                return NotFound();
            }

            var menu = await _context.Menus
                .FirstOrDefaultAsync(m => m.ID == id);
            if (menu == null)
            {
                return NotFound();
            }

            return View(menu);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            
            if (_context.Menus == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Menus'  is null.");
            }
            var menu = await _context.Menus.FindAsync(id);
            if (menu != null)
            {
                _context.Menus.Remove(menu);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MenuExists(int id)
        {
          return (_context.Menus?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
