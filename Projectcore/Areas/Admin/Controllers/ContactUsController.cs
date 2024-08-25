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
    [Authorize(Roles = "mainadmin")]
    [Area("Admin")]
    public class ContactUsController : BaseAdminController
    {
        private readonly ApplicationDbContext _context;

        public ContactUsController(ApplicationDbContext context)
            :base(context)
        {
            _context = context;
        }
        
       
        public async Task<IActionResult> Index()
        {
           
            return _context.ContactUs != null ? 
                          View(await _context.ContactUs.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.ContactUs'  is null.");
        }

      
        public async Task<IActionResult> Details(int? id)
        {
           
            if (id == null || _context.ContactUs == null)
            {
                return NotFound();
            }

            var contactUs = await _context.ContactUs
                .FirstOrDefaultAsync(m => m.ID == id);
            if (contactUs == null)
            {
                return NotFound();
            }

            return View(contactUs);
        }

       
        public IActionResult Create()
        {
            
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,Message,NameFamily,Email,status,Date")] ContactUs contactUs)
        {
            
            if (ModelState.IsValid)
            {
                _context.Add(contactUs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contactUs);
        }

       
        public async Task<IActionResult> Edit(int? id)
        {
            
            if (id == null || _context.ContactUs == null)
            {
                return NotFound();
            }

            var contactUs = await _context.ContactUs.FindAsync(id);
            if (contactUs == null)
            {
                return NotFound();
            }
            return View(contactUs);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,Message,NameFamily,Email,status,Date")] ContactUs contactUs)
        {
            
            if (id != contactUs.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contactUs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactUsExists(contactUs.ID))
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
            return View(contactUs);
        }

      
        public async Task<IActionResult> Delete(int? id)
        {
            
            if (id == null || _context.ContactUs == null)
            {
                return NotFound();
            }

            var contactUs = await _context.ContactUs
                .FirstOrDefaultAsync(m => m.ID == id);
            if (contactUs == null)
            {
                return NotFound();
            }

            return View(contactUs);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            
            if (_context.ContactUs == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ContactUs'  is null.");
            }
            var contactUs = await _context.ContactUs.FindAsync(id);
            if (contactUs != null)
            {
                _context.ContactUs.Remove(contactUs);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactUsExists(int id)
        {
          return (_context.ContactUs?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
