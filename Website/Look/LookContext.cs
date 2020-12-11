using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Look.Models;

namespace Look
{
    public class LookContext : DbContext
    {
        public DbSet<Gebruiker> Gebruikers {get; set;}
        public DbSet<Melding> Meldingen {get; set;}
        public DbSet<Reactie> Reacties {get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder b) =>
        b.UseSqlite("Data Source=look.db");

        protected override void OnModelCreating(ModelBuilder ModelBuilder)
        {
            ModelBuilder.Entity<Gebruiker>()
                .Property(i => i.GebruikersNummer)
                .ValueGeneratedOnAdd();
            ModelBuilder.Entity<Melding>()
                .Property(i => i.MeldingId)
                .ValueGeneratedOnAdd();
            ModelBuilder.Entity<Reactie>()
                .Property(i => i.ReactieId)
                .ValueGeneratedOnAdd();    
        }
    }
}