using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Projectcore.Controllers;
using Projectcore.Data;
using Projectcore.Models;
using System.Security.Claims;

namespace Projectcore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "mainadmin")]
    public class AdminController : BaseController
    {
        
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _db;
        public AdminController(UserManager<ApplicationUser> userManager,IUnitOfWork db)
            :base(db)
        {
            _userManager = userManager;
            _db = db;

        }

        public async Task<IActionResult> Index()
        {
            if(User.Identity.IsAuthenticated)
            {
               
                var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var user = _db.ApplicationUserRepository.GetBYID(userid);
               
                ViewData["FullName"] = user.NameFamily;
            }
            var fav = _db.FavortiesRepository.GetByList().Count();
            ViewData["fav"] = fav;
            var salead = _db.SaleAdRepository.GetByList().Count();
            ViewData["salead"] = salead;
            var users = _db.ApplicationUserRepository.GetByList().Count();
            ViewData["users"] = users;
            var newusalead = _db.SaleAdRepository.GetByList().OrderByDescending(x => x.Date).Take(4);
            ViewData["newusalead"] = newusalead;
            return View();
        }
    }

}