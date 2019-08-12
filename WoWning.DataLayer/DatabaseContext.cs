using WoWning.DataLayer.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WoWning.DataLayer
{
    public class DatabaseContext : IdentityDbContext<WoWUser>
    {
        public DatabaseContext() : base() { }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<Character> Characters { get; set; }
        public DbSet<CharacterRecipe> CharacterRecipes { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Recommendation> Recommendations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlite("Data Source=../WoWning/localdatabase.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Character>()
                .HasOne(x => x.User)
                .WithMany(x => x.Characters)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CharacterRecipe>()
                .HasKey(table => new {
                    table.CharacterId,
                    table.RecipeId
                });

            modelBuilder.Entity<Character>()
                .HasMany(x => x.CharacterRecipes)
                .WithOne(x => x.Character)
                .HasForeignKey(x => x.CharacterId);

            modelBuilder.Entity<Recipe>()
                .HasMany(x => x.CharacterRecipes)
                .WithOne(x => x.Recipe)
                .HasForeignKey(x => x.RecipeId);

            modelBuilder.Entity<Recipe>()
                .HasMany(x => x.RecipeMaterials)
                .WithOne(x => x.Recipe)
                .HasForeignKey(x => x.RecipeId);

            modelBuilder.Entity<Material>()
                .HasMany(x => x.RecipeMaterials)
                .WithOne(x => x.Material)
                .HasForeignKey(x => x.MaterialId);

            modelBuilder.Entity<RecipeMaterial>()
                .HasKey(table => new {
                    table.MaterialId,
                    table.RecipeId
                });

            modelBuilder.Entity<Recommendation>()
                .HasIndex(p => new { p.GiverId, p.TakerId });
        }
    }
}