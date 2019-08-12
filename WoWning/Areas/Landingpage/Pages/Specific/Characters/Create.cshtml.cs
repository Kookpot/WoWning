using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WoWning.DataLayer.Entities;
using WoWning.DataLayer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Localization;

namespace WoWning.Areas.Landingpage.Pages.Specific.Characters
{
    public class CreateModel : PageModel
    {
        private readonly DatabaseContext _context;
        private readonly UserManager<WoWUser> _userManager;
        private readonly IHtmlLocalizer<CreateModel> _localizer;

        public CreateModel(DatabaseContext context, UserManager<WoWUser> userManager, IHtmlLocalizer<CreateModel> localizer)
        {
            _context = context;
            _userManager = userManager;
            _localizer = localizer;
        }

        public IActionResult OnGet() => Page();

        [BindProperty]
        public Character Character { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            if (Character.IsElemental)
                Character.IsLeather = true;

            if (Character.IsTribal)
                Character.IsLeather = true;

            if (Character.IsDragonscale)
                Character.IsLeather = true;

            if (Character.IsGnomish)
                Character.IsEngineer = true;

            if (Character.IsGoblin)
                Character.IsEngineer = true;

            if (Character.IsArmorsmith)
                Character.IsBlacksmith = true;

            if (Character.IsAxesmith)
                Character.IsWeaponsmith = true;

            if (Character.IsSwordsmith)
                Character.IsWeaponsmith = true;

            if (Character.IsMacesmith)
                Character.IsWeaponsmith = true;

            if (Character.IsWeaponsmith)
                Character.IsBlacksmith = true;

            var numberMain = 0;
            if (Character.IsBlacksmith)
                numberMain++;
            if (Character.IsAlchemist)
                numberMain++;
            if (Character.IsTailor)
                numberMain++;
            if (Character.IsEnchanter)
                numberMain++;
            if (Character.IsEngineer)
                numberMain++;
            if (Character.IsLeather)
                numberMain++;

            if (numberMain > 2)
            {
                ModelState.AddModelError(string.Empty, _localizer.GetString("Only two main professions are allowed."));
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            Character.UserId = user.Id;
            _context.Characters.Add(Character);

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}