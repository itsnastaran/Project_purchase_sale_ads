// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Projectcore.Data;
using Projectcore.Models;

namespace Projectcore.Areas.Identity.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LogoutModel> _logger;
        protected readonly IUnitOfWork _db;
        public LogoutModel(SignInManager<ApplicationUser> signInManager, ILogger<LogoutModel> logger,IUnitOfWork db)
        {
            _signInManager = signInManager;
            _logger = logger;
            _db = db;
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
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
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                // This needs to be a redirect so that the browser performs a new
                // request and the identity for the user gets updated.
                return RedirectToPage();
            }
        }
    }
}
