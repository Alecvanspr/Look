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
        private LookContext db = new LookContext();
        public string UserHostAddress {get; set;}
        static long LaatstemeldingID;
    

        public HomeController(ILogger<HomeController> logger)
        {
            CheckMeldingenOpDatum();
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
       [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostComment([Bind("bericht")] Reactie reactie)
        {
            if (ModelState.IsValid)
            {
                reactie.ReactieId= db.Meldingen.Where(m=>m.MeldingId==1).First().Reacties.Count()+1;
                reactie.GeplaatstOp = DateTime.Now;
                reactie.Likes = 0;
                Console.WriteLine("Yes");
                db.Meldingen.Where(m=>m.MeldingId==1).First().Reacties.Add(reactie);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Meldingen));
            }
            return View(reactie);
        }
        public IActionResult CreateMelding()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateMelding([Bind("Titel,Inhoud,Categorie")] Melding melding)
        {
            if (ModelState.IsValid)
            {
                melding.MeldingId = LaatstemeldingID;
                melding.AangemaaktOp = DateTime.Now;
                melding.Likes = 0;
                melding.Views=0;
                db.Add(melding);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Meldingen));
            }
            return View(melding);
        }
        public async Task<IActionResult> Delete(string? titel)
        {
            if (titel == null)
            {
                return NotFound();
            }

            var melding = await db.Meldingen
                .FirstOrDefaultAsync(m => m.Titel == titel);
            if (melding == null)
            {
                return NotFound();
            }

            return View(melding);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long MeldingId)
        {
            var melding = await db.Meldingen.FindAsync(MeldingId);
            db.Meldingen.Remove(melding);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Meldingen));
        }

        //s is sorteren, z is zoeken
            public async Task<IActionResult> Meldingen(string s,string z, int page = 0)
        {
            var meldingen = db.Meldingen;
            List<Melding> meldings = meldingen.ToList();
            //Check of er een gebruiker is ingelogd.
            var CurrentSession = this.HttpContext.Session.GetString("Naam");
            var DeveloperSession = "Developer";
            LaatstemeldingID = meldings.Count();
            
            List<Melding> query =  null;
            
            if(z!=null){
                query = meldings.Where(m=>m.Categorie.Contains(z)).ToList();
            }else{
                query = meldings;
            }

            if(s!=null){
                if(s.Equals("likes")){
                    query = meldings.OrderByDescending(M=>M.Likes).ToList();
                }else if(s.Equals("views")){
                    query = meldings.OrderByDescending(M=>M.Views).ToList();
                }else if(s.Equals("titels")){
                    query = meldings.OrderByDescending(M=>M.Titel).ToList();
                }else if(s.Equals("datum")){
                    query = meldings.OrderByDescending(M=>M.AangemaaktOp).ToList();
                }else if(s.Equals("reacties")){
                    //query = meldings.OrderByDescending(M=>M.Reacties.Count()).ToList();
                }
            }

            const int pageSize = 3;
            var count = this.db.Meldingen.Count();
            var data = this.db.Meldingen.Skip(page * pageSize).Take(pageSize).ToList();
            this.ViewBag.MaxPage = (count / pageSize) - (count % pageSize == 0 ? 1 : 0);
            this.ViewBag.Page = page;



            //Toon de views mits de gebruiker is ingelogd.
            if(CurrentSession != null || DeveloperSession != null)
            {
                if(query.Count()!=0){
                    return View(data);
                }else{
                    return View(data);
                }
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public async void CheckMeldingenOpDatum(){
            DateTime VerloopDatum = DateTime.Now;
            VerloopDatum = VerloopDatum.AddDays(-30);
            //addMonths
            foreach (var melding in db.Meldingen.Where(m=>m.AangemaaktOp<VerloopDatum))
            {
                db.Meldingen.Remove(melding);
                await db.SaveChangesAsync();
            }
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
                Gebruiker IngelogdeGebruiker = db.Gebruikers.Where(s => s.GebruikersNummer == CurrentSessionUserId).FirstOrDefault();
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
            Gebruiker IngelogdeGebruiker = db.Gebruikers.Where(s => s.GebruikersNummer == CurrentSessionUserId).FirstOrDefault();

            //Verwijder de gebruiker uit de database
            db.Gebruikers.Remove(IngelogdeGebruiker);
            db.SaveChanges();
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
                Gebruiker _gebruiker = db.Gebruikers.Where(s => s.GebruikersNummer == CurrentSessionUserId).FirstOrDefault();

                //Voeg dezelfde encryptie toe aan het meegegeven wachtwoord en vergelijk deze met de wachtwoorden uit de database
                var f_password = GetMD5(wachtWoord);
                var data = db.Gebruikers.Where(s => s.GebruikersNummer.Equals(CurrentSessionUserId) && s.WachtWoord.Equals(f_password)).ToList();

                //Check of er al een gebruiker bestaat met het opgegeven e-mailadres
                var EmailCheck = db.Gebruikers.FirstOrDefault(s => s.EmailAdres.Equals(emailAdres.ToLower()));

                //Check of er al een gebruiker bestaat met de opgegeven gebruikersnaam
                var UserNameCheck = db.Gebruikers.FirstOrDefault(s => s.GebruikersNaam.Equals(gebruikersNaam.ToLower()));

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
                            db.Update(_gebruiker);
                            db.SaveChanges();
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
            Gebruiker IngelogdeGebruiker = db.Gebruikers.Where(s => s.GebruikersNummer == CurrentSessionUserId).FirstOrDefault();
            Console.WriteLine("Gebruiker met 'GebruikersNummer' {0} heeft zijn Profiel gewijzigd.", IngelogdeGebruiker.GebruikersNummer);

            return View("Profiel", IngelogdeGebruiker);
        }

        [HttpPost]
        public IActionResult Wachtwoord(string wachtWoord, string nieuwWachtWoord, string herhaalWachtwoord)
        {
            //Haal het GebruikersNummer op van de ingelogde gebruiker
            var CurrentSessionUserId = this.HttpContext.Session.GetInt32("IdGebruiker").Value;

            //Haal op welke gebruiker er is ingelogd.
            Gebruiker _gebruiker = db.Gebruikers.Where(s => s.GebruikersNummer == CurrentSessionUserId).FirstOrDefault();

            //Voeg dezelfde encryptie toe aan het meegegeven wachtwoord en vergelijk deze met de wachtwoorden uit de database
            var f_password = GetMD5(wachtWoord);
            var data = db.Gebruikers.Where(s => s.GebruikersNummer.Equals(CurrentSessionUserId) && s.WachtWoord.Equals(f_password)).ToList();

            if(data.Count > 0) {
                _gebruiker.WachtWoord = GetMD5(nieuwWachtWoord);
                db.Update(_gebruiker);
                db.SaveChanges();

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

            Gebruiker IngelogdeGebruiker = db.Gebruikers.Where(s => s.GebruikersNummer == CurrentSessionUserId).FirstOrDefault();
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
                var data = db.Gebruikers.Where(s => s.EmailAdres.Equals(email) && s.WachtWoord.Equals(f_password)).ToList();

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
                    db.Gebruikers.FirstOrDefault(s => s.EmailAdres.Equals(email)).LoginPogingen = 0;
                    db.SaveChanges();
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
                    db.Gebruikers.FirstOrDefault(s => s.EmailAdres.Equals(email)).LoginPogingen += 1;
                    db.SaveChanges();
                    Console.WriteLine("INFO: Dit is loginpoging nummer: " + db.Gebruikers.FirstOrDefault(s => s.EmailAdres.Equals(email)).LoginPogingen);

                    //Toon de <a> tag "Wachtwoord vergeten" op het moment dat 'LoginPogingen' groter of gelijk is aan 3
                    if(db.Gebruikers.FirstOrDefault(s => s.EmailAdres.Equals(email)).LoginPogingen >= 3)
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
                var EmailCheck = db.Gebruikers.FirstOrDefault(s => s.EmailAdres.Equals(_gebruiker.EmailAdres));

                //Check of er al een gebruiker bestaat met de opgegeven gebruikersnaam
                var UserNameCheck = db.Gebruikers.FirstOrDefault(s => s.GebruikersNaam.Equals(_gebruiker.GebruikersNaam));

                if(EmailCheck == null)
                {
                    if(UserNameCheck == null)
                    {
                        //Encrypt het meegegeven wachtwoord en vervang die voor het wachtwoord dat meegegeven is vanuit de form
                        _gebruiker.WachtWoord = GetMD5(_gebruiker.WachtWoord);
                        db.Gebruikers.Add(_gebruiker);
                        db.SaveChanges();
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