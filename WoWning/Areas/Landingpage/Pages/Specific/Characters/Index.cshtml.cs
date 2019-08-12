using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WoWning.DataLayer.Entities;
using WoWning.DataLayer;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using Microsoft.Extensions.Localization;
using WoWning.Helpers;

namespace WoWning.Areas.Landingpage.Pages.Specific.Characters
{
    public class IndexModel : PageModel
    {
        private readonly DatabaseContext _context;
        private readonly UserManager<WoWUser> _userManager;
        private readonly IStringLocalizer<Character> _localizer;

        public string NameSort { get; set; }
        public string ServerSort { get; set; }
        public string SideSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public int PageSize = 5;

        public IndexModel(DatabaseContext context, UserManager<WoWUser> userManager, IStringLocalizer<Character> localizer)
        {
            _context = context;
            _userManager = userManager;
            _localizer = localizer;
        }

        public PaginatedList<Character> Character { get;set; }

        public async Task OnGetAsync(string sortOrder, string currentFilter, string searchString, int? pageIndex)
        {
            CurrentSort = sortOrder;
            NameSort = string.IsNullOrEmpty(sortOrder) ? "name_desc" : string.Empty;
            ServerSort = sortOrder == "server" ? "server_desc" : "server";
            SideSort = sortOrder == "side" ? "side_desc" : "side";
            CurrentFilter = searchString;
            if (searchString != null)
                pageIndex = 1;
            else
                searchString = currentFilter;

            var user = await _userManager.GetUserAsync(User);

            var query = _context.Characters.AsNoTracking();
            if (!string.IsNullOrEmpty(searchString))
                query = query.Where(s => s.Name.Contains(searchString));

            switch (sortOrder)
            {
                case "name_desc":
                    query = query.OrderByDescending(s => s.Name);
                    break;
                case "server":
                    query = query.OrderBy(s => s.Server);
                    break;
                case "server_desc":
                    query = query.OrderByDescending(s => s.Server);
                    break;
                case "side":
                    query = query.OrderBy(s => s.Side);
                    break;
                case "side_desc":
                    query = query.OrderByDescending(s => s.Side);
                    break;
                default:
                    query = query.OrderBy(s => s.Name);
                    break;
            }

            query = query.Where(x => x.UserId == user.Id);
            var characters = await PaginatedList<Character>.CreateAsync(query, pageIndex ?? 1, PageSize);

            foreach (var chara in characters)
                chara.Map(_localizer);

            Character = characters;
        }
    }
}