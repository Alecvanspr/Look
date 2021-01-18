using Microsoft.EntityFrameworkCore.Migrations;

namespace Look.Migrations
{
    public partial class EditRoleClaim : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Role CLaim_Role_RoleId",
                table: "Role CLaim");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Role CLaim",
                table: "Role CLaim");

            migrationBuilder.RenameTable(
                name: "Role CLaim",
                newName: "Role Claim");

            migrationBuilder.RenameIndex(
                name: "IX_Role CLaim_RoleId",
                table: "Role Claim",
                newName: "IX_Role Claim_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Role Claim",
                table: "Role Claim",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Role Claim_Role_RoleId",
                table: "Role Claim",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Role Claim_Role_RoleId",
                table: "Role Claim");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Role Claim",
                table: "Role Claim");

            migrationBuilder.RenameTable(
                name: "Role Claim",
                newName: "Role CLaim");

            migrationBuilder.RenameIndex(
                name: "IX_Role Claim_RoleId",
                table: "Role CLaim",
                newName: "IX_Role CLaim_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Role CLaim",
                table: "Role CLaim",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Role CLaim_Role_RoleId",
                table: "Role CLaim",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
