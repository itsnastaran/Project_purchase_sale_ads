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
    public class ReportsController : BaseAdminController
    {
        private readonly ApplicationDbContext _context;

        public ReportsController(ApplicationDbContext context)
            : base(context)
        {
            _context = context;
        }
       
        public async Task<IActionResult> Index()
        {
            
            var applicationDbContext = _context.Reports.Include(r => r._Report_SaleAd);
            return View(await applicationDbContext.ToListAsync());
        }

        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Reports == null)
            {
                return NotFound();
            }

            var report = await _context.Reports
                .Include(r => r._Report_SaleAd)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (report == null)
            {
                return NotFound();
            }

            return View(report);
        }

       
        public IActionResult Create()
        {
            ViewData["AdID"] = new SelectList(_context.SaleAds, "ID", "Brand");
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,status,Date,AdID")] Report report)
        {
            if (ModelState.IsValid)
            {
                _context.Add(report);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AdID"] = new SelectList(_context.SaleAds, "ID", "Brand", report.AdID);
            return View(report);
        }

       
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Reports == null)
            {
                return NotFound();
            }

            var report = await _context.Reports.FindAsync(id);
            if (report == null)
            {
                return NotFound();
            }
            ViewData["AdID"] = new SelectList(_context.SaleAds, "ID", "Brand", report.AdID);
            return View(report);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,status,Date,AdID")] Report report)
        {
            if (id != report.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(report);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReportExists(report.ID))
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
            ViewData["AdID"] = new SelectList(_context.SaleAds, "ID", "Brand", report.AdID);
            return View(report);
        }

        
        public async Task<IActionResult> Delete(int? id)
        {
            
            if (id == null || _context.Reports == null)
            {
                return NotFound();
            }

            var report = await _context.Reports
                .Include(r => r._Report_SaleAd)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (report == null)
            {
                return NotFound();
            }

            return View(report);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            
            if (_context.Reports == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Reports'  is null.");
            }
            var report = await _context.Reports.FindAsync(id);
            if (report != null)
            {
                _context.Reports.Remove(report);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReportExists(int id)
        {
          return (_context.Reports?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
