using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WoWning.DataLayer.Entities;
using WoWning.DataLayer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Mvc.Localization;

namespace WoWning.Areas.Landingpage.Pages.Specific.Characters
{
    public class DeleteModel : PageModel
    {
        private readonly DatabaseContext _context;
        private readonly UserManager<WoWUser> _userManager;
        private readonly IStringLocalizer<Character> _localizer;
        private readonly IHtmlLocalizer<DeleteModel> _myLocalizer;

        public DeleteModel(DatabaseContext context, UserManager<WoWUser> userManager, IStringLocalizer<Character> localizer,
            IHtmlLocalizer<DeleteModel> myLocalizer)
        {
            _context = context;
            _userManager = userManager;
            _localizer = localizer;
            _myLocalizer = myLocalizer;
        }

        [BindProperty]
        public Character Character { get; set; }
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, bool? saveChangesError = false)
        {
            if (id == null)
                return NotFound();

            var user = await _userManager.GetUserAsync(User);
            var character = await _context.Characters.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id && m.UserId == user.Id);
            character.Map(_localizer);
            Character = character;

            if (Character == null)
                return NotFound();

            if (saveChangesError.GetValueOrDefault())
                ErrorMessage = _myLocalizer.GetString("Delete failed. Try again");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
                return NotFound();

            var user = await _userManager.GetUserAsync(User);

            var character = await _context.Characters
                    .AsNoTracking()
                    .FirstOrDefaultAsync(m => m.Id == id);

            if (character == null || character.UserId == user.Id)
                return NotFound();

            try
            {
                _context.Characters.Remove(Character);
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction("./Delete", new { id, saveChangesError = true });
            }
        }
    }
}