using System;
using System.Collections.Generic;
using System.Linq;
using Look.Areas.Identity.Data;
using Look;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Look.Areas.Identity.Data
{
    public static class ContextSeed
    {
        public static async Task SeedRolesAsync(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            //Seed Roles
            await roleManager.CreateAsync(new ApplicationRole(Enums.Roles.SuperAdmin.ToString()));
            await roleManager.CreateAsync(new ApplicationRole(Enums.Roles.Admin.ToString()));
            await roleManager.CreateAsync(new ApplicationRole(Enums.Roles.Moderator.ToString()));
            await roleManager.CreateAsync(new ApplicationRole(Enums.Roles.Member.ToString()));
        }
    }
}
