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
using PagedList;

namespace Look.Controllers
{
     
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private static List<Gebruiker> _gebruikers = new List<Gebruiker>();
        private readonly LookContext _context;        
        public string UserHostAddress {get; set;}

        public HomeController(ILogger<HomeController> logger, LookContext context)
        {
            // CheckMeldingenOpDatum();
            _context = context;
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


        public IActionResult Profiel()
        {
            //Check of er een gebruiker is ingelogd.
            var CurrentSession = this.HttpContext.Session.GetString("Naam");
            var DeveloperSession = "Developer";

            //Toon de views mits de gebruiker is ingelogd.
            if(CurrentSession != null || DeveloperSession != null)
            {
                //Haal op welke gebruiker er is ingelogd.
                var CurrentSessionUserId = this.HttpContext.Session.GetInt32("IdGebruiker").Value;
                Gebruiker IngelogdeGebruiker = _context.Gebruikers.Where(s => s.GebruikersNummer == CurrentSessionUserId).FirstOrDefault();
                Console.WriteLine("Gebruiker met 'GebruikersNummer' {0} heeft het Profiel scherm geopend.", IngelogdeGebruiker.GebruikersNummer);

                return View("Profiel", IngelogdeGebruiker);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public IActionResult Login()
        {
            return View();
        }

        
        public IActionResult Register()
        {
            return View();
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        public IActionResult EmailVerificatie()
        {
            return View();
        }

        public IActionResult RemoveAccount(){

            //Haal op welke gebruiker er is ingelogd.
            var CurrentSessionUserId = this.HttpContext.Session.GetInt32("IdGebruiker").Value;
            Gebruiker IngelogdeGebruiker = _context.Gebruikers.Where(s => s.GebruikersNummer == CurrentSessionUserId).FirstOrDefault();

            //Verwijder de gebruiker uit de database
            _context.Gebruikers.Remove(IngelogdeGebruiker);
            _context.SaveChanges();
            Console.WriteLine("Gebruiker met 'GebruikersNummer' {0} is succesvol verwijderd.", IngelogdeGebruiker.GebruikersNummer);

            //Verwijder de sessie van de ingelogde gebruiker
            HttpContext.Session.Clear();
            return View("Index");
        }


        [HttpPost]
        public IActionResult NaamEnEmailadres(string gebruikersNaam, string emailAdres, string voorNaam, string achterNaam, string wachtWoord, string nieuwWachtwoord, string herhaalWachtwoord, bool isAnoniem) {
            
            //Haal het GebruikersNummer op van de ingelogde gebruiker
            var CurrentSessionUserId = this.HttpContext.Session.GetInt32("IdGebruiker").Value;

            //Check of de meegegeven model/parameters voldoen aan de Data Annotations in de Model.cs
            if (ModelState.IsValid)
            {
                //Haal op welke gebruiker er is ingelogd.
                Gebruiker _gebruiker = _context.Gebruikers.Where(s => s.GebruikersNummer == CurrentSessionUserId).FirstOrDefault();

                //Voeg dezelfde encryptie toe aan het meegegeven wachtwoord en vergelijk deze met de wachtwoorden uit de database
                var f_password = GetMD5(wachtWoord);
                var data = _context.Gebruikers.Where(s => s.GebruikersNummer.Equals(CurrentSessionUserId) && s.WachtWoord.Equals(f_password)).ToList();

                //Check of er al een gebruiker bestaat met het opgegeven e-mailadres
                var EmailCheck = _context.Gebruikers.FirstOrDefault(s => s.EmailAdres.Equals(emailAdres.ToLower()));

                //Check of er al een gebruiker bestaat met de opgegeven gebruikersnaam
                var UserNameCheck = _context.Gebruikers.FirstOrDefault(s => s.GebruikersNaam.Equals(gebruikersNaam.ToLower()));

                //Als er meerdere Gebruikers zijn geteld in de database komt het wachtwoord overeen met die uit de database
                if(data.Count() > 0)
                {
                    if(EmailCheck == null)
                    {
                        if(UserNameCheck == null)
                        {
                            //Wijzig de gegevens en update deze in de database
                            _gebruiker.VoorNaam = voorNaam;
                            _gebruiker.AchterNaam = achterNaam;
                            _gebruiker.GebruikersNaam = gebruikersNaam.ToLower();
                            _gebruiker.EmailAdres = emailAdres.ToLower();
                            _gebruiker.WachtWoord = GetMD5(wachtWoord);
                            _gebruiker.IsAnoniem = isAnoniem;
                            _context.Update(_gebruiker);
                            _context.SaveChanges();
                            Console.WriteLine("SUCCES: Succesfully altered data");

                            //Toon de succes message in de HTML
                            ViewBag.editSuccess = true;
                            ViewBag.editError = false;
                            ViewBag.editSuccessText = "Je gegevens zijn succesvol gewijzigd.";
                        }
                        else
                        {
                            //Toon de error message in de HTML
                            ViewBag.editError = true;
                            ViewBag.editSuccess = false;
                            ViewBag.editErrorText = "De opgegeven gebruikersnaam is al in gebruik.";
                            Console.WriteLine("ERROR: Couldn't alter data, username in use");
                        }
                    }
                    else
                    {
                        //Toon de error message in de HTML
                        ViewBag.editError = true;
                        ViewBag.editSuccess = false;
                        ViewBag.editErrorText = "Het opgegeven e-mailadres is al in gebruik.";
                        Console.WriteLine("ERROR: Couldn't alter data, email in use");
                    }
                }
                else
                {
                    //Toon de error message in de HTML
                    ViewBag.editError = true;
                    ViewBag.editSuccess = false;
                    ViewBag.editErrorText = "Het opgegeven huidige wachtwoord is incorrect.";
                    Console.WriteLine("ERROR: Couldn't alter data, password incorrect");
                }
            }

            //Haal de gewijzigde gebruiker op
            Gebruiker IngelogdeGebruiker = _context.Gebruikers.Where(s => s.GebruikersNummer == CurrentSessionUserId).FirstOrDefault();
            Console.WriteLine("Gebruiker met 'GebruikersNummer' {0} heeft zijn Profiel gewijzigd.", IngelogdeGebruiker.GebruikersNummer);

            return View("Profiel", IngelogdeGebruiker);
        }

        [HttpPost]
        public IActionResult Wachtwoord(string wachtWoord, string nieuwWachtWoord, string herhaalWachtwoord)
        {
            //Haal het GebruikersNummer op van de ingelogde gebruiker
            var CurrentSessionUserId = this.HttpContext.Session.GetInt32("IdGebruiker").Value;

            //Haal op welke gebruiker er is ingelogd.
            Gebruiker _gebruiker = _context.Gebruikers.Where(s => s.GebruikersNummer == CurrentSessionUserId).FirstOrDefault();

            //Voeg dezelfde encryptie toe aan het meegegeven wachtwoord en vergelijk deze met de wachtwoorden uit de database
            var f_password = GetMD5(wachtWoord);
            var data = _context.Gebruikers.Where(s => s.GebruikersNummer.Equals(CurrentSessionUserId) && s.WachtWoord.Equals(f_password)).ToList();

            if(data.Count > 0) {
                _gebruiker.WachtWoord = GetMD5(nieuwWachtWoord);
                _context.Update(_gebruiker);
                _context.SaveChanges();

                ViewBag.editSuccess = true;
                ViewBag.editError = false;
                ViewBag.editSuccessText = "Het wachtwoord is succesvol gewijzigd.";
            }
            else
            {
                //Toon de error message in de HTML
                ViewBag.editError = true;
                ViewBag.editSuccess = false;
                ViewBag.editErrorText = "Het opgegeven huidige wachtwoord is incorrect.";
                Console.WriteLine("ERROR: Couldn't alter data, password incorrect");
            }

            Gebruiker IngelogdeGebruiker = _context.Gebruikers.Where(s => s.GebruikersNummer == CurrentSessionUserId).FirstOrDefault();
            Console.WriteLine("Gebruiker met 'GebruikersNummer' {0} heeft zijn wachtwoord gewijzigd.", IngelogdeGebruiker.GebruikersNummer);

            return View("Profiel", IngelogdeGebruiker);
        }

        //Methode die de Login form uitvoert zodra er op de submit knop is gedrukt
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            //Check of de meegegeven model/parameters voldoen aan de Data Annotations in de Model.cs
            if (ModelState.IsValid)
            {
                //Voeg dezelfde encryptie toe aan het meegegeven wachtwoord en vergelijk deze met de wachtwoorden uit de database
                var f_password = GetMD5(password);
                var data = _context.Gebruikers.Where(s => s.EmailAdres.Equals(email) && s.WachtWoord.Equals(f_password)).ToList();

                //Als er meerdere Gebruikers zijn geteld in de database is de gebruiker succesvol ingelogd
                if(data.Count() > 0)
                {
                    //Maak een session value aan voor de gebruiker die op het moment inlogt
                    HttpContext.Session.SetInt32("IdGebruiker", data.FirstOrDefault(s => s.EmailAdres.Equals(email)).GebruikersNummer);
                    HttpContext.Session.SetString("Email", data.FirstOrDefault(s => s.EmailAdres.Equals(email)).EmailAdres);
                    HttpContext.Session.SetString("Naam", data.FirstOrDefault(s => s.EmailAdres.Equals(email)).VoorNaam + " " + data.FirstOrDefault(s => s.EmailAdres.Equals(email)).AchterNaam);
                    HttpContext.Session.SetString("VoorNaam", data.FirstOrDefault(s => s.EmailAdres.Equals(email)).VoorNaam);
                    HttpContext.Session.SetString("AchterNaam", data.FirstOrDefault(s => s.EmailAdres.Equals(email)).AchterNaam);

                    //Reset 'LoginPogingen' naar 0 als de gebruiker succesvol heeft ingelogd en verberg de "Wachtwoord vergeten" <a> tag
                    _context.Gebruikers.FirstOrDefault(s => s.EmailAdres.Equals(email)).LoginPogingen = 0;
                    _context.SaveChanges();
                    ViewBag.LoginPogingDrie = false;
                    Console.WriteLine("SUCCCES: Authenticated, login successfull");

                    return RedirectToAction("Index");
                }
                else
                {
                    //Geef een error als de gebruiker niet in heeft kunnen loggen
                    ViewBag.loginError = true;
                    ViewBag.loginErrorText = "U heeft een onjuist e-mailadres of wachtwoord ingevoerd.";
                    Console.WriteLine("ERROR: Authentication failed, wrong email or password");

                    //Verhoog het getal 'LoginPogingen' met 1 elke keer dat er een fout voorkomt per e-mailadres en sla dit op in de database
                    _context.Gebruikers.FirstOrDefault(s => s.EmailAdres.Equals(email)).LoginPogingen += 1;
                    _context.SaveChanges();
                    Console.WriteLine("INFO: Dit is loginpoging nummer: " + _context.Gebruikers.FirstOrDefault(s => s.EmailAdres.Equals(email)).LoginPogingen);

                    //Toon de <a> tag "Wachtwoord vergeten" op het moment dat 'LoginPogingen' groter of gelijk is aan 3
                    if(_context.Gebruikers.FirstOrDefault(s => s.EmailAdres.Equals(email)).LoginPogingen >= 3)
                    {
                        ViewBag.LoginPogingDrie = true;
                    }
                    else
                    {
                        ViewBag.LoginPogingDrie = false;
                    }
                }
            }
            return View();
        }

        //Methode die de Register form uitvoert zodra er op de submit knop is gedrukt
        [HttpPost]
        public IActionResult Register(string fname, string lname, string username, string streetaddress, string housenumber, string city, string postalcode,string email, string password, string passwordvalid)
        {
            //Maak een object aan met de meegegeven parameters
            Gebruiker _gebruiker = new Gebruiker
            { 
                VoorNaam = fname, 
                AchterNaam = lname, 
                GebruikersNaam = username.ToLower(), 
                Straat = streetaddress, 
                HuisNummer = housenumber, 
                Woonplaats = city, 
                PostCode = postalcode, 
                EmailAdres = email.ToLower(), 
                WachtWoord = password, 
                VerifieerWachtWoord = passwordvalid, 
                IsGeverifieerd = false, 
                IsAnoniem = false 
            };

            //Check of de meegegeven model/parameters voldoen aan de Data Annotations in de Model.cs
            if(ModelState.IsValid)
            {
                //Check of er al een gebruiker bestaat met het opgegeven e-mailadres
                var EmailCheck = _context.Gebruikers.FirstOrDefault(s => s.EmailAdres.Equals(_gebruiker.EmailAdres));

                //Check of er al een gebruiker bestaat met de opgegeven gebruikersnaam
                var UserNameCheck = _context.Gebruikers.FirstOrDefault(s => s.GebruikersNaam.Equals(_gebruiker.GebruikersNaam));

                if(EmailCheck == null)
                {
                    if(UserNameCheck == null)
                    {
                        //Encrypt het meegegeven wachtwoord en vervang die voor het wachtwoord dat meegegeven is vanuit de form
                        _gebruiker.WachtWoord = GetMD5(_gebruiker.WachtWoord);
                        _context.Gebruikers.Add(_gebruiker);
                        _context.SaveChanges();
                        ViewBag.registerError = false;
                        Console.WriteLine("SUCCES: Successfull registration of user");

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.registerError = true;
                        ViewBag.registerErrorText = "De opgegeven gebruikersnaam is al in gebruik.";
                        Console.WriteLine("ERROR: Username already exists");
                        return View();
                    }
                    
                }
                else 
                {
                    //Geef een error zodra het opgegeven e-mailadres al in gebruik is
                    ViewBag.registerError = true;
                    ViewBag.registerErrorText = "Het opgegeven e-mailadres is al in gebruik.";
                    Console.WriteLine("ERROR: Email already exists");
                    return View();
                }
            }
            return View();
        }

        //Methode die de errors handelt
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //Methode om wachtwoorden te encrypten
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }
    }
}