using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Look.Migrations
{
    public partial class MigratieMetRapporten : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Liked",
                table: "Liked");

            migrationBuilder.RenameTable(
                name: "Liked",
                newName: "Likes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Likes",
                table: "Likes",
                columns: new[] { "UserId", "MeldingId" });

            migrationBuilder.CreateTable(
                name: "MeldingRapporten",
                columns: table => new
                {
                    RapportId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    GerapporteerdeMeldingMeldingId = table.Column<long>(type: "bigint", nullable: true),
                    GemaaktDoorId = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: true),
                    GeplaatstOp = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Categorie = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeldingRapporten", x => x.RapportId);
                    table.ForeignKey(
                        name: "FK_MeldingRapporten_Meldingen_GerapporteerdeMeldingMeldingId",
                        column: x => x.GerapporteerdeMeldingMeldingId,
                        principalTable: "Meldingen",
                        principalColumn: "MeldingId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MeldingRapporten_User_GemaaktDoorId",
                        column: x => x.GemaaktDoorId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReactieRapporten",
                columns: table => new
                {
                    RapportId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    GerapporteerdeReactieMeldingId = table.Column<long>(type: "bigint", nullable: true),
                    GemaaktDoorId = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: true),
                    GeplaatstOp = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Categorie = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReactieRapporten", x => x.RapportId);
                    table.ForeignKey(
                        name: "FK_ReactieRapporten_Meldingen_GerapporteerdeReactieMeldingId",
                        column: x => x.GerapporteerdeReactieMeldingId,
                        principalTable: "Meldingen",
                        principalColumn: "MeldingId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReactieRapporten_User_GemaaktDoorId",
                        column: x => x.GemaaktDoorId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MeldingRapporten_GemaaktDoorId",
                table: "MeldingRapporten",
                column: "GemaaktDoorId");

            migrationBuilder.CreateIndex(
                name: "IX_MeldingRapporten_GerapporteerdeMeldingMeldingId",
                table: "MeldingRapporten",
                column: "GerapporteerdeMeldingMeldingId");

            migrationBuilder.CreateIndex(
                name: "IX_ReactieRapporten_GemaaktDoorId",
                table: "ReactieRapporten",
                column: "GemaaktDoorId");

            migrationBuilder.CreateIndex(
                name: "IX_ReactieRapporten_GerapporteerdeReactieMeldingId",
                table: "ReactieRapporten",
                column: "GerapporteerdeReactieMeldingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MeldingRapporten");

            migrationBuilder.DropTable(
                name: "ReactieRapporten");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Likes",
                table: "Likes");

            migrationBuilder.RenameTable(
                name: "Likes",
                newName: "Liked");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Liked",
                table: "Liked",
                columns: new[] { "UserId", "MeldingId" });
        }
    }
}
