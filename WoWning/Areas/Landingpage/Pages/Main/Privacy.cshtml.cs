using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WoWning.Areas.Landingpage.Pages.Main
{
    [AllowAnonymous]
    public class PrivacyModel : PageModel
    {
        public void OnGet() { }
    }
}