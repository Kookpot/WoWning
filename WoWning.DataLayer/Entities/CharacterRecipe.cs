using System.ComponentModel.DataAnnotations;

namespace WoWning.DataLayer.Entities
{
    public class CharacterRecipe
    {
        public int CharacterId { get; set; }
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
        public Character Character { get; set; }
        public int Silver { get; set; }
        public int Gold { get; set; }
        public int Copper { get; set; }
    }
}