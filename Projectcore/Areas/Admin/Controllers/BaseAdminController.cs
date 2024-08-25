using Microsoft.AspNetCore.Mvc;
using Projectcore.Data;
using System.Security.Claims;

namespace Projectcore.Areas.Admin.Controllers
{
    public class BaseAdminController:Controller
    {
        
        protected readonly ApplicationDbContext _context;
        public BaseAdminController(ApplicationDbContext context)
        {
            _context = context;
        }
        public override void OnActionExecuting(Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext filterContext)
        {

            
            var qq = _context.Menus.Where(x => x.Status == true).ToList();
            if (qq.Count() == 0)
            {
                ViewData["Menu"] = null;
                ViewData["ErrorMenu"] = "منو ثبت نشده است";
            }
            else
            {
                ViewData["Menu"] = qq;
            }
            if (User.Identity.IsAuthenticated)
            {

                var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var user = _context.Users.Find(userid);
                ViewData["FullName"] = user.NameFamily;
                ViewBag.FullName = user.NameFamily; 
            }
            ViewBag.Menu = qq;
            base.OnActionExecuting(filterContext);
        }
        
        
    }
}
