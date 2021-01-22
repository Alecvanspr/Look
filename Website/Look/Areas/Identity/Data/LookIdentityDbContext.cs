using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Look.Areas.Identity.Data;
using Look.Models;

namespace Look.Areas.Identity.Data
{
    public class LookIdentityDbContext
    : IdentityDbContext<
        ApplicationUser, ApplicationRole, string,
        ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin,
        ApplicationRoleClaim, ApplicationUserToken>
    {
        public DbSet<Melding> Meldingen {get; set;}
        public DbSet<Reactie> Reacties {get; set;}
        public DbSet<Liked> Likes { get; set; }

        public LookIdentityDbContext(DbContextOptions<LookIdentityDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //override default table name
            modelBuilder.Entity<ApplicationUser>().ToTable("User");
            modelBuilder.Entity<ApplicationRole>().ToTable("Role");

            modelBuilder.Entity<ApplicationUserClaim>().ToTable("User Claim");
            modelBuilder.Entity<ApplicationUserLogin>().ToTable("User Login");
            modelBuilder.Entity<ApplicationUserToken>().ToTable("User Token");
            modelBuilder.Entity<ApplicationUserRole>().ToTable("User Role");
            modelBuilder.Entity<ApplicationRoleClaim>().ToTable("Role Claim");

            modelBuilder.Entity<ApplicationUser>(b =>
            {
                // Each User can have many UserClaims
                b.HasMany(e => e.Claims)
                    .WithOne(e => e.User)
                    .HasForeignKey(uc => uc.UserId)
                    .IsRequired();

                // Each User can have many UserLogins
                b.HasMany(e => e.Logins)
                    .WithOne(e => e.User)
                    .HasForeignKey(ul => ul.UserId)
                    .IsRequired();

                // Each User can have many UserTokens
                b.HasMany(e => e.Tokens)
                    .WithOne(e => e.User)
                    .HasForeignKey(ut => ut.UserId)
                    .IsRequired();

                // Each User can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.User)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            modelBuilder.Entity<ApplicationRole>(b =>
            {
                // Each Role can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.Role)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                // Each Role can have many associated RoleClaims
                b.HasMany(e => e.RoleClaims)
                    .WithOne(e => e.Role)
                    .HasForeignKey(rc => rc.RoleId)
                    .IsRequired();
            });

            
            modelBuilder.Entity<Melding>()
                .Property(i => i.MeldingId)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Reactie>()
                .Property(i => i.ReactieId)
                .ValueGeneratedOnAdd();   
            modelBuilder.Entity<Liked>()
                .HasKey(p => new { p.UserId, p.MeldingId});
        }
    }
}
