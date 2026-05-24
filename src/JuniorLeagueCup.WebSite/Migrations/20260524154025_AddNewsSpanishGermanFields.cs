using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JuniorLeagueCup.WebSite.Migrations
{
    /// <inheritdoc />
    public partial class AddNewsSpanishGermanFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContentDe",
                table: "NewsArticles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContentEs",
                table: "NewsArticles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SummaryDe",
                table: "NewsArticles",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SummaryEs",
                table: "NewsArticles",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TitleDe",
                table: "NewsArticles",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TitleEs",
                table: "NewsArticles",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentDe",
                table: "NewsArticles");

            migrationBuilder.DropColumn(
                name: "ContentEs",
                table: "NewsArticles");

            migrationBuilder.DropColumn(
                name: "SummaryDe",
                table: "NewsArticles");

            migrationBuilder.DropColumn(
                name: "SummaryEs",
                table: "NewsArticles");

            migrationBuilder.DropColumn(
                name: "TitleDe",
                table: "NewsArticles");

            migrationBuilder.DropColumn(
                name: "TitleEs",
                table: "NewsArticles");
        }
    }
}
