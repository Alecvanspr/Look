using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Look.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Liked",
                columns: table => new
                {
                    LikedId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    MeldingId = table.Column<long>(type: "bigint", nullable: false),
                    heeftGeliked = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Liked", x => x.LikedId);
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
                    FirstName = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    LastName = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Street = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    HouseNumber = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    City = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    ZipCode = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    IsAnonymous = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    UserName = table.Column<string>(type: "varchar(256) CHARACTER SET utf8mb4", maxLength: 256, nullable: true),
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
                name: "Role CLaim",
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
                    table.PrimaryKey("PK_Role CLaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Role CLaim_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    AuteurId = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: true),
                    Categorie = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meldingen", x => x.MeldingId);
                    table.ForeignKey(
                        name: "FK_Meldingen_User_AuteurId",
                        column: x => x.AuteurId,
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
                name: "Reacties",
                columns: table => new
                {
                    ReactieId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    GeplaatstDoorId = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: true),
                    GeplaatstOp = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Bericht = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Likes = table.Column<int>(type: "int", nullable: false),
                    MeldingId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reacties", x => x.ReactieId);
                    table.ForeignKey(
                        name: "FK_Reacties_Meldingen_MeldingId",
                        column: x => x.MeldingId,
                        principalTable: "Meldingen",
                        principalColumn: "MeldingId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reacties_User_GeplaatstDoorId",
                        column: x => x.GeplaatstDoorId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Meldingen_AuteurId",
                table: "Meldingen",
                column: "AuteurId");

            migrationBuilder.CreateIndex(
                name: "IX_Reacties_GeplaatstDoorId",
                table: "Reacties",
                column: "GeplaatstDoorId");

            migrationBuilder.CreateIndex(
                name: "IX_Reacties_MeldingId",
                table: "Reacties",
                column: "MeldingId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Role",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Role CLaim_RoleId",
                table: "Role CLaim",
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
                name: "Liked");

            migrationBuilder.DropTable(
                name: "Reacties");

            migrationBuilder.DropTable(
                name: "Role CLaim");

            migrationBuilder.DropTable(
                name: "User Claim");

            migrationBuilder.DropTable(
                name: "User Login");

            migrationBuilder.DropTable(
                name: "User Role");

            migrationBuilder.DropTable(
                name: "User Token");

            migrationBuilder.DropTable(
                name: "Meldingen");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
