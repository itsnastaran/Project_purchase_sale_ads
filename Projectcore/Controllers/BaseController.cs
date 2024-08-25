using Microsoft.AspNetCore.Mvc;
using Projectcore.Data;
using Projectcore.Models;

namespace Projectcore.Controllers
{
    public class BaseController:Controller
    {
        protected readonly IUnitOfWork _db;
        public BaseController(IUnitOfWork db)
        {
            _db = db;
        }
        public override void OnActionExecuting(Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext filterContext)
        {
            
            var q = _db.MenuRepository.GetByList().Where(x => x.Status == true);
            if (q.Count() == 0)
            {
                ViewData["Menu"] = null;
                ViewData["ErrorMenu"] = "منو ثبت نشده است";
            }
            else
            {
                ViewData["Menu"] = q;
            }
            ViewBag.Menu = q; 
            base.OnActionExecuting(filterContext);
        }
    }
}
