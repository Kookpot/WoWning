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
                        return "Arugal";
                    case 2:
                        return "Atiesh";
                    case 3:
                        return "Auberdine";
                    case 4:
                        return "Blaumeux";
                    case 5:
                        return "Bloodsail Buccaneers";
                    case 6:
                        return "ХРОМИ (Chromie)";
                    case 7:
                        return "Everlook";
                    case 8:
                        return "Faerlina";
                    case 9:
                        return "Fairbanks";
                    case 10:
                        return "ПЛАМЕГОР (Flamegor)";
                    case 11:
                        return "Gehennas";
                    case 12:
                        return "Golemagg";
                    case 13:
                        return "Grobbulus";
                    case 14:
                        return "Herod";
                    case 15:
                        return "Hydraxian Waterlords";
                    case 16:
                        return "Lucifron";
                    case 17:
                        return "Mankrik";
                    case 18:
                        return "Mirage Raceway";
                    case 19:
                        return "Myzrael";
                    case 20:
                        return "Pagle";
                    case 21:
                        return "Pyrewood Village";
                    case 22:
                        return "Razorfen";
                    case 23:
                        return "Remulos";
                    case 24:
                        return "Shazzrah";
                    case 25:
                        return "Skeram";
                    case 26:
                        return "Stalagg";
                    case 27:
                        return "Sulfuron";
                    case 28:
                        return "Thalnos";
                    case 29:
                        return "Venoxis";
                    case 30:
                        return "Whitemane";
                    case 31:
                        return "Zandalar Tribe";
                    case 32:
                        return "Flamelash";
                    case 33:
                        return "Gandling";
                    case 34:
                        return "Mograine";
                    case 35:
                        return "Nethergarde Keep";
                    case 36:
                        return "Razorgore";
                    case 37:
                        return "Old Blanchy";
                    case 38:
                        return "Westfall";
                    case 39:
                        return "Bigglesworth";
                    case 40:
                        return "Incendius";
                    case 41:
                        return "Ashkandi";
                    case 42:
                        return "Kirtonos";
                    case 43:
                        return "Kurinaxx";
                    case 44:
                        return "Kromcrush";
                    case 45:
                        return "Rattlegore";
                    case 46:
                        return "Smolderweb";
                    case 47:
                        return "Sulfuras";
                    case 48:
                        return "Firemaw";
                    case 49:
                        return "Noggenfogger";
                    case 50:
                        return "Stonespine";
                    case 51:
                        return "Amnennar";
                    case 52:
                        return "Змейталак";
                    case 53:
                        return "Felstriker";
                    case 54:
                        return "Yojamba";
                    case 55:
                        return "Deviant Delight";
                    default:
                        return "Zandalar Tribe";
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