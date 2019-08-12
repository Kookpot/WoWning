using System.ComponentModel.DataAnnotations;

namespace WoWning.Areas.Landingpage.Pages.Specific
{
    public class SearchItem
    {
        public string CurrentSort { get; set; }
        public string TipSort { get; set; }
        public string RecommendationSort { get; set; }
        public int PageSize = 10;
        public int? PageIndex { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [Display(Name = "Server")]
        public int Server { get; set; }

        [Display(Name = "Server")]
        public string ServerName
        {
            get
            {
                switch (Server)
                {
                    case 1:
                        return "Molten Core";
                    case 2:
                        return "Winterspring";
                    case 3:
                        return "Gadzet";
                    default:
                        return "Kazaak";
                }
            }
        }

        [Required(ErrorMessage = "The {0} field is required.")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [Display(Name = "Side")]
        public int? Side { get; set; }
    }
}