using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ServerApp.Migrations.RusagrDb
{
    /// <inheritdoc />
    public partial class InitialR : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DSE",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ZagType = table.Column<string>(type: "text", nullable: false),
                    DepCons = table.Column<string>(type: "text", nullable: false),
                    DepProd = table.Column<string>(type: "text", nullable: false),
                    Material = table.Column<string>(type: "text", nullable: false),
                    RespDesigner = table.Column<string>(type: "text", nullable: false),
                    RespTechnolog = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DSE", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DepRoute",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DseId = table.Column<long>(type: "bigint", nullable: false),
                    DepCons = table.Column<string>(type: "text", nullable: false),
                    DepProd = table.Column<string>(type: "text", nullable: false),
                    EntryDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    OutDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepRoute", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DepRoute_DSE_DseId",
                        column: x => x.DseId,
                        principalTable: "DSE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DseSostav",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ChildId = table.Column<long>(type: "bigint", nullable: false),
                    ParentId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DseSostav", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DseSostav_DSE_ChildId",
                        column: x => x.ChildId,
                        principalTable: "DSE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DseSostav_DSE_ParentId",
                        column: x => x.ParentId,
                        principalTable: "DSE",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DepRoute_DseId",
                table: "DepRoute",
                column: "DseId");

            migrationBuilder.CreateIndex(
                name: "IX_DseSostav_ChildId",
                table: "DseSostav",
                column: "ChildId");

            migrationBuilder.CreateIndex(
                name: "IX_DseSostav_ParentId",
                table: "DseSostav",
                column: "ParentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DepRoute");

            migrationBuilder.DropTable(
                name: "DseSostav");

            migrationBuilder.DropTable(
                name: "DSE");
        }
    }
}
