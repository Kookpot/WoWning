using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace WoWning.DataLayer.Migrations
{
    public partial class IdGen2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex("IX_Recommendations_GiverId_TakerId", "Recommendations");
            migrationBuilder.DropTable("CharacterRecipes");
            migrationBuilder.DropTable("Recommendations");
            migrationBuilder.DropTable("Characters");

            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: true),
                    Server = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Side = table.Column<int>(nullable: false),
                    IsLeather = table.Column<bool>(nullable: false),
                    IsEnchanter = table.Column<bool>(nullable: false),
                    IsEngineer = table.Column<bool>(nullable: false),
                    IsAlchemist = table.Column<bool>(nullable: false),
                    IsCook = table.Column<bool>(nullable: false),
                    IsFirstAid = table.Column<bool>(nullable: false),
                    IsTailor = table.Column<bool>(nullable: false),
                    IsBlacksmith = table.Column<bool>(nullable: false),
                    IsWeaponsmith = table.Column<bool>(nullable: false),
                    IsArmorsmith = table.Column<bool>(nullable: false),
                    IsSwordsmith = table.Column<bool>(nullable: false),
                    IsAxesmith = table.Column<bool>(nullable: false),
                    IsMacesmith = table.Column<bool>(nullable: false),
                    IsGoblin = table.Column<bool>(nullable: false),
                    IsGnomish = table.Column<bool>(nullable: false),
                    IsElemental = table.Column<bool>(nullable: false),
                    IsDragonscale = table.Column<bool>(nullable: false),
                    IsTribal = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Characters_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CharacterRecipes",
                columns: table => new
                {
                    CharacterId = table.Column<int>(nullable: false),
                    RecipeId = table.Column<int>(nullable: false),
                    Silver = table.Column<int>(nullable: false),
                    Gold = table.Column<int>(nullable: false),
                    Copper = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterRecipes", x => new { x.CharacterId, x.RecipeId });
                    table.ForeignKey(
                        name: "FK_CharacterRecipes_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterRecipes_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Recommendations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GiverId = table.Column<string>(nullable: true),
                    TakerId = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recommendations", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Recommendations_GiverId_TakerId",
                table: "Recommendations",
                columns: new[] { "GiverId", "TakerId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropIndex(
            //    name: "UserNameIndex",
            //    table: "AspNetUsers");

          
        }
    }
}
