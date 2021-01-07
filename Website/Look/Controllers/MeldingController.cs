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
    public class LikeInfo{
            public int aantal{get;set;}
        }
    public class MeldingController : Controller
    {
        private static List<Gebruiker> _gebruikers = new List<Gebruiker>();
        private readonly LookContext _context; 
        public long LaatstemeldingID;

        public MeldingController(LookContext context)
        {
            //CheckMeldingenOpDatum();
            _context = context;
            LaatstemeldingID = _context.Meldingen.OrderByDescending(m=>m.MeldingId).ToList().First().MeldingId+1;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostComment([Bind("bericht")] Reactie reactie)
        {
            if (ModelState.IsValid)
            {
                reactie.ReactieId= _context.Meldingen.Where(m=>m.MeldingId==1).First().Reacties.Count()+1;
                reactie.GeplaatstOp = DateTime.Now;
                reactie.Likes = 0;
                Console.WriteLine("Yes");
                _context.Meldingen.Where(m=>m.MeldingId==1).First().Reacties.Add(reactie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Meldingen));
            }
            return View(reactie);
        }
        public List<Reactie> MaakFakeReacties(){
            List<Reactie> reacties = new List<Reactie>();
            Reactie reactie = new Reactie();
            reactie.ReactieId = 1;
            reactie.GeplaatstDoor = _context.Gebruikers.Where(g=>g.GebruikersNummer==3).First();
            reactie.GeplaatstOp = DateTime.Now;
            reactie.Bericht ="Zuig mijn lul";
            reactie.Likes = 0;
            reacties.Add(reactie);
            return reacties;
        }

        public IActionResult CreateMelding()
        {
            try{
            int? isNull = this.HttpContext.Session.GetInt32("IdGebruiker").Value;
            return View();
            }catch
            {
                return RedirectToAction("Melding");
            }
        }
        public JsonResult Like()
        {
            return Json(new LikeInfo { aantal =_context.Meldingen.Where(m=>m.MeldingId==1).First().Likes});
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateMelding([Bind("Titel,Inhoud,Categorie,IsPrive")] Melding melding)
        {
            if (ModelState.IsValid)
            {
                int auteur = this.HttpContext.Session.GetInt32("IdGebruiker").Value;
                Console.WriteLine("de autheur is"+auteur);
                melding.Auteur = _context.Gebruikers.Where(g=>g.GebruikersNummer==auteur).First();//dit werkt 
                Console.WriteLine("de autheur is"+  melding.Auteur); //maar later staat die op null zonder reden
                LaatstemeldingID++;
                melding.MeldingId = LaatstemeldingID;
                melding.AangemaaktOp = DateTime.Now;
                melding.Likes=0;
                melding.Views=0;
                _context.Add(melding);
                await _context.SaveChangesAsync();
                Console.WriteLine("de autheur is :/"+_context.Meldingen.Where(m=>m.MeldingId==melding.MeldingId).First().Auteur.VoorNaam);
                Console.WriteLine("melding staat "+_context.Meldingen.Skip(1).First().Auteur); //dit is ineens niet meer null
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
            Console.WriteLine("melding staat delete "+_context.Meldingen.Skip(1).First().Auteur); //dit is ineens niet meer nul
            var melding = await _context.Meldingen
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
            var melding = await _context.Meldingen.FindAsync(MeldingId);
            _context.Meldingen.Remove(melding);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Meldingen));
        }

         public static List<Melding> query=null;

        //s is sorteren, z is zoeken

            public IActionResult Meldingen(string s,string z, int page = 0)
        {
            //dit zijn de de termen waarop het gesorteerd wordt
            ViewData["Sorteer"] = s ?? "datum";
            ViewData["Zoek"] = z ?? "";

            //dit maakt een lijst aan waarop het gesorteerd wordt
            var gebruikers = _context.Gebruikers.Where(g=>g.GebruikersNummer==3).First();//dit werkt 
            var meldingen = _context.Meldingen;
            List<Melding> meldings = meldingen.ToList();
            
            foreach (var melding in meldings)
            {
                Console.WriteLine("melding is "+ _context.Meldingen); //dit is null
            }

            //Check of er een gebruiker is ingelogd.
            var CurrentSession = this.HttpContext.Session.GetString("Naam");
            var DeveloperSession = "Developer";
            LaatstemeldingID = meldings.Count();
            
            //dit zorgt ervoor dat je kan sorteren als je op pagina 0 zit zonder dat je lijst weg gaat als je naar andere pagias gaat
            if(page==0){
                //dit zorgt ervoor dat je kan zoeken
                if(z!=null){
                    query = meldings.Where(m=>m.Categorie.Contains(z)).ToList();
                }else{
                    query = meldings;
                }

                //dit zorgt ervoor dat je kan sorteren
                if(s!=null){
                    if(s.Equals("likes")){
                        query = query.OrderByDescending(M=>M.Likes).ToList();
                    }else if(s.Equals("views")){
                        query = query.OrderByDescending(M=>M.Views).ToList();
                    }else if(s.Equals("titels")){
                        query = query.OrderByDescending(M=>M.Titel).ToList();
                    }else if(s.Equals("datum")){
                        query = query.OrderByDescending(M=>M.AangemaaktOp).ToList();
                    }
                }
            }
            //dit geeft het aantal tabs
            const int pageSize = 3;
            var count = query.Count();
            var data = query.Skip(page * pageSize).Take(pageSize).ToList();
            this.ViewBag.MaxPage = (count / pageSize) - (count % pageSize == 0 ? 1 : 0);
            this.ViewBag.Page = page;



            //Toon de views mits de gebruiker is ingelogd.
            if(CurrentSession != null || DeveloperSession != null)
            {
                return View(data);
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
            foreach (var melding in _context.Meldingen.Where(m=>m.AangemaaktOp<VerloopDatum))
            {
                _context.Meldingen.Remove(melding);
                await _context.SaveChangesAsync();
            }
        }
    }
}