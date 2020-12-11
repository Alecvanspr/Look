using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Look.Models;

namespace Look.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Meldingen()
        {
            return View();
        }

        public IActionResult Profiel()
        {
            return View();  
        }
        //Neppe ingelogde gebruiker
        static Gebruiker IngelogdeGebruiker = new Gebruiker()
        {
                GebruikersNummer = 1,
                VoorNaam = "Ingelogde",
                AchterNaam = "Gozer",
                GebruikersNaam = "Buurman11034",
                EmailAdres = "i.gozer@gmail.com",
                WachtWoord = "gozer123",
                IsAnoniem = false
        };

        [HttpPost]
        public IActionResult Edit(string emailAdres, string voorNaam, string achterNaam, string wachtWoord, string nieuwWachtwoord, string herhaalWachtwoord, bool isAnoniem) {
            if(wachtWoord == IngelogdeGebruiker.WachtWoord) 
            {
                if(nieuwWachtwoord == herhaalWachtwoord) 
                {
                    IngelogdeGebruiker.WachtWoord = nieuwWachtwoord;

                    Console.WriteLine(IngelogdeGebruiker.WachtWoord);
                } 
                else 
                {
                    Console.WriteLine(nieuwWachtwoord);
                    Console.WriteLine(herhaalWachtwoord);
                }
            } 
            else 
            {
                Console.WriteLine("Klopt niet");
            }

            Console.WriteLine(isAnoniem);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
