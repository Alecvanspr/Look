using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Session;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using Look.Areas.Identity.Data;

namespace Look.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class RoleManagerController : Controller
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly LookIdentityDbContext _context;

        public RoleManagerController(RoleManager<ApplicationRole> roleManager, LookIdentityDbContext context)
        {
            _context = context;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            
            return View(roles);
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(string roleName)
        {
            if (roleName != null)
            {
                await _roleManager.CreateAsync(new ApplicationRole(roleName.Trim()));
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole(string roleName)
        {
            if (roleName != null)
            {
                ApplicationRole _role = _context.Roles.FirstOrDefault(role => role.Name.Equals(roleName));
                await _roleManager.DeleteAsync(_role);
            }
            return RedirectToAction("Index");
        }
    }
}