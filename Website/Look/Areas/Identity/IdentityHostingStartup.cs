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
                services.AddIdentity<ApplicationUser, ApplicationRole>(options => 
                    options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<LookIdentityDbContext>()
                    .AddDefaultUI()
                    .AddDefaultTokenProviders()
                    .AddErrorDescriber<ApplicationErrorDescriber>();
            });
        }
    }
}