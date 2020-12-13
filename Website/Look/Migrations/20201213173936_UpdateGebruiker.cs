using Microsoft.EntityFrameworkCore.Migrations;

namespace Look.Migrations
{
    public partial class UpdateGebruiker : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adres",
                table: "Gebruikers");

            migrationBuilder.AddColumn<int>(
                name: "likes",
                table: "Reacties",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "WachtWoord",
                table: "Gebruikers",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "VoorNaam",
                table: "Gebruikers",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GebruikersNaam",
                table: "Gebruikers",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmailAdres",
                table: "Gebruikers",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AchterNaam",
                table: "Gebruikers",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HuisNummer",
                table: "Gebruikers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PostCode",
                table: "Gebruikers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Straat",
                table: "Gebruikers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Woonplaats",
                table: "Gebruikers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "likes",
                table: "Reacties");

            migrationBuilder.DropColumn(
                name: "HuisNummer",
                table: "Gebruikers");

            migrationBuilder.DropColumn(
                name: "PostCode",
                table: "Gebruikers");

            migrationBuilder.DropColumn(
                name: "Straat",
                table: "Gebruikers");

            migrationBuilder.DropColumn(
                name: "Woonplaats",
                table: "Gebruikers");

            migrationBuilder.AlterColumn<string>(
                name: "WachtWoord",
                table: "Gebruikers",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "VoorNaam",
                table: "Gebruikers",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "GebruikersNaam",
                table: "Gebruikers",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "EmailAdres",
                table: "Gebruikers",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "AchterNaam",
                table: "Gebruikers",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "Adres",
                table: "Gebruikers",
                type: "TEXT",
                nullable: true);
        }
    }
}
