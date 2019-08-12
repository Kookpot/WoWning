namespace WoWning.DataLayer.Entities
{
    public class RecipeMaterial
    {
        public int RecipeId { get; set; }
        public int MaterialId { get; set; }
        public Recipe Recipe { get; set; }
        public Material Material { get; set; }
        public int Amount { get; set; }
    }
}