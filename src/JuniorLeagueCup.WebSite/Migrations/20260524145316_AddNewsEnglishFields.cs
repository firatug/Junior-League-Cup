using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JuniorLeagueCup.WebSite.Migrations
{
    /// <inheritdoc />
    public partial class AddNewsEnglishFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContentEn",
                table: "NewsArticles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SummaryEn",
                table: "NewsArticles",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TitleEn",
                table: "NewsArticles",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentEn",
                table: "NewsArticles");

            migrationBuilder.DropColumn(
                name: "SummaryEn",
                table: "NewsArticles");

            migrationBuilder.DropColumn(
                name: "TitleEn",
                table: "NewsArticles");
        }
    }
}
