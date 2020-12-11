﻿using System;
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

        private static List<Gebruiker> gebruikers = new List<Gebruiker>();
    

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
            
            return View(query);
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

        public IActionResult Login()
        {
            
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Register(string fname, string lname, string username, string streetaddress, string housenumber, string addition, string city, string postalcode,string email, string password)
        {
            int AantalGebruikers = gebruikers.Count();
            int GebruikersNummer = AantalGebruikers + 1;
            bool DoesExist = gebruikers.Any(gebruiker => gebruiker.EmailAdres == email);

            if(DoesExist)
            {
                Console.WriteLine("User Already Exists");
            }else
            {
                Gebruiker NieuweGebruiker = new Gebruiker() {GebruikersNummer = GebruikersNummer, VoorNaam = fname, AchterNaam = lname, GebruikersNaam = username, Adres = streetaddress + " " + housenumber + " " + addition + ", " + postalcode + ", " + city, EmailAdres = email, WachtWoord = password,IsAnoniem = false, IsGeverifieerd = false};
                gebruikers.Add(NieuweGebruiker);
                Console.WriteLine("Succesfully Created A New User In List<Gebruiker> gebruikers!");
            }

            return RedirectToAction("Index");;
        }

        public IActionResult Register()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
