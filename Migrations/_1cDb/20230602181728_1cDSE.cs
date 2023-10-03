using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerApp.Migrations._1cDb
{
    /// <inheritdoc />
    public partial class _1cDSE : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AgregatId",
                table: "DSE");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AgregatId",
                table: "DSE",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
