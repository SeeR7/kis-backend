using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerApp.Migrations.RusagrDb
{
    /// <inheritdoc />
    public partial class UpdateDse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RespDesigner",
                table: "DSE");

            migrationBuilder.DropColumn(
                name: "RespTechnolog",
                table: "DSE");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RespDesigner",
                table: "DSE",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RespTechnolog",
                table: "DSE",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
