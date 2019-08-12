using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WoWning.DataLayer.Entities;
using WoWning.DataLayer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Localization;

namespace WoWning.Areas.Landingpage.Pages.Specific.Characters
{
    public class EditModel : PageModel
    {
        private readonly DatabaseContext _context;
        private readonly UserManager<WoWUser> _userManager;
        private readonly IHtmlLocalizer<EditModel> _localizer;

        public EditModel(DatabaseContext context, UserManager<WoWUser> userManager, IHtmlLocalizer<EditModel> localizer)
        {
            _context = context;
            _userManager = userManager;
            _localizer = localizer;
        }

        [BindProperty]
        public Character Character { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            var user = await _userManager.GetUserAsync(User);
            Character = await _context.Characters.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id && m.UserId == user.Id);

            if (Character == null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            
            var user = await _userManager.GetUserAsync(User);
            var oldCharacter = await _context.Characters.FirstOrDefaultAsync(m => m.Id == Character.Id);
            if (oldCharacter.UserId == user.Id)
            {
                if (await TryUpdateModelAsync(
                    oldCharacter,
                    "character",
                    s => s.Name, s => s.Server, s => s.Side, s => s.IsAlchemist, s => s.IsArmorsmith, s => s.IsAxesmith, s => s.IsBlacksmith, 
                    s => s.IsCook, s => s.IsDragonscale, s => s.IsElemental, s => s.IsEnchanter, s => s.IsEngineer, s => s.IsFirstAid,
                    s => s.IsGnomish, s => s.IsGoblin, s => s.IsLeather, s => s.IsMacesmith, s => s.IsSwordsmith, s => s.IsTailor, s => s.IsTribal,
                    s => s.IsWeaponsmith))
                {
                    if (oldCharacter.IsElemental)
                        oldCharacter.IsLeather = true;

                    if (oldCharacter.IsTribal)
                        oldCharacter.IsLeather = true;

                    if (oldCharacter.IsDragonscale)
                        oldCharacter.IsLeather = true;

                    if (oldCharacter.IsGnomish)
                        oldCharacter.IsEngineer = true;

                    if (oldCharacter.IsGoblin)
                        oldCharacter.IsEngineer = true;

                    if (oldCharacter.IsArmorsmith)
                        oldCharacter.IsBlacksmith = true;

                    if (oldCharacter.IsAxesmith)
                        oldCharacter.IsWeaponsmith = true;

                    if (oldCharacter.IsSwordsmith)
                        oldCharacter.IsWeaponsmith = true;

                    if (oldCharacter.IsMacesmith)
                        oldCharacter.IsWeaponsmith = true;

                    if (oldCharacter.IsWeaponsmith)
                        oldCharacter.IsBlacksmith = true;

                    var numberMain = 0;
                    if (oldCharacter.IsBlacksmith)
                        numberMain++;
                    if (oldCharacter.IsAlchemist)
                        numberMain++;
                    if (oldCharacter.IsTailor)
                        numberMain++;
                    if (oldCharacter.IsEnchanter)
                        numberMain++;
                    if (oldCharacter.IsEngineer)
                        numberMain++;
                    if (oldCharacter.IsLeather)
                        numberMain++;

                    if (numberMain > 2)
                    {
                        ModelState.AddModelError(string.Empty, _localizer.GetString("Only two main professions are allowed."));
                        return Page();
                    }

                    try
                    {
                        await DeleteOldRecipes(oldCharacter);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!CharacterExists(Character.Id))
                            return NotFound();
                        else
                            throw;
                    }
                }
            }

            return RedirectToPage("./Index");
        }

        private async Task DeleteWhenFalse(bool check, int category)
        {
            if (!check)
            {
                var recipes = await _context.CharacterRecipes.Where(x => x.Recipe.Category == category).ToListAsync();
                _context.CharacterRecipes.RemoveRange(recipes);
            }
        }

        private async Task DeleteOldRecipes(Character oldCharacter)
        {
            await DeleteWhenFalse(oldCharacter.IsLeather, 1);
            await DeleteWhenFalse(oldCharacter.IsDragonscale, 2);
            await DeleteWhenFalse(oldCharacter.IsElemental, 3);
            await DeleteWhenFalse(oldCharacter.IsTribal, 4);
            await DeleteWhenFalse(oldCharacter.IsAlchemist, 5);
            await DeleteWhenFalse(oldCharacter.IsBlacksmith, 6);
            await DeleteWhenFalse(oldCharacter.IsWeaponsmith, 7);
            await DeleteWhenFalse(oldCharacter.IsArmorsmith, 8);
            await DeleteWhenFalse(oldCharacter.IsAxesmith, 9);
            await DeleteWhenFalse(oldCharacter.IsSwordsmith, 10);
            await DeleteWhenFalse(oldCharacter.IsMacesmith, 11);
            await DeleteWhenFalse(oldCharacter.IsEnchanter, 12);
            await DeleteWhenFalse(oldCharacter.IsTailor, 13);
            await DeleteWhenFalse(oldCharacter.IsEngineer, 14);
            await DeleteWhenFalse(oldCharacter.IsGoblin, 15);
            await DeleteWhenFalse(oldCharacter.IsGnomish, 16);
            await DeleteWhenFalse(oldCharacter.IsCook, 17);
            await DeleteWhenFalse(oldCharacter.IsFirstAid, 18);
        }

        private bool CharacterExists(int id)
        {
            return _context.Characters.Any(e => e.Id == id);
        }
    }
}