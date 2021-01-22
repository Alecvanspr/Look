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
using System.Security.Cryptography;
using System.Text;
using Look.Models;
using Look;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Look.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace Look.Controllers
{
     
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly LookIdentityDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public string UserHostAddress {get; set;}

        public HomeController(ILogger<HomeController> logger, LookIdentityDbContext context, UserManager<ApplicationUser> userManager)
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
        public IActionResult Privacy()
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