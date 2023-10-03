using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ServerApp.Migrations._1cDb
{
    /// <inheritdoc />
    public partial class InitialS : Migration
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
                    AgregatId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    PlanZapuska = table.Column<int>(type: "integer", nullable: false),
                    PlanTrebDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PlanSdachi = table.Column<int>(type: "integer", nullable: true),
                    StockAvail = table.Column<string>(type: "text", nullable: true),
                    Vydano = table.Column<int>(type: "integer", nullable: true),
                    DateVydachi = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    FactMechDepDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    QuantityMechDep = table.Column<int>(type: "integer", nullable: true),
                    FactProdDepDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    QuantityProdDep = table.Column<int>(type: "integer", nullable: true),
                    SerialGroup = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DSE", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProjectType = table.Column<string>(type: "text", nullable: false),
                    ProjectNumber = table.Column<string>(type: "text", nullable: false),
                    Consumer = table.Column<string>(type: "text", nullable: false),
                    SrokDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Destination = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RNO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DseId = table.Column<long>(type: "bigint", nullable: false),
                    RegNumber = table.Column<string>(type: "text", nullable: false),
                    VpDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RegDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    TrebMaterial = table.Column<string>(type: "text", nullable: false),
                    SchemeMaterial = table.Column<string>(type: "text", nullable: false),
                    FactMaterial = table.Column<string>(type: "text", nullable: true),
                    DocumentNumber = table.Column<string>(type: "text", nullable: true),
                    DocumentURL = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RNO", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RNO_DSE_DseId",
                        column: x => x.DseId,
                        principalTable: "DSE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VydachaTrebov",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DseId = table.Column<long>(type: "bigint", nullable: false),
                    NumberTreb = table.Column<int>(type: "integer", nullable: false),
                    Treb = table.Column<int>(type: "integer", nullable: false),
                    TrebDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Vydano = table.Column<int>(type: "integer", nullable: true),
                    VydanoDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    MaterialZam = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VydachaTrebov", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VydachaTrebov_DSE_DseId",
                        column: x => x.DseId,
                        principalTable: "DSE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectAgregat",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AgregatId = table.Column<long>(type: "bigint", nullable: false),
                    ProjectId = table.Column<int>(type: "integer", nullable: true),
                    AgregatName = table.Column<string>(type: "text", nullable: false),
                    KolvoUstPart = table.Column<int>(type: "integer", nullable: false),
                    KolvoIzdIsp = table.Column<int>(type: "integer", nullable: false),
                    KolvoIzdOtg = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectAgregat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectAgregat_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectAgregat_ProjectId",
                table: "ProjectAgregat",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_RNO_DseId",
                table: "RNO",
                column: "DseId");

            migrationBuilder.CreateIndex(
                name: "IX_VydachaTrebov_DseId",
                table: "VydachaTrebov",
                column: "DseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectAgregat");

            migrationBuilder.DropTable(
                name: "RNO");

            migrationBuilder.DropTable(
                name: "VydachaTrebov");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "DSE");
        }
    }
}
