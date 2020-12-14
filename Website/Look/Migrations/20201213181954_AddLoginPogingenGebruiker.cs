using Microsoft.EntityFrameworkCore.Migrations;

namespace Look.Migrations
{
    public partial class AddLoginPogingenGebruiker : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LoginPogingen",
                table: "Gebruikers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LoginPogingen",
                table: "Gebruikers");
        }
    }
}
