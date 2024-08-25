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
    public class ConfigSmsController : BaseAdminController
    {
        private readonly ApplicationDbContext _context;

        public ConfigSmsController(ApplicationDbContext context)
            :base(context)
        {
            _context = context;
        }
        
        
        public async Task<IActionResult> Index()
        {
          
              return _context.ConfigSms != null ? 
                          View(await _context.ConfigSms.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.ConfigSms'  is null.");
        }

        
        public async Task<IActionResult> Details(int? id)
        {
            
            if (id == null || _context.ConfigSms == null)
            {
                return NotFound();
            }

            var configSms = await _context.ConfigSms
                .FirstOrDefaultAsync(m => m.ID == id);
            if (configSms == null)
            {
                return NotFound();
            }

            return View(configSms);
        }

       
        public IActionResult Create()
        {
          
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Key,Api,status")] ConfigSms configSms)
        {
            
            if (ModelState.IsValid)
            {
                _context.Add(configSms);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(configSms);
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            
            if (id == null || _context.ConfigSms == null)
            {
                return NotFound();
            }

            var configSms = await _context.ConfigSms.FindAsync(id);
            if (configSms == null)
            {
                return NotFound();
            }
            return View(configSms);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Key,Api,status")] ConfigSms configSms)
        {
            
            if (id != configSms.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(configSms);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConfigSmsExists(configSms.ID))
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
            return View(configSms);
        }

        
        public async Task<IActionResult> Delete(int? id)
        {
            
            if (id == null || _context.ConfigSms == null)
            {
                return NotFound();
            }

            var configSms = await _context.ConfigSms
                .FirstOrDefaultAsync(m => m.ID == id);
            if (configSms == null)
            {
                return NotFound();
            }

            return View(configSms);
        }

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            
            if (_context.ConfigSms == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ConfigSms'  is null.");
            }
            var configSms = await _context.ConfigSms.FindAsync(id);
            if (configSms != null)
            {
                _context.ConfigSms.Remove(configSms);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConfigSmsExists(int id)
        {
            
            return (_context.ConfigSms?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
