using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Projectcore.Data;
using System.Security.Claims;

namespace Projectcore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "mainadmin")]
    public class RolesController : BaseAdminController
    {
        RoleManager<IdentityRole> roleManager;
        UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext _context;
        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> _userManager,ApplicationDbContext context)
            :base(context)
        {
            this.roleManager = roleManager;
            this.userManager = _userManager;
            this._context = context;
        }
        
        public IActionResult Index()
        {
            
            var roles = roleManager.Roles.ToList();
            return View(roles);
        }
        public IActionResult Create()
        {
            
            return View(new IdentityRole());
        }
        [HttpPost]
        public async Task<IActionResult> Create(IdentityRole role)
        {
            
            await roleManager.CreateAsync(role);
            return RedirectToAction("Index");
        }
        public IActionResult Listuser()
        {
            
            var result = userManager.Users;
            ViewData["Listusers"] = result;
            return View();
        }
        public async Task<IActionResult> ListUserRole(string id)
        {
            
            var listrole = roleManager.Roles;
            var listuser = await userManager.FindByIdAsync(id);
            ViewData["RoleSelect"] = listrole;
            ViewData["UserSelect"] = listuser;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(string id,string role)
        {
            
            var user = await userManager.FindByIdAsync(id);
            if(user !=null)
            {
                List<string> lst = new List<string>();
                var roles = roleManager.Roles.ToList();
                foreach (var item in roles)
                {
                    var delete = await userManager.RemoveFromRoleAsync(user, item.NormalizedName);
                }
                
                var result = await userManager.AddToRoleAsync(user, role);
                return RedirectToAction(nameof(Listuser));
            }
            return RedirectToAction(nameof(ListUserRole));
        }
    }
}
