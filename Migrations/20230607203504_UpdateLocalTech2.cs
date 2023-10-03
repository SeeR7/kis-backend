using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLocalTech2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TechСompletionPercentage",
                table: "Technology");

            migrationBuilder.DropColumn(
                name: "СompletionPercentage",
                table: "Technology");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TechСompletionPercentage",
                table: "Technology",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "СompletionPercentage",
                table: "Technology",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
