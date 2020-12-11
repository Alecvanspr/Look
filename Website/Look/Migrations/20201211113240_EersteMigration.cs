using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Look.Migrations
{
    public partial class EersteMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Gebruikers",
                columns: table => new
                {
                    GebruikersNummer = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    VoorNaam = table.Column<string>(type: "TEXT", nullable: true),
                    AchterNaam = table.Column<string>(type: "TEXT", nullable: true),
                    GebruikersNaam = table.Column<string>(type: "TEXT", nullable: true),
                    Adres = table.Column<string>(type: "TEXT", nullable: true),
                    EmailAdres = table.Column<string>(type: "TEXT", nullable: true),
                    WachtWoord = table.Column<string>(type: "TEXT", nullable: true),
                    IsGeverifieerd = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsAnoniem = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gebruikers", x => x.GebruikersNummer);
                });

            migrationBuilder.CreateTable(
                name: "Meldingen",
                columns: table => new
                {
                    MeldingId = table.Column<string>(type: "TEXT", nullable: false),
                    IsPrive = table.Column<bool>(type: "INTEGER", nullable: false),
                    AangemaaktOp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Titel = table.Column<string>(type: "TEXT", nullable: true),
                    Inhoud = table.Column<string>(type: "TEXT", nullable: true),
                    Likes = table.Column<int>(type: "INTEGER", nullable: false),
                    Views = table.Column<int>(type: "INTEGER", nullable: false),
                    IsActief = table.Column<bool>(type: "INTEGER", nullable: false),
                    AuteurGebruikersNummer = table.Column<int>(type: "INTEGER", nullable: true),
                    Categorie = table.Column<string>(type: "TEXT", nullable: true)
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
                    ReactieId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GeplaatstDoorGebruikersNummer = table.Column<int>(type: "INTEGER", nullable: true),
                    GeplaatstOp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Bericht = table.Column<string>(type: "TEXT", nullable: true),
                    MeldingId = table.Column<string>(type: "TEXT", nullable: true)
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
