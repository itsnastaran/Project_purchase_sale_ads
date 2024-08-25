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
    public class ConfigEmailsController : BaseAdminController
    {
        private readonly ApplicationDbContext _context;

        public ConfigEmailsController(ApplicationDbContext context)
            :base(context)
        {
            _context = context;
        }
        
        
        public async Task<IActionResult> Index()
        {
           
            return _context.ConfigEmails != null ? 
                          View(await _context.ConfigEmails.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.ConfigEmails'  is null.");
        }

        
        public async Task<IActionResult> Details(int? id)
        {
            
            if (id == null || _context.ConfigEmails == null)
            {
                return NotFound();
            }

            var configEmail = await _context.ConfigEmails
                .FirstOrDefaultAsync(m => m.ID == id);
            if (configEmail == null)
            {
                return NotFound();
            }

            return View(configEmail);
        }

     
        public IActionResult Create()
        {
           
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Username,Port,Password,Smtp")] ConfigEmail configEmail)
        {
            
            if (ModelState.IsValid)
            {
                _context.Add(configEmail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(configEmail);
        }

       
        public async Task<IActionResult> Edit(int? id)
        {
            
            if (id == null || _context.ConfigEmails == null)
            {
                return NotFound();
            }

            var configEmail = await _context.ConfigEmails.FindAsync(id);
            if (configEmail == null)
            {
                return NotFound();
            }
            return View(configEmail);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Username,Port,Password,Smtp")] ConfigEmail configEmail)
        {
            
            if (id != configEmail.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(configEmail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConfigEmailExists(configEmail.ID))
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
            return View(configEmail);
        }

     
        public async Task<IActionResult> Delete(int? id)
        {
            
            if (id == null || _context.ConfigEmails == null)
            {
                return NotFound();
            }

            var configEmail = await _context.ConfigEmails
                .FirstOrDefaultAsync(m => m.ID == id);
            if (configEmail == null)
            {
                return NotFound();
            }

            return View(configEmail);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            
            if (_context.ConfigEmails == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ConfigEmails'  is null.");
            }
            var configEmail = await _context.ConfigEmails.FindAsync(id);
            if (configEmail != null)
            {
                _context.ConfigEmails.Remove(configEmail);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConfigEmailExists(int id)
        {
          return (_context.ConfigEmails?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
