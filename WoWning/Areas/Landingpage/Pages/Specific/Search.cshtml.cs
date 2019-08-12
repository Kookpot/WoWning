using WoWning.DataLayer;
using WoWning.DataLayer.Entities;
using WoWning.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace WoWning.Areas.Landingpage.Pages.Specific
{
    public class SearchModel : PageModel
    {
        [BindProperty]
        public SearchItem SearchItem { get; set; }

        public Recipe Recipe { get; set; }

        public PaginatedList<CharacterRecipe> Recipes { get; set; }

        private readonly DatabaseContext _context;
        private readonly UserManager<WoWUser> _userManager;
        private readonly IHtmlLocalizer<SearchModel> _localizer;

        public SearchModel(DatabaseContext context, IHtmlLocalizer<SearchModel> localizer, UserManager<WoWUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            _localizer = localizer;
        }

        public async Task<JsonResult> OnGetRecipeListAsync(string text = null)
        {
            text = text.ToLower().Trim();
            var str = await _context
                .Recipes.Where(x => x.Name.ToLower().StartsWith(text))
                .Select(x => x.Name)
                .ToListAsync();

            return new JsonResult(str);
        }

        public IActionResult OnGet()
        {
            SearchItem = new SearchItem { TipSort = "tip", CurrentSort = "tip", PageIndex = 1 };
            return Page();
        }

        public async Task<IActionResult> OnGetStoreUpAsync(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var otherUser = _context.Characters.Include(x => x.User).FirstOrDefault(x => x.Id == id);
            await _context.Recommendations.AddAsync(new Recommendation { Date = DateTime.Now, TakerId = otherUser.User.Id, GiverId = user.Id });
            otherUser.User.PositiveRecommendations++;
            await _context.SaveChangesAsync();
            return new OkResult();
        }

        public async Task<IActionResult> OnGetStoreDown(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var otherUser = _context.Characters.Include(x => x.User).FirstOrDefault(x => x.Id == id);
            await _context.Recommendations.AddAsync(new Recommendation { Date = DateTime.Now, TakerId = otherUser.User.Id, GiverId = user.Id });
            otherUser.User.NegativeRecommendations++;
            await _context.SaveChangesAsync();
            return new OkResult();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var user = await _userManager.GetUserAsync(User);

            Recipe = _context.Recipes
                .AsNoTracking()
                .Include(x => x.RecipeMaterials)
                .FirstOrDefault(x => x.Name == SearchItem.Name);

            var recipesQuery = _context.CharacterRecipes
                .AsNoTracking()
                .Include(x => x.Character)
                .ThenInclude(x => x.User)
                .ThenInclude(x => x.Characters)
                .Where(x => x.Recipe.Name == SearchItem.Name && x.Character.Server == SearchItem.Server && x.Character.Side == SearchItem.Side && x.Character.UserId != user.Id);

            if (SearchItem.TipSort != SearchItem.CurrentSort)
            {
                SearchItem.CurrentSort = SearchItem.TipSort;
                SearchItem.RecommendationSort = string.Empty;
            }
            else if (SearchItem.RecommendationSort != null && SearchItem.RecommendationSort != SearchItem.CurrentSort)
            {
                SearchItem.CurrentSort = SearchItem.RecommendationSort;
                SearchItem.TipSort = string.Empty;
            }
            SearchItem = new SearchItem {
                CurrentSort = SearchItem.CurrentSort,
                Name = SearchItem.Name,
                PageIndex = SearchItem.PageIndex,
                RecommendationSort = SearchItem.RecommendationSort,
                Server = SearchItem.Server, Side = SearchItem.Side, TipSort = SearchItem.TipSort };

            if (SearchItem.CurrentSort == "tip")
                recipesQuery = recipesQuery.OrderBy(x => x.Gold).ThenBy(x => x.Silver).ThenBy(x => x.Copper);
            else if (SearchItem.CurrentSort == "tip_desc")
                recipesQuery = recipesQuery.OrderByDescending(x => x.Gold).ThenByDescending(x => x.Silver).ThenByDescending(x => x.Copper);
            else if (SearchItem.CurrentSort == "recc")
                recipesQuery = recipesQuery.OrderByDescending(x => x.Character.User.Recommendations);
            else if (SearchItem.CurrentSort == "recc_desc")
                recipesQuery = recipesQuery.OrderBy(x => x.Character.User.Recommendations);

            Recipes = await PaginatedList<CharacterRecipe>.CreateAsync(recipesQuery, SearchItem.PageIndex ?? 1, SearchItem.PageSize);

            foreach (var rec in Recipes)
            {
                rec.Character.User.Characters = rec.Character.User.Characters.Where(x => x.Id != rec.CharacterId && x.Server == SearchItem.Server && x.Side == SearchItem.Side).ToList();
                rec.Character.User.CanRecommend = !_context.Recommendations.Any(x => x.GiverId == user.Id && x.TakerId == rec.Character.User.Id && x.Date >= DateTime.Now.AddDays(-7));
            }

            return Page();
        }
    }
}