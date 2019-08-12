using System.Threading.Tasks;
using WoWning.Areas.Identity.Pages.Account.Manage.Models;
using WoWning.DataLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WoWning.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<WoWUser> _userManager;
        private readonly SignInManager<WoWUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly IHtmlLocalizer<IndexModel> _localizer;

        public IndexModel(
            UserManager<WoWUser> userManager,
            SignInManager<WoWUser> signInManager,
            IEmailSender emailSender,
            IHtmlLocalizer<IndexModel> localizer)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _localizer = localizer;
        }

        public bool IsEmailConfirmed { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public IndexInputModel Input { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return NotFound(_localizer.GetString("Unable to load user with ID '{0}'.", _userManager.GetUserId(User)));

            var email = await _userManager.GetEmailAsync(user);

            Input = new IndexInputModel { Email = email };

            return Page();
        }
    }
}