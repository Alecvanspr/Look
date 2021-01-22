using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using System.Security.Cryptography;
using System.Text;
using Look.Models;
using Look;
using Look.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Look.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace Look.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
     
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly LookIdentityDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public string UserHostAddress {get; set;}

        public AdminController(ILogger<AdminController> logger, LookIdentityDbContext context, UserManager<ApplicationUser> userManager)
        {
            // CheckMeldingenOpDatum();
            _context = context;
            _logger = logger;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        //Methode die de errors handelt
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}