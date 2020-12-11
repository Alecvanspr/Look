﻿// <auto-generated />
using System;
using Look;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Look.Migrations
{
    [DbContext(typeof(LookContext))]
    partial class LookContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Look.Models.Gebruiker", b =>
                {
                    b.Property<int>("GebruikersNummer")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AchterNaam")
                        .HasColumnType("TEXT");

                    b.Property<string>("Adres")
                        .HasColumnType("TEXT");

                    b.Property<string>("EmailAdres")
                        .HasColumnType("TEXT");

                    b.Property<string>("GebruikersNaam")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsAnoniem")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsGeverifieerd")
                        .HasColumnType("INTEGER");

                    b.Property<string>("VoorNaam")
                        .HasColumnType("TEXT");

                    b.Property<string>("WachtWoord")
                        .HasColumnType("TEXT");

                    b.HasKey("GebruikersNummer");

                    b.ToTable("Gebruikers");
                });

            modelBuilder.Entity("Look.Models.Melding", b =>
                {
                    b.Property<int>("MeldingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("AangemaaktOp")
                        .HasColumnType("TEXT");

                    b.Property<int?>("AuteurGebruikersNummer")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Categorie")
                        .HasColumnType("TEXT");

                    b.Property<string>("Inhoud")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsActief")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsPrive")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Likes")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Titel")
                        .HasColumnType("TEXT");

                    b.Property<int>("Views")
                        .HasColumnType("INTEGER");

                    b.HasKey("MeldingId");

                    b.HasIndex("AuteurGebruikersNummer");

                    b.ToTable("Meldingen");
                });

            modelBuilder.Entity("Look.Models.Reactie", b =>
                {
                    b.Property<int>("ReactieId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Bericht")
                        .HasColumnType("TEXT");

                    b.Property<int?>("GeplaatstDoorGebruikersNummer")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("GeplaatstOp")
                        .HasColumnType("TEXT");

                    b.Property<int?>("MeldingId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ReactieId");

                    b.HasIndex("GeplaatstDoorGebruikersNummer");

                    b.HasIndex("MeldingId");

                    b.ToTable("Reacties");
                });

            modelBuilder.Entity("Look.Models.Melding", b =>
                {
                    b.HasOne("Look.Models.Gebruiker", "Auteur")
                        .WithMany()
                        .HasForeignKey("AuteurGebruikersNummer");

                    b.Navigation("Auteur");
                });

            modelBuilder.Entity("Look.Models.Reactie", b =>
                {
                    b.HasOne("Look.Models.Gebruiker", "GeplaatstDoor")
                        .WithMany()
                        .HasForeignKey("GeplaatstDoorGebruikersNummer");

                    b.HasOne("Look.Models.Melding", null)
                        .WithMany("Reacties")
                        .HasForeignKey("MeldingId");

                    b.Navigation("GeplaatstDoor");
                });

            modelBuilder.Entity("Look.Models.Melding", b =>
                {
                    b.Navigation("Reacties");
                });
#pragma warning restore 612, 618
        }
    }
}
