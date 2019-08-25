using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WoWning.DataLayer.Entities
{
    public class Recipe
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public int ItemId { get; set; }
        public string Name { get; set; }
        public string FrenchName { get; set; }
        public string GermanName { get; set; }
        public string RussianName { get; set; }
        public ICollection<RecipeMaterial> RecipeMaterials { get; set; }
        public ICollection<CharacterRecipe> CharacterRecipes { get; set; }
        public int Category { get; set; }
        public int Level { get; set; }
        public int Amount { get; set; }
        public int? MaxAmount { get; set; }

        [NotMapped]
        public bool IsSelected { get; set; }
        [NotMapped]
        public int Gold { get; set; }
        [NotMapped]
        public int Silver { get; set; }
        [NotMapped]
        public int Copper { get; set; }

        [NotMapped]
        public string TransName
        {
            get
            {
                var t = System.Threading.Thread.CurrentThread.CurrentCulture;
                if (t.Name == "fr-FR")
                    return FrenchName;
                else if (t.Name == "de-DE")
                    return GermanName;
                else if (t.Name == "ru-RU")
                    return RussianName;
                else
                    return Name;
            }
        }
    }
}