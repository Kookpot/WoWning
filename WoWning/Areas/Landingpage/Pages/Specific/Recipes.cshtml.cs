using WoWning.DataLayer;
using WoWning.DataLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WoWning.Areas.Landingpage.Pages.Specific
{
    public class RecipesModel : PageModel
    {
        private readonly DatabaseContext _context;
        private readonly UserManager<WoWUser> _userManager;
        private readonly IHtmlLocalizer<RecipesModel> _localizer;

        public RecipesModel(DatabaseContext context, UserManager<WoWUser> userManager,
            IHtmlLocalizer<RecipesModel> localizer)
        {
            _context = context;
            _userManager = userManager;
            _localizer = localizer;
        }

        public string Character { get; set; }
        public List<SelectListItem> Options { get; set; }
        public string Profession { get; set; }
        public List<SelectListItem> OptionsProfessions { get; set; }

        public string NameSort { get; set; }
        public string LevelSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        [BindProperty]
        public List<Recipe> Recipes { get; set; } = new List<Recipe>();

        public async Task<IActionResult> OnGet(string character = null, string profession = null, string sortOrder = null, string currentFilter = null, string searchString = null)
        {
            CurrentSort = sortOrder;
            LevelSort = string.IsNullOrEmpty(sortOrder) ? "level_desc" : string.Empty;
            NameSort = sortOrder == "name" ? "name_desc" : "name";
            CurrentFilter = searchString;
            if (searchString == null)
                searchString = currentFilter;

            Character = character;
            Profession = profession;

            var user = await _userManager.GetUserAsync(User);
            Options = await _context.Characters.AsNoTracking().Where(x => x.UserId == user.Id).Select(a =>
                                  new SelectListItem
                                  {
                                      Value = a.Id.ToString(),
                                      Text = $"{a.Name} - {a.ServerName}"
                                  }).ToListAsync();

            if (!string.IsNullOrEmpty(character))
            {
                Character characterDB;

                if (string.IsNullOrEmpty(profession))
                {
                    characterDB = await _context.Characters
                        .AsNoTracking()
                        .Where(x => x.UserId == user.Id && x.Id == int.Parse(character))
                        .FirstOrDefaultAsync();

                }
                else
                {
                    characterDB = await _context.Characters
                        .Include(x => x.CharacterRecipes)
                        .AsNoTracking()
                        .Where(x => x.UserId == user.Id && x.Id == int.Parse(character))
                        .FirstOrDefaultAsync();
                }

                if (characterDB != null)
                {
                    OptionsProfessions = new List<SelectListItem>();
                    if (characterDB.IsLeather)
                        OptionsProfessions.Add(new SelectListItem(_localizer.GetString("Leatherworker"), "1"));

                    if (characterDB.IsAlchemist)
                        OptionsProfessions.Add(new SelectListItem(_localizer.GetString("Alchemist"), "5"));

                    if (characterDB.IsBlacksmith)
                        OptionsProfessions.Add(new SelectListItem(_localizer.GetString("Blacksmithing"), "6"));

                    if (characterDB.IsEnchanter)
                        OptionsProfessions.Add(new SelectListItem(_localizer.GetString("Enchanting"), "12"));

                    if (characterDB.IsTailor)
                        OptionsProfessions.Add(new SelectListItem(_localizer.GetString("Tailoring"), "13"));

                    if (characterDB.IsEngineer)
                        OptionsProfessions.Add(new SelectListItem(_localizer.GetString("Engineering"), "14"));

                    if (characterDB.IsCook)
                        OptionsProfessions.Add(new SelectListItem(_localizer.GetString("Cooking"), "17"));

                    if (characterDB.IsFirstAid)
                        OptionsProfessions.Add(new SelectListItem(_localizer.GetString("First Aid"), "18"));

                    var query = _context.Recipes.AsNoTracking();
                    if (!string.IsNullOrEmpty(searchString))
                        query = query.Where(s => s.Name.Contains(searchString));


                    if (!string.IsNullOrEmpty(profession))
                    {
                        var recipes = new List<Recipe>();
                        if (profession == "1" && characterDB.IsLeather)
                        {
                            recipes.AddRange(await query.Where(x => x.Category == 1).ToListAsync());
                            if (characterDB.IsDragonscale)
                                recipes.AddRange(await query.Where(x => x.Category == 2).ToListAsync());

                            if (characterDB.IsElemental)
                                recipes.AddRange(await query.Where(x => x.Category == 3).ToListAsync());

                            if (characterDB.IsTribal)
                                recipes.AddRange(await query.Where(x => x.Category == 4).ToListAsync());
                        }

                        if (profession == "5" && characterDB.IsAlchemist)
                            recipes.AddRange(await query.Where(x => x.Category == 5).ToListAsync());

                        if (profession == "6" && characterDB.IsBlacksmith)
                        {
                            recipes.AddRange(await query.Where(x => x.Category == 6).ToListAsync());

                            if (characterDB.IsWeaponsmith)
                                recipes.AddRange(await query.Where(x => x.Category == 7).ToListAsync());

                            if (characterDB.IsArmorsmith)
                                recipes.AddRange(await query.Where(x => x.Category == 8).ToListAsync());

                            if (characterDB.IsAxesmith)
                                recipes.AddRange(await query.Where(x => x.Category == 9).ToListAsync());

                            if (characterDB.IsSwordsmith)
                                recipes.AddRange(await query.Where(x => x.Category == 10).ToListAsync());

                            if (characterDB.IsMacesmith)
                                recipes.AddRange(await query.Where(x => x.Category == 11).ToListAsync());
                        }

                        if (profession == "12" && characterDB.IsEnchanter)
                            recipes.AddRange(await query.Where(x => x.Category == 12).ToListAsync());

                        if (profession == "13" && characterDB.IsTailor)
                            recipes.AddRange(await query.Where(x => x.Category == 13).ToListAsync());

                        if (profession == "14" && characterDB.IsEngineer)
                        {
                            recipes.AddRange(await query.Where(x => x.Category == 14).ToListAsync());

                            if (characterDB.IsGoblin)
                                recipes.AddRange(await query.Where(x => x.Category == 15).ToListAsync());

                            if (characterDB.IsGnomish)
                                recipes.AddRange(await query.Where(x => x.Category == 16).ToListAsync());
                        }

                        if (profession == "17" && characterDB.IsCook)
                            recipes.AddRange(await query.Where(x => x.Category == 17).ToListAsync());

                        if (profession == "18" && characterDB.IsFirstAid)
                            recipes.AddRange(await query.Where(x => x.Category == 18).ToListAsync());

                        foreach (var rec in recipes)
                        {
                            var found = characterDB.CharacterRecipes.SingleOrDefault(y => y.RecipeId == rec.Id);
                            if (found != null)
                            {
                                rec.IsSelected = true;
                                rec.Gold = found.Gold;
                                rec.Silver = found.Silver;
                                rec.Copper = found.Copper;
                            }
                        }

                        switch (sortOrder)
                        {
                            case "level_desc":
                                recipes = recipes.OrderByDescending(s => s.Level).ToList();
                                break;
                            case "name":
                                recipes = recipes.OrderBy(s => s.Name).ToList();
                                break;
                            case "name_desc":
                                recipes = recipes.OrderByDescending(s => s.Name).ToList();
                                break;
                            default:
                                recipes = recipes.OrderBy(s => s.Level).ToList();
                                break;
                        }
                        Recipes = recipes;
                    }
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string character, string profession, string sortOrder = null, string currentFilter = null, string searchString = null)
        {
            var user = await _userManager.GetUserAsync(User);
            var characterDB = await _context.Characters
                .Include(x => x.CharacterRecipes)
                .Where(x => x.UserId == user.Id && x.Id == int.Parse(character))
                .FirstOrDefaultAsync();

            if (characterDB != null)
            {
                foreach (var data in Recipes)
                {
                    var found = characterDB.CharacterRecipes.FirstOrDefault(x => x.RecipeId == data.Id);
                    if (found == null && data.IsSelected)
                        characterDB.CharacterRecipes.Add(new CharacterRecipe { CharacterId = int.Parse(character), RecipeId = data.Id, Copper = data.Copper, Silver = data.Silver, Gold = data.Gold });
                    else if (found != null && !data.IsSelected)
                        characterDB.CharacterRecipes.Remove(found);
                    else if (found != null)
                    {
                        found.Copper = data.Copper;
                        found.Silver = data.Silver;
                        found.Gold = data.Gold;
                    }
                }
                await _context.SaveChangesAsync();
            }

            return await OnGet(character, profession, sortOrder, currentFilter, searchString);
        }
    }
}