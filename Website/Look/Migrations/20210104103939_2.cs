using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Look.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Gebruikers",
                columns: table => new
                {
                    GebruikersNummer = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    VoorNaam = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: false),
                    AchterNaam = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: false),
                    GebruikersNaam = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: false),
                    Straat = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: false),
                    HuisNummer = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: false),
                    Woonplaats = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: false),
                    PostCode = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: false),
                    EmailAdres = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: false),
                    WachtWoord = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: false),
                    IsGeverifieerd = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsAnoniem = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LoginPogingen = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gebruikers", x => x.GebruikersNummer);
                });

            migrationBuilder.CreateTable(
                name: "Meldingen",
                columns: table => new
                {
                    MeldingId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IsPrive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AangemaaktOp = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Titel = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Inhoud = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Likes = table.Column<int>(type: "int", nullable: false),
                    Views = table.Column<int>(type: "int", nullable: false),
                    IsActief = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AuteurGebruikersNummer = table.Column<int>(type: "int", nullable: true),
                    Categorie = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meldingen", x => x.MeldingId);
                    table.ForeignKey(
                        name: "FK_Meldingen_Gebruikers_AuteurGebruikersNummer",
                        column: x => x.AuteurGebruikersNummer,
                        principalTable: "Gebruikers",
                        principalColumn: "GebruikersNummer",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reacties",
                columns: table => new
                {
                    ReactieId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    GeplaatstDoorGebruikersNummer = table.Column<int>(type: "int", nullable: true),
                    GeplaatstOp = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Bericht = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Likes = table.Column<int>(type: "int", nullable: false),
                    MeldingId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reacties", x => x.ReactieId);
                    table.ForeignKey(
                        name: "FK_Reacties_Gebruikers_GeplaatstDoorGebruikersNummer",
                        column: x => x.GeplaatstDoorGebruikersNummer,
                        principalTable: "Gebruikers",
                        principalColumn: "GebruikersNummer",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reacties_Meldingen_MeldingId",
                        column: x => x.MeldingId,
                        principalTable: "Meldingen",
                        principalColumn: "MeldingId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Meldingen_AuteurGebruikersNummer",
                table: "Meldingen",
                column: "AuteurGebruikersNummer");

            migrationBuilder.CreateIndex(
                name: "IX_Reacties_GeplaatstDoorGebruikersNummer",
                table: "Reacties",
                column: "GeplaatstDoorGebruikersNummer");

            migrationBuilder.CreateIndex(
                name: "IX_Reacties_MeldingId",
                table: "Reacties",
                column: "MeldingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reacties");

            migrationBuilder.DropTable(
                name: "Meldingen");

            migrationBuilder.DropTable(
                name: "Gebruikers");
        }
    }
}
