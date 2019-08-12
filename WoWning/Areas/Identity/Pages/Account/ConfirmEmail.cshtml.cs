using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using WoWning.DataLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Localization;

namespace WoWning.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ConfirmEmailModel : PageModel
    {
        private readonly UserManager<WoWUser> _userManager;
        private readonly SignInManager<WoWUser> _signInManager;
        private readonly IHtmlLocalizer<ConfirmEmailModel> _localizer;

        public ConfirmEmailModel(UserManager<WoWUser> userManager, SignInManager<WoWUser> signInManager,
            IHtmlLocalizer<ConfirmEmailModel> localizer)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _localizer = localizer;
        }

        public async Task<IActionResult> OnGetAsync(string userId, string code, bool isDone = false)
        {
            if (isDone)
                return Page();

            if (userId == null || code == null)
                return LocalRedirect("/Landingpage/Main/Index");

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound(_localizer.GetString("Unable to load user with ID '{0}'.", _userManager.GetUserId(User)));

            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (!result.Succeeded)
                throw new InvalidOperationException($"Error confirming email for user with ID '{userId}':");           
            else
            {
                await _signInManager.SignInAsync(user, false);
                return RedirectToPage(new { isDone = true });
            }
        }
    }
}