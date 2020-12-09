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
        public IActionResult Meldingen(string sorteerVolgorde)
        {
            Gebruiker Alec = new Gebruiker{VoorNaam="Alec",AchterNaam="van Spronsen"};
            Gebruiker Dechaun = new Gebruiker{VoorNaam="Dechaun",AchterNaam="Bakker"};
            Gebruiker Scott = new Gebruiker{VoorNaam="Scott",AchterNaam="van Duin"}; 
            Gebruiker Joeri = new Gebruiker{VoorNaam="Joeri",AchterNaam="de hoog"};
            List<Melding> meldings = new List<Melding>();
            List<Reactie> reacties1 = new List<Reactie>();
                Reactie reactie1 = new Reactie {Bericht="LOL",GeplaatstDoor=Alec,GeplaatstOp=new DateTime(2020, 12, 9)};
                Reactie reactie2 = new Reactie {Bericht="bruh",GeplaatstDoor=Alec,GeplaatstOp=new DateTime(2020, 12, 9)};
            reacties1.Add(reactie1);
            reacties1.Add(reactie2);
            var melding1 = new Melding()  {meldingID="1", AangemaaktOp=new DateTime(2020, 12, 8),Titel="Lorem Ipsum 1", Inhoud="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam vitae vehicula elit, quis porttitor eros. Ut tincidunt felis tortor, et lacinia turpis imperdiet et. Integer rhoncus lacus dui, id commodo libero aliquet non. Suspendisse quis felis risus. Nulla eu metus tincidunt, ultricies tortor nec, ultricies turpis. Suspendisse vitae lacinia nulla. Ut imperdiet varius finibus. Proin interdum libero a mi iaculis venenatis. Donec aliquet varius dui, non mollis neque sollicitudin id. Sed lorem quam, porta ac arcu non, pellentesque cursus augue. Ut ac est mauris. Duis tristique ante vitae interdum dictum. Fusce dictum mattis urna eu mattis. Duis vitae rutrum nisl. Mauris ultrices pulvinar neque sed blandit.",Likes=225,Views=1200,Categorieen=new List<string>{"lorem","Ipsum"},IsActief=true,auteur=Alec,Reacties=reacties1};
            var melding2 = new Melding()  {meldingID="2", AangemaaktOp=new DateTime(2020, 12, 5),Titel="Lorem Ipsum 2", Inhoud="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam vitae vehicula elit, quis porttitor eros. Ut tincidunt felis tortor, et lacinia turpis imperdiet et. Integer rhoncus lacus dui, id commodo libero aliquet non. Suspendisse quis felis risus. Nulla eu metus tincidunt, ultricies tortor nec, ultricies turpis. Suspendisse vitae lacinia nulla. Ut imperdiet varius finibus. Proin interdum libero a mi iaculis venenatis. Donec aliquet varius dui, non mollis neque sollicitudin id. Sed lorem quam, porta ac arcu non, pellentesque cursus augue. Ut ac est mauris. Duis tristique ante vitae interdum dictum. Fusce dictum mattis urna eu mattis. Duis vitae rutrum nisl. Mauris ultrices pulvinar neque sed blandit.",Likes=4,Views=7,Categorieen=new List<string>{"lorem","Ipsum"},IsActief=true,auteur=Dechaun};
            var melding3 = new Melding()  {meldingID="3", AangemaaktOp=new DateTime(2020, 12, 2),Titel="Lorem Ipsum 3", Inhoud="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam vitae vehicula elit, quis porttitor eros. Ut tincidunt felis tortor, et lacinia turpis imperdiet et. Integer rhoncus lacus dui, id commodo libero aliquet non. Suspendisse quis felis risus. Nulla eu metus tincidunt, ultricies tortor nec, ultricies turpis. Suspendisse vitae lacinia nulla. Ut imperdiet varius finibus. Proin interdum libero a mi iaculis venenatis. Donec aliquet varius dui, non mollis neque sollicitudin id. Sed lorem quam, porta ac arcu non, pellentesque cursus augue. Ut ac est mauris. Duis tristique ante vitae interdum dictum. Fusce dictum mattis urna eu mattis. Duis vitae rutrum nisl. Mauris ultrices pulvinar neque sed blandit.",Likes=4,Views=7,Categorieen=new List<string>{"lorem","Ipsum"},IsActief=true,auteur=Joeri};
            var melding4 = new Melding()  {meldingID="4", AangemaaktOp=new DateTime(2020, 11, 25),Titel="Lorem Ipsum 4", Inhoud="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam vitae vehicula elit, quis porttitor eros. Ut tincidunt felis tortor, et lacinia turpis imperdiet et. Integer rhoncus lacus dui, id commodo libero aliquet non. Suspendisse quis felis risus. Nulla eu metus tincidunt, ultricies tortor nec, ultricies turpis. Suspendisse vitae lacinia nulla. Ut imperdiet varius finibus. Proin interdum libero a mi iaculis venenatis. Donec aliquet varius dui, non mollis neque sollicitudin id. Sed lorem quam, porta ac arcu non, pellentesque cursus augue. Ut ac est mauris. Duis tristique ante vitae interdum dictum. Fusce dictum mattis urna eu mattis. Duis vitae rutrum nisl. Mauris ultrices pulvinar neque sed blandit.",Likes=5,Views=8,Categorieen=new List<string>{"lorem","Ipsum"},IsActief=false,auteur=Scott};
            var melding5 = new Melding()  {meldingID="5", AangemaaktOp=new DateTime(2020, 12, 20),Titel="Lorem Ipsum 5", Inhoud="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam vitae vehicula elit, quis porttitor eros. Ut tincidunt felis tortor, et lacinia turpis imperdiet et. Integer rhoncus lacus dui, id commodo libero aliquet non. Suspendisse quis felis risus. Nulla eu metus tincidunt, ultricies tortor nec, ultricies turpis. Suspendisse vitae lacinia nulla. Ut imperdiet varius finibus. Proin interdum libero a mi iaculis venenatis. Donec aliquet varius dui, non mollis neque sollicitudin id. Sed lorem quam, porta ac arcu non, pellentesque cursus augue. Ut ac est mauris. Duis tristique ante vitae interdum dictum. Fusce dictum mattis urna eu mattis. Duis vitae rutrum nisl. Mauris ultrices pulvinar neque sed blandit.",Likes=123,Views=553,Categorieen=new List<string>{"lorem","Ipsum"},IsActief=false};
            meldings.Add(melding1);
            meldings.Add(melding2);
            meldings.Add(melding3);
            meldings.Add(melding4);
            meldings.Add(melding5);
            Melding ModalMelding = melding1;
            return View(meldings);
        }
        
        public IActionResult Profiel()
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
