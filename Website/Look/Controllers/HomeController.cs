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


namespace Look.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private static List<Gebruiker> _gebruikers = new List<Gebruiker>();
        private LookContext db = new LookContext();
        public string UserHostAddress {get; set;}
    

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
        public IActionResult Meldingen(string s)
        {
            //Check of er een gebruiker is ingelogd.
            var CurrentSession = this.HttpContext.Session.GetString("Naam");

            Gebruiker Alec = new Gebruiker{VoorNaam="Alec",AchterNaam="van Spronsen"};
            Gebruiker Dechaun = new Gebruiker{VoorNaam="Dechaun",AchterNaam="Bakker"};
            Gebruiker Scott = new Gebruiker{VoorNaam="Scott",AchterNaam="van Duin"}; 
            Gebruiker Joeri = new Gebruiker{VoorNaam="Joeri",AchterNaam="de Hoog"};
            List<Melding> meldings = new List<Melding>();
            List<Reactie> reacties1 = new List<Reactie>();
            List<Reactie> reacties2 = new List<Reactie>();
            List<Reactie> reacties3 = new List<Reactie>();
            List<Reactie> reacties4 = new List<Reactie>();
                Reactie reactie1 = new Reactie {ReactieId=1, Bericht="LOL",GeplaatstDoor=Alec,GeplaatstOp=new DateTime(2020, 12, 9),likes=3};
                Reactie reactie2 = new Reactie {ReactieId=2,Bericht="bruh",GeplaatstDoor=Alec,GeplaatstOp=new DateTime(2020, 12, 9),likes=1};
                Reactie reactie3 = new Reactie {ReactieId=3,Bericht="Licht geïrriteerde opmerking over Sinterklaas zijn hulpjes",GeplaatstDoor=Joeri,GeplaatstOp=new DateTime(2020,12,11),likes=1232};
            reacties1.Add(reactie1);
            reacties3.Add(reactie1);
            reacties1.Add(reactie2);
            var melding1 = new Melding()  {MeldingId= 1, AangemaaktOp=new DateTime(2020, 12, 8),Titel="Lorem Ipsum 1", Inhoud="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam vitae vehicula elit, quis porttitor eros. Ut tincidunt felis tortor, et lacinia turpis imperdiet et. Integer rhoncus lacus dui, id commodo libero aliquet non. Suspendisse quis felis risus. Nulla eu metus tincidunt, ultricies tortor nec, ultricies turpis. Suspendisse vitae lacinia nulla. Ut imperdiet varius finibus. Proin interdum libero a mi iaculis venenatis. Donec aliquet varius dui, non mollis neque sollicitudin id. Sed lorem quam, porta ac arcu non, pellentesque cursus augue. Ut ac est mauris. Duis tristique ante vitae interdum dictum. Fusce dictum mattis urna eu mattis. Duis vitae rutrum nisl. Mauris ultrices pulvinar neque sed blandit.",Likes=225,Views=1200,Categorie="Gevonden voorwerp",IsActief=true,Auteur=Alec,Reacties=reacties1};
            var melding2 = new Melding()  {MeldingId= 2, AangemaaktOp=new DateTime(2020, 12, 5),Titel="Lorem Ipsum 2", Inhoud="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam vitae vehicula elit, quis porttitor eros. Ut tincidunt felis tortor, et lacinia turpis imperdiet et. Integer rhoncus lacus dui, id commodo libero aliquet non. Suspendisse quis felis risus. Nulla eu metus tincidunt, ultricies tortor nec, ultricies turpis. Suspendisse vitae lacinia nulla. Ut imperdiet varius finibus. Proin interdum libero a mi iaculis venenatis. Donec aliquet varius dui, non mollis neque sollicitudin id. Sed lorem quam, porta ac arcu non, pellentesque cursus augue. Ut ac est mauris. Duis tristique ante vitae interdum dictum. Fusce dictum mattis urna eu mattis. Duis vitae rutrum nisl. Mauris ultrices pulvinar neque sed blandit.",Likes=4,Views=7,Categorie="Gevonden voorwerp",IsActief=true,Auteur=Dechaun};
            var melding3 = new Melding()  {MeldingId= 3, AangemaaktOp=new DateTime(2020, 12, 2),Titel="Lorem Ipsum 3", Inhoud="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam vitae vehicula elit, quis porttitor eros. Ut tincidunt felis tortor, et lacinia turpis imperdiet et. Integer rhoncus lacus dui, id commodo libero aliquet non. Suspendisse quis felis risus. Nulla eu metus tincidunt, ultricies tortor nec, ultricies turpis. Suspendisse vitae lacinia nulla. Ut imperdiet varius finibus. Proin interdum libero a mi iaculis venenatis. Donec aliquet varius dui, non mollis neque sollicitudin id. Sed lorem quam, porta ac arcu non, pellentesque cursus augue. Ut ac est mauris. Duis tristique ante vitae interdum dictum. Fusce dictum mattis urna eu mattis. Duis vitae rutrum nisl. Mauris ultrices pulvinar neque sed blandit.",Likes=4,Views=7,Categorie="Gevonden voorwerp",IsActief=true,Auteur=Joeri};
            var melding4 = new Melding()  {MeldingId= 4, AangemaaktOp=new DateTime(2020, 11, 25),Titel="Lorem Ipsum 4", Inhoud="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam vitae vehicula elit, quis porttitor eros. Ut tincidunt felis tortor, et lacinia turpis imperdiet et. Integer rhoncus lacus dui, id commodo libero aliquet non. Suspendisse quis felis risus. Nulla eu metus tincidunt, ultricies tortor nec, ultricies turpis. Suspendisse vitae lacinia nulla. Ut imperdiet varius finibus. Proin interdum libero a mi iaculis venenatis. Donec aliquet varius dui, non mollis neque sollicitudin id. Sed lorem quam, porta ac arcu non, pellentesque cursus augue. Ut ac est mauris. Duis tristique ante vitae interdum dictum. Fusce dictum mattis urna eu mattis. Duis vitae rutrum nisl. Mauris ultrices pulvinar neque sed blandit.",Likes=5,Views=8,Categorie="Gevonden voorwerp",IsActief=false,Auteur=Scott};
            var melding5 = new Melding()  {MeldingId= 5, AangemaaktOp=new DateTime(2020, 12, 20),Titel="Lorem Ipsum 5", Inhoud="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam vitae vehicula elit, quis porttitor eros. Ut tincidunt felis tortor, et lacinia turpis imperdiet et. Integer rhoncus lacus dui, id commodo libero aliquet non. Suspendisse quis felis risus. Nulla eu metus tincidunt, ultricies tortor nec, ultricies turpis. Suspendisse vitae lacinia nulla. Ut imperdiet varius finibus. Proin interdum libero a mi iaculis venenatis. Donec aliquet varius dui, non mollis neque sollicitudin id. Sed lorem quam, porta ac arcu non, pellentesque cursus augue. Ut ac est mauris. Duis tristique ante vitae interdum dictum. Fusce dictum mattis urna eu mattis. Duis vitae rutrum nisl. Mauris ultrices pulvinar neque sed blandit.",Likes=123,Views=553,Categorie="Gevonden voorwerp",IsActief=false};
            
            meldings.Add(melding1);
            meldings.Add(melding2);
            meldings.Add(melding3);
            meldings.Add(melding4);
            meldings.Add(melding5);

            List<Melding> query = meldings;

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

            //Toon de views mits de gebruiker is ingelogd.
            if(CurrentSession != null)
            {
                return View(query);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }


        public IActionResult Profiel()
        {
            //Check of er een gebruiker is ingelogd.
            var CurrentSession = this.HttpContext.Session.GetString("Naam");
            
            //Toon de views mits de gebruiker is ingelogd.
            if(CurrentSession != null)
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

        [HttpPost]
        public IActionResult Profiel(string emailAdres, string voorNaam, string achterNaam, string wachtWoord, string nieuwWachtwoord, string herhaalWachtwoord, bool isAnoniem) {
            
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

                //Als er meerdere Gebruikers zijn geteld in de database komt het wachtwoord overeen met die uit de database
                if(data.Count() > 0)
                {
                    //Wijzig de gegevens en update deze in de database
                    _gebruiker.VoorNaam = voorNaam;
                    _gebruiker.AchterNaam = achterNaam;
                    _gebruiker.EmailAdres = emailAdres;
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
                    ViewBag.editErrorText = "Het opgegeven huidige wachtwoord is incorrect.";
                    Console.WriteLine("ERROR: Couldn't alter data, password incorrect");
                }
            }

            //Haal de gewijzigde gebruiker op
            Gebruiker IngelogdeGebruiker = db.Gebruikers.Where(s => s.GebruikersNummer == CurrentSessionUserId).FirstOrDefault();
            Console.WriteLine("Gebruiker met 'GebruikersNummer' {0} heeft zijn Profiel gewijzigd.", IngelogdeGebruiker.GebruikersNummer);

            return View("Profiel", IngelogdeGebruiker);
        }

        

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
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
            return RedirectToAction("Login");
        }

        public IActionResult Login()
        {
            
            return View();
        }

        
        public IActionResult Register()
        {
            return View();
        }

        public IActionResult EmailVerificatie()
        {
            return View();
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
                GebruikersNaam = username, 
                Straat = streetaddress, 
                HuisNummer = housenumber, 
                Woonplaats = city, 
                PostCode = postalcode, 
                EmailAdres = email, 
                WachtWoord = password, 
                VerifieerWachtWoord = passwordvalid, 
                IsGeverifieerd = false, 
                IsAnoniem = false 
            };

            //Check of de meegegeven model/parameters voldoen aan de Data Annotations in de Model.cs
            if(ModelState.IsValid)
            {
                //Check of er al een gebruiker bestaat met het opgegeven e-mailadres
                var check = db.Gebruikers.FirstOrDefault(s => s.EmailAdres == _gebruiker.EmailAdres);

                if(check == null)
                {
                    //Encrypt het meegegeven wachtwoord en vervang die voor het wachtwoord dat meegegeven is vanuit de form
                    _gebruiker.WachtWoord = GetMD5(_gebruiker.WachtWoord);
                    db.Gebruikers.Add(_gebruiker);
                    db.SaveChanges();
                    Console.WriteLine("SUCCES: Successfull registration of user");

                    return RedirectToAction("Index");;
                }
                else 
                {
                    //Geef een error zodra het opgegeven e-mailadres al in gebruik is
                    ViewBag.registerError = "Het opgegeven e-mailadres is al gelinkt aan een acount.";
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

        public IActionResult EmailVerificatie()
        {
            return View();
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
