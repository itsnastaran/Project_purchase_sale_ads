using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain;
using Projectcore.Data;
using X.PagedList.Mvc;
using X.PagedList.Mvc.Core;
using X.PagedList;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Projectcore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "mainadmin")]
    public class SaleAdsController : BaseAdminController
    {
        private readonly ApplicationDbContext _context;

        public SaleAdsController(ApplicationDbContext context)
            : base(context)
        {
            _context = context;
        }
        
       
        public async Task<IActionResult> Index(int? page)
        {
           
            var pagenumber = page ?? 1;
            int pagesize = 3;
            var applicationDbContext = _context.SaleAds.Include(s => s._SaleAd_Categories).Include(s => s._SaleAd_ProductType).Include(s => s._SaleAd_SubCategory).Include(s => s._SaleAd_TypeOfAd);
            return View(await applicationDbContext.ToPagedListAsync(pagenumber,pagesize));
        }

        
        public async Task<IActionResult> Delete(int? id)
        {
          
            
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var saleAd = await _context.SaleAds

                .FirstOrDefaultAsync(m => m.ID == id);
            if (saleAd == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(saleAd);
        }

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            
            
            var saleAd = await _context.SaleAds.FindAsync(id);
            var res = _context.CategoryDetailsAd.Where(x => x.IDAd == id).ToList();

            _context.CategoryDetailsAd.RemoveRange(res);
            await _context.SaveChangesAsync();

            _context.SaleAds.Remove(saleAd);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> IsActive(int id)
        {
            
            var saleAd = await _context.SaleAds

                .FirstOrDefaultAsync(m => m.ID == id);
            if (saleAd == null)
            {
                return RedirectToAction(nameof(Index));
            }

            if (saleAd.Status == true)
            {
                saleAd.Status = false;
                _context.Update(saleAd);
                await _context.SaveChangesAsync();
            }
            else
            {
                saleAd.Status = true;
                _context.Update(saleAd);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
        private bool SaleAdExists(int id)
        {
            return _context.SaleAds.Any(e => e.ID == id);
        }
    }
}
