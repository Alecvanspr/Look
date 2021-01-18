using System;
using Look.Areas.Identity.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Look.Models;

[assembly: HostingStartup(typeof(Look.Areas.Identity.IdentityHostingStartup))]
namespace Look.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<LookIdentityDbContext>(
                dbContextOptions => 
                    dbContextOptions.UseMySql("Server=94.209.210.86; User Id=Groepje1E; Password=b48e3c8796024b86b825276414a0ca4b; Database = data6ea578e716254ef8ab18f464c5bdcffc", 
                    ServerVersion.FromString("8.0.22-mysql"))
                );

                services.AddIdentity<ApplicationUser, ApplicationRole>()
                    .AddEntityFrameworkStores<LookIdentityDbContext>()
                    .AddDefaultUI()
                    .AddDefaultTokenProviders()
                    .AddErrorDescriber<ApplicationErrorDescriber>();
            });
        }
    }
}