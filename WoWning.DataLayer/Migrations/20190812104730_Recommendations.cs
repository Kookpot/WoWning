using Microsoft.EntityFrameworkCore.Migrations;

namespace WoWning.DataLayer.Migrations
{
    public partial class Recommendations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NegativeRecommendations",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PositiveRecommendations",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NegativeRecommendations",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PositiveRecommendations",
                table: "AspNetUsers");
        }
    }
}
