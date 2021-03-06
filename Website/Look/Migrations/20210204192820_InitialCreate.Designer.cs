// <auto-generated />
using System;
using Look.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Look.Migrations
{
    [DbContext(typeof(LookIdentityDbContext))]
    [Migration("20210204192820_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("Look.Areas.Identity.Data.ApplicationRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("Look.Areas.Identity.Data.ApplicationRoleClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Role Claim");
                });

            modelBuilder.Entity("Look.Areas.Identity.Data.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("FirstName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("HouseNumber")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("HouseNumberAddition")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("IsAnonymous")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("LastLogin")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("LastName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid>("PriveCode")
                        .HasColumnType("char(36)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Street")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4");

                    b.Property<string>("ZipCode")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Look.Areas.Identity.Data.ApplicationUserClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("User Claim");
                });

            modelBuilder.Entity("Look.Areas.Identity.Data.ApplicationUserLogin", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("User Login");
                });

            modelBuilder.Entity("Look.Areas.Identity.Data.ApplicationUserRole", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("User Role");
                });

            modelBuilder.Entity("Look.Areas.Identity.Data.ApplicationUserToken", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Value")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("User Token");
                });

            modelBuilder.Entity("Look.Models.Liked", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<long>("MeldingID")
                        .HasColumnType("bigint");

                    b.HasKey("UserId", "MeldingID");

                    b.ToTable("Likes");
                });

            modelBuilder.Entity("Look.Models.Melding", b =>
                {
                    b.Property<long>("MeldingID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTime>("AangemaaktOp")
                        .HasColumnType("datetime(6)");

                    b.Property<byte[]>("AfbeeldingData")
                        .HasColumnType("longblob");

                    b.Property<string>("AfbeeldingTitel")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ApplicationUserID")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Categorie")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Inhoud")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("IsActief")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsPrive")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("Likes")
                        .HasColumnType("int");

                    b.Property<Guid?>("PriveCode")
                        .HasColumnType("char(36)");

                    b.Property<string>("Titel")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Views")
                        .HasColumnType("int");

                    b.HasKey("MeldingID");

                    b.HasIndex("ApplicationUserID");

                    b.ToTable("Meldingen");
                });

            modelBuilder.Entity("Look.Models.MeldingRapport", b =>
                {
                    b.Property<long>("RapportId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("ApplicationUserID")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Categorie")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("GeplaatstOp")
                        .HasColumnType("datetime(6)");

                    b.Property<long>("MeldingID")
                        .HasColumnType("bigint");

                    b.HasKey("RapportId");

                    b.HasIndex("ApplicationUserID");

                    b.HasIndex("MeldingID");

                    b.ToTable("MeldingRapporten");
                });

            modelBuilder.Entity("Look.Models.Reactie", b =>
                {
                    b.Property<long>("ReactieId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("ApplicationUserID")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Bericht")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("GeplaatstOp")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Likes")
                        .HasColumnType("int");

                    b.Property<long?>("MeldingID")
                        .HasColumnType("bigint");

                    b.HasKey("ReactieId");

                    b.HasIndex("ApplicationUserID");

                    b.HasIndex("MeldingID");

                    b.ToTable("Reacties");
                });

            modelBuilder.Entity("Look.Models.ReactieRapport", b =>
                {
                    b.Property<long>("RapportId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("ApplicationUserID")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Categorie")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("GeplaatstOp")
                        .HasColumnType("datetime(6)");

                    b.Property<long>("ReactieID")
                        .HasColumnType("bigint");

                    b.HasKey("RapportId");

                    b.HasIndex("ApplicationUserID");

                    b.HasIndex("ReactieID");

                    b.ToTable("ReactieRapporten");
                });

            modelBuilder.Entity("Look.Areas.Identity.Data.ApplicationRoleClaim", b =>
                {
                    b.HasOne("Look.Areas.Identity.Data.ApplicationRole", "Role")
                        .WithMany("RoleClaims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Look.Areas.Identity.Data.ApplicationUserClaim", b =>
                {
                    b.HasOne("Look.Areas.Identity.Data.ApplicationUser", "User")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Look.Areas.Identity.Data.ApplicationUserLogin", b =>
                {
                    b.HasOne("Look.Areas.Identity.Data.ApplicationUser", "User")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Look.Areas.Identity.Data.ApplicationUserRole", b =>
                {
                    b.HasOne("Look.Areas.Identity.Data.ApplicationRole", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Look.Areas.Identity.Data.ApplicationUser", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Look.Areas.Identity.Data.ApplicationUserToken", b =>
                {
                    b.HasOne("Look.Areas.Identity.Data.ApplicationUser", "User")
                        .WithMany("Tokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Look.Models.Melding", b =>
                {
                    b.HasOne("Look.Areas.Identity.Data.ApplicationUser", "Auteur")
                        .WithMany()
                        .HasForeignKey("ApplicationUserID");

                    b.Navigation("Auteur");
                });

            modelBuilder.Entity("Look.Models.MeldingRapport", b =>
                {
                    b.HasOne("Look.Areas.Identity.Data.ApplicationUser", "GemaaktDoor")
                        .WithMany("MeldingRapporten")
                        .HasForeignKey("ApplicationUserID");

                    b.HasOne("Look.Models.Melding", "GerapporteerdeMelding")
                        .WithMany("Rapporten")
                        .HasForeignKey("MeldingID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GemaaktDoor");

                    b.Navigation("GerapporteerdeMelding");
                });

            modelBuilder.Entity("Look.Models.Reactie", b =>
                {
                    b.HasOne("Look.Areas.Identity.Data.ApplicationUser", "GeplaatstDoor")
                        .WithMany()
                        .HasForeignKey("ApplicationUserID");

                    b.HasOne("Look.Models.Melding", null)
                        .WithMany("Reacties")
                        .HasForeignKey("MeldingID");

                    b.Navigation("GeplaatstDoor");
                });

            modelBuilder.Entity("Look.Models.ReactieRapport", b =>
                {
                    b.HasOne("Look.Areas.Identity.Data.ApplicationUser", "GemaaktDoor")
                        .WithMany("ReactieRapporten")
                        .HasForeignKey("ApplicationUserID");

                    b.HasOne("Look.Models.Reactie", "GerapporteerdeReactie")
                        .WithMany("Rapporten")
                        .HasForeignKey("ReactieID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GemaaktDoor");

                    b.Navigation("GerapporteerdeReactie");
                });

            modelBuilder.Entity("Look.Areas.Identity.Data.ApplicationRole", b =>
                {
                    b.Navigation("RoleClaims");

                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("Look.Areas.Identity.Data.ApplicationUser", b =>
                {
                    b.Navigation("Claims");

                    b.Navigation("Logins");

                    b.Navigation("MeldingRapporten");

                    b.Navigation("ReactieRapporten");

                    b.Navigation("Tokens");

                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("Look.Models.Melding", b =>
                {
                    b.Navigation("Rapporten");

                    b.Navigation("Reacties");
                });

            modelBuilder.Entity("Look.Models.Reactie", b =>
                {
                    b.Navigation("Rapporten");
                });
#pragma warning restore 612, 618
        }
    }
}
