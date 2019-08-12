using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WoWning.DataLayer.Entities;
using WoWning.DataLayer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace WoWning.Areas.Landingpage.Pages.Specific.Characters
{
    public class DetailsModel : PageModel
    {
        private readonly DatabaseContext _context;
        private readonly UserManager<WoWUser> _userManager;
        private readonly IStringLocalizer<Character> _localizer;

        public DetailsModel(DatabaseContext context, UserManager<WoWUser> userManager, IStringLocalizer<Character> localizer)
        {
            _context = context;
            _userManager = userManager;
            _localizer = localizer;
        }

        public Character Character { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            var user = await _userManager.GetUserAsync(User);
            var character = await _context.Characters.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id && m.UserId == user.Id);
            character.Map(_localizer);
            Character = character;

            if (Character == null)
                return NotFound();

            return Page();
        }
    }
}