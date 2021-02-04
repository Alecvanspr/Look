using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Look.Migrations
{
    public partial class MigratieMetRapportenMetNavProps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Likes",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: false),
                    MeldingID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Likes", x => new { x.UserId, x.MeldingID });
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: false),
                    Name = table.Column<string>(type: "varchar(256) CHARACTER SET utf8mb4", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "varchar(256) CHARACTER SET utf8mb4", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: false),
                    UserName = table.Column<string>(type: "varchar(256) CHARACTER SET utf8mb4", maxLength: 256, nullable: true),
                    FirstName = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    LastName = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Street = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    HouseNumber = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    HouseNumberAddition = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    City = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    ZipCode = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    IsAnonymous = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    NormalizedUserName = table.Column<string>(type: "varchar(256) CHARACTER SET utf8mb4", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "varchar(256) CHARACTER SET utf8mb4", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "varchar(256) CHARACTER SET utf8mb4", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PasswordHash = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    SecurityStamp = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    PhoneNumber = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role Claim",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: false),
                    ClaimType = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    ClaimValue = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role Claim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Role Claim_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Meldingen",
                columns: table => new
                {
                    MeldingID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IsPrive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AangemaaktOp = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Titel = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Inhoud = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    AfbeeldingTitel = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    AfbeeldingData = table.Column<byte[]>(type: "longblob", nullable: true),
                    Likes = table.Column<int>(type: "int", nullable: false),
                    Views = table.Column<int>(type: "int", nullable: false),
                    IsActief = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ApplicationUserID = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: true),
                    Categorie = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meldingen", x => x.MeldingID);
                    table.ForeignKey(
                        name: "FK_Meldingen_User_ApplicationUserID",
                        column: x => x.ApplicationUserID,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "User Claim",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: false),
                    ClaimType = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    ClaimValue = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User Claim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User Claim_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User Login",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: false),
                    ProviderKey = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    UserId = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User Login", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_User Login_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User Role",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: false),
                    RoleId = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User Role", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_User Role_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User Role_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User Token",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: false),
                    LoginProvider = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: false),
                    Name = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: false),
                    Value = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User Token", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_User Token_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MeldingRapporten",
                columns: table => new
                {
                    RapportId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MeldingID = table.Column<long>(type: "bigint", nullable: false),
                    ApplicationUserID = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: true),
                    GeplaatstOp = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Categorie = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeldingRapporten", x => x.RapportId);
                    table.ForeignKey(
                        name: "FK_MeldingRapporten_Meldingen_MeldingID",
                        column: x => x.MeldingID,
                        principalTable: "Meldingen",
                        principalColumn: "MeldingID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MeldingRapporten_User_ApplicationUserID",
                        column: x => x.ApplicationUserID,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reacties",
                columns: table => new
                {
                    ReactieId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ApplicationUserID = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: true),
                    GeplaatstOp = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Bericht = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Likes = table.Column<int>(type: "int", nullable: false),
                    MeldingID = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reacties", x => x.ReactieId);
                    table.ForeignKey(
                        name: "FK_Reacties_Meldingen_MeldingID",
                        column: x => x.MeldingID,
                        principalTable: "Meldingen",
                        principalColumn: "MeldingID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reacties_User_ApplicationUserID",
                        column: x => x.ApplicationUserID,
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
                    ReactieID = table.Column<long>(type: "bigint", nullable: false),
                    ApplicationUserID = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: true),
                    GeplaatstOp = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Categorie = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReactieRapporten", x => x.RapportId);
                    table.ForeignKey(
                        name: "FK_ReactieRapporten_Reacties_ReactieID",
                        column: x => x.ReactieID,
                        principalTable: "Reacties",
                        principalColumn: "ReactieId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReactieRapporten_User_ApplicationUserID",
                        column: x => x.ApplicationUserID,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Meldingen_ApplicationUserID",
                table: "Meldingen",
                column: "ApplicationUserID");

            migrationBuilder.CreateIndex(
                name: "IX_MeldingRapporten_ApplicationUserID",
                table: "MeldingRapporten",
                column: "ApplicationUserID");

            migrationBuilder.CreateIndex(
                name: "IX_MeldingRapporten_MeldingID",
                table: "MeldingRapporten",
                column: "MeldingID");

            migrationBuilder.CreateIndex(
                name: "IX_ReactieRapporten_ApplicationUserID",
                table: "ReactieRapporten",
                column: "ApplicationUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ReactieRapporten_ReactieID",
                table: "ReactieRapporten",
                column: "ReactieID");

            migrationBuilder.CreateIndex(
                name: "IX_Reacties_ApplicationUserID",
                table: "Reacties",
                column: "ApplicationUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Reacties_MeldingID",
                table: "Reacties",
                column: "MeldingID");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Role",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Role Claim_RoleId",
                table: "Role Claim",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "User",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "User",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User Claim_UserId",
                table: "User Claim",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_User Login_UserId",
                table: "User Login",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_User Role_RoleId",
                table: "User Role",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Likes");

            migrationBuilder.DropTable(
                name: "MeldingRapporten");

            migrationBuilder.DropTable(
                name: "ReactieRapporten");

            migrationBuilder.DropTable(
                name: "Role Claim");

            migrationBuilder.DropTable(
                name: "User Claim");

            migrationBuilder.DropTable(
                name: "User Login");

            migrationBuilder.DropTable(
                name: "User Role");

            migrationBuilder.DropTable(
                name: "User Token");

            migrationBuilder.DropTable(
                name: "Reacties");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Meldingen");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
