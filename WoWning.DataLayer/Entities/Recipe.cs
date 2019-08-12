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
    }
}