using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WoWning.DataLayer.Entities
{
    public class Character
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string UserId { get; set; }
        public WoWUser User { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [Display(Name = "Server")]
        public int Server { get; set; }

        [Display(Name = "Server")]
        [NotMapped]
        public string ServerName { get
            {
                switch(Server)
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
        public int Side { get; set; }
        public ICollection<CharacterRecipe> CharacterRecipes { get; set; }

        [Display(Name = "Side")]
        [NotMapped]
        public string SideName { get; set; }

        [Display(Name = "Professions")]
        [NotMapped]
        public string Professions { get; set; }

        [Display(Name = "Leatherworking")]
        public bool IsLeather { get; set; }
        [Display(Name = "Enchanting")]
        public bool IsEnchanter { get; set; }
        [Display(Name = "Engineering")]
        public bool IsEngineer { get; set; }
        [Display(Name = "Alchemy")]
        public bool IsAlchemist { get; set; }
        [Display(Name = "Cooking")]
        public bool IsCook { get; set; }
        [Display(Name = "First Aid")]
        public bool IsFirstAid { get; set; }
        [Display(Name = "Tailoring")]
        public bool IsTailor { get; set; }
        [Display(Name = "Blacksmithing")]
        public bool IsBlacksmith { get; set; }
        [Display(Name = "Weaponsmithing")]
        public bool IsWeaponsmith { get; set; }
        [Display(Name = "Armorsmithing")]
        public bool IsArmorsmith { get; set; }
        [Display(Name = "Swordsmithing")]
        public bool IsSwordsmith { get; set; }
        [Display(Name = "Axesmithing")]
        public bool IsAxesmith { get; set; }
        [Display(Name = "Macesmithing")]
        public bool IsMacesmith { get; set; }
        [Display(Name = "Goblin Engineering")]
        public bool IsGoblin { get; set; }
        [Display(Name = "Gnomish Engineering")]
        public bool IsGnomish { get; set; }
        [Display(Name = "Elemental leatherworking")]
        public bool IsElemental { get; set; }
        [Display(Name = "Dragonscale leatherworking")]
        public bool IsDragonscale { get; set; }
        [Display(Name = "Tribal leatherworking")]
        public bool IsTribal { get; set; }

        public void Map(IStringLocalizer<Character> localizer)
        {
            switch (Side)
            {
                case 1:
                    SideName = localizer.GetString("Horde");
                    break;
                default:
                    SideName = localizer.GetString("Alliance");
                    break;
            }
            var prof = new List<string>();
            if (IsGnomish)
                prof.Add(localizer.GetString("Gnomish Engineering"));

            if (IsGoblin)
                prof.Add(localizer.GetString("Goblin Engineering"));

            if (!IsGoblin && !IsGnomish && IsEngineer)
                prof.Add(localizer.GetString("Engineering"));

            if (IsTribal)
                prof.Add(localizer.GetString("Tribal leatherworking"));

            if (IsElemental)
                prof.Add(localizer.GetString("Elemental leatherworking"));

            if (IsDragonscale)
                prof.Add(localizer.GetString("Dragonscale leatherworking"));

            if (!IsElemental && !IsDragonscale && !IsTribal && IsLeather)
                prof.Add(localizer.GetString("Leatherworking"));

            if (IsSwordsmith)
                prof.Add(localizer.GetString("Swordsmithing"));

            if (IsAxesmith)
                prof.Add(localizer.GetString("Axesmithing"));

            if (IsMacesmith)
                prof.Add(localizer.GetString("Macesmithing"));

            if (!IsSwordsmith && !IsAxesmith && !IsMacesmith && IsWeaponsmith)
                prof.Add(localizer.GetString("Weaponsmithing"));

            if (IsArmorsmith)
                prof.Add(localizer.GetString("Armorsmithing"));

            if (IsAlchemist)
                prof.Add(localizer.GetString("Alchemy"));

            if (IsEnchanter)
                prof.Add(localizer.GetString("Enchanting"));

            if (IsTailor)
                prof.Add(localizer.GetString("Tailoring"));

            if (IsCook)
                prof.Add(localizer.GetString("Cooking"));

            if (IsFirstAid)
                prof.Add(localizer.GetString("First Aid"));

            Professions = string.Join(",", prof);
        }
    }
}