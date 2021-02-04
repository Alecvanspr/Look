using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Look.Migrations
{
    public partial class MigratieMetNavProps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meldingen_User_AuteurId",
                table: "Meldingen");

            migrationBuilder.DropForeignKey(
                name: "FK_MeldingRapporten_Meldingen_GerapporteerdeMeldingMeldingId",
                table: "MeldingRapporten");

            migrationBuilder.DropForeignKey(
                name: "FK_MeldingRapporten_User_GemaaktDoorId",
                table: "MeldingRapporten");

            migrationBuilder.DropForeignKey(
                name: "FK_ReactieRapporten_Meldingen_GerapporteerdeReactieMeldingId",
                table: "ReactieRapporten");

            migrationBuilder.DropForeignKey(
                name: "FK_ReactieRapporten_User_GemaaktDoorId",
                table: "ReactieRapporten");

            migrationBuilder.DropForeignKey(
                name: "FK_Reacties_Meldingen_MeldingId",
                table: "Reacties");

            migrationBuilder.DropForeignKey(
                name: "FK_Reacties_User_GeplaatstDoorId",
                table: "Reacties");

            migrationBuilder.DropIndex(
                name: "IX_ReactieRapporten_GerapporteerdeReactieMeldingId",
                table: "ReactieRapporten");

            migrationBuilder.DropIndex(
                name: "IX_MeldingRapporten_GerapporteerdeMeldingMeldingId",
                table: "MeldingRapporten");

            migrationBuilder.DropColumn(
                name: "GerapporteerdeReactieMeldingId",
                table: "ReactieRapporten");

            migrationBuilder.DropColumn(
                name: "GerapporteerdeMeldingMeldingId",
                table: "MeldingRapporten");

            migrationBuilder.RenameColumn(
                name: "MeldingId",
                table: "Reacties",
                newName: "MeldingID");

            migrationBuilder.RenameColumn(
                name: "ReactieId",
                table: "Reacties",
                newName: "ReactieID");

            migrationBuilder.RenameColumn(
                name: "GeplaatstDoorId",
                table: "Reacties",
                newName: "ApplicationUserID");

            migrationBuilder.RenameIndex(
                name: "IX_Reacties_MeldingId",
                table: "Reacties",
                newName: "IX_Reacties_MeldingID");

            migrationBuilder.RenameIndex(
                name: "IX_Reacties_GeplaatstDoorId",
                table: "Reacties",
                newName: "IX_Reacties_ApplicationUserID");

            migrationBuilder.RenameColumn(
                name: "GemaaktDoorId",
                table: "ReactieRapporten",
                newName: "ApplicationUserID");

            migrationBuilder.RenameIndex(
                name: "IX_ReactieRapporten_GemaaktDoorId",
                table: "ReactieRapporten",
                newName: "IX_ReactieRapporten_ApplicationUserID");

            migrationBuilder.RenameColumn(
                name: "GemaaktDoorId",
                table: "MeldingRapporten",
                newName: "ApplicationUserID");

            migrationBuilder.RenameIndex(
                name: "IX_MeldingRapporten_GemaaktDoorId",
                table: "MeldingRapporten",
                newName: "IX_MeldingRapporten_ApplicationUserID");

            migrationBuilder.RenameColumn(
                name: "MeldingId",
                table: "Meldingen",
                newName: "MeldingID");

            migrationBuilder.RenameColumn(
                name: "AuteurId",
                table: "Meldingen",
                newName: "ApplicationUserID");

            migrationBuilder.RenameIndex(
                name: "IX_Meldingen_AuteurId",
                table: "Meldingen",
                newName: "IX_Meldingen_ApplicationUserID");

            migrationBuilder.RenameColumn(
                name: "MeldingId",
                table: "Likes",
                newName: "MeldingID");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Likes",
                newName: "ApplicationUserID");

            migrationBuilder.AddColumn<Guid>(
                name: "PriveCode",
                table: "User",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<long>(
                name: "MeldingID",
                table: "Reacties",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ReactieID",
                table: "ReactieRapporten",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "MeldingID",
                table: "MeldingRapporten",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_ReactieRapporten_ReactieID",
                table: "ReactieRapporten",
                column: "ReactieID");

            migrationBuilder.CreateIndex(
                name: "IX_MeldingRapporten_MeldingID",
                table: "MeldingRapporten",
                column: "MeldingID");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_MeldingID",
                table: "Likes",
                column: "MeldingID");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Meldingen_MeldingID",
                table: "Likes",
                column: "MeldingID",
                principalTable: "Meldingen",
                principalColumn: "MeldingID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_User_ApplicationUserID",
                table: "Likes",
                column: "ApplicationUserID",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Meldingen_User_ApplicationUserID",
                table: "Meldingen",
                column: "ApplicationUserID",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MeldingRapporten_Meldingen_MeldingID",
                table: "MeldingRapporten",
                column: "MeldingID",
                principalTable: "Meldingen",
                principalColumn: "MeldingID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MeldingRapporten_User_ApplicationUserID",
                table: "MeldingRapporten",
                column: "ApplicationUserID",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReactieRapporten_Reacties_ReactieID",
                table: "ReactieRapporten",
                column: "ReactieID",
                principalTable: "Reacties",
                principalColumn: "ReactieID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReactieRapporten_User_ApplicationUserID",
                table: "ReactieRapporten",
                column: "ApplicationUserID",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reacties_Meldingen_MeldingID",
                table: "Reacties",
                column: "MeldingID",
                principalTable: "Meldingen",
                principalColumn: "MeldingID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reacties_User_ApplicationUserID",
                table: "Reacties",
                column: "ApplicationUserID",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Meldingen_MeldingID",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_User_ApplicationUserID",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Meldingen_User_ApplicationUserID",
                table: "Meldingen");

            migrationBuilder.DropForeignKey(
                name: "FK_MeldingRapporten_Meldingen_MeldingID",
                table: "MeldingRapporten");

            migrationBuilder.DropForeignKey(
                name: "FK_MeldingRapporten_User_ApplicationUserID",
                table: "MeldingRapporten");

            migrationBuilder.DropForeignKey(
                name: "FK_ReactieRapporten_Reacties_ReactieID",
                table: "ReactieRapporten");

            migrationBuilder.DropForeignKey(
                name: "FK_ReactieRapporten_User_ApplicationUserID",
                table: "ReactieRapporten");

            migrationBuilder.DropForeignKey(
                name: "FK_Reacties_Meldingen_MeldingID",
                table: "Reacties");

            migrationBuilder.DropForeignKey(
                name: "FK_Reacties_User_ApplicationUserID",
                table: "Reacties");

            migrationBuilder.DropIndex(
                name: "IX_ReactieRapporten_ReactieID",
                table: "ReactieRapporten");

            migrationBuilder.DropIndex(
                name: "IX_MeldingRapporten_MeldingID",
                table: "MeldingRapporten");

            migrationBuilder.DropIndex(
                name: "IX_Likes_MeldingID",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "PriveCode",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ReactieID",
                table: "ReactieRapporten");

            migrationBuilder.DropColumn(
                name: "MeldingID",
                table: "MeldingRapporten");

            migrationBuilder.RenameColumn(
                name: "MeldingID",
                table: "Reacties",
                newName: "MeldingId");

            migrationBuilder.RenameColumn(
                name: "ReactieID",
                table: "Reacties",
                newName: "ReactieId");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserID",
                table: "Reacties",
                newName: "GeplaatstDoorId");

            migrationBuilder.RenameIndex(
                name: "IX_Reacties_MeldingID",
                table: "Reacties",
                newName: "IX_Reacties_MeldingId");

            migrationBuilder.RenameIndex(
                name: "IX_Reacties_ApplicationUserID",
                table: "Reacties",
                newName: "IX_Reacties_GeplaatstDoorId");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserID",
                table: "ReactieRapporten",
                newName: "GemaaktDoorId");

            migrationBuilder.RenameIndex(
                name: "IX_ReactieRapporten_ApplicationUserID",
                table: "ReactieRapporten",
                newName: "IX_ReactieRapporten_GemaaktDoorId");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserID",
                table: "MeldingRapporten",
                newName: "GemaaktDoorId");

            migrationBuilder.RenameIndex(
                name: "IX_MeldingRapporten_ApplicationUserID",
                table: "MeldingRapporten",
                newName: "IX_MeldingRapporten_GemaaktDoorId");

            migrationBuilder.RenameColumn(
                name: "MeldingID",
                table: "Meldingen",
                newName: "MeldingId");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserID",
                table: "Meldingen",
                newName: "AuteurId");

            migrationBuilder.RenameIndex(
                name: "IX_Meldingen_ApplicationUserID",
                table: "Meldingen",
                newName: "IX_Meldingen_AuteurId");

            migrationBuilder.RenameColumn(
                name: "MeldingID",
                table: "Likes",
                newName: "MeldingId");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserID",
                table: "Likes",
                newName: "UserId");

            migrationBuilder.AlterColumn<long>(
                name: "MeldingId",
                table: "Reacties",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "GerapporteerdeReactieMeldingId",
                table: "ReactieRapporten",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "GerapporteerdeMeldingMeldingId",
                table: "MeldingRapporten",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReactieRapporten_GerapporteerdeReactieMeldingId",
                table: "ReactieRapporten",
                column: "GerapporteerdeReactieMeldingId");

            migrationBuilder.CreateIndex(
                name: "IX_MeldingRapporten_GerapporteerdeMeldingMeldingId",
                table: "MeldingRapporten",
                column: "GerapporteerdeMeldingMeldingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Meldingen_User_AuteurId",
                table: "Meldingen",
                column: "AuteurId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MeldingRapporten_Meldingen_GerapporteerdeMeldingMeldingId",
                table: "MeldingRapporten",
                column: "GerapporteerdeMeldingMeldingId",
                principalTable: "Meldingen",
                principalColumn: "MeldingId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MeldingRapporten_User_GemaaktDoorId",
                table: "MeldingRapporten",
                column: "GemaaktDoorId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReactieRapporten_Meldingen_GerapporteerdeReactieMeldingId",
                table: "ReactieRapporten",
                column: "GerapporteerdeReactieMeldingId",
                principalTable: "Meldingen",
                principalColumn: "MeldingId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReactieRapporten_User_GemaaktDoorId",
                table: "ReactieRapporten",
                column: "GemaaktDoorId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reacties_Meldingen_MeldingId",
                table: "Reacties",
                column: "MeldingId",
                principalTable: "Meldingen",
                principalColumn: "MeldingId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reacties_User_GeplaatstDoorId",
                table: "Reacties",
                column: "GeplaatstDoorId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
