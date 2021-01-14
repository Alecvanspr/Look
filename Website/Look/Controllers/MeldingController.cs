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
        public class MeldingController : Controller
    {
        private static List<Gebruiker> _gebruikers = new List<Gebruiker>();
        private readonly LookContext _context; 
        public long LaatstemeldingID;
        public long UniekMeldingID;

        public MeldingController(LookContext context)
        {
            //CheckMeldingenOpDatum();
            _context = context;
            LaatstemeldingID = _context.Meldingen.OrderByDescending(m=>m.MeldingId).ToList().First().MeldingId+1;
            UniekMeldingID = _context.Reacties.OrderByDescending(r=>r.ReactieId).ToList().First().ReactieId+1;
        }

        public List<Reactie> MaakFakeReacties(){
            List<Reactie> reacties = new List<Reactie>();
            Reactie reactie = new Reactie();
            reactie.ReactieId = UniekMeldingID;
            UniekMeldingID++;
            reactie.GeplaatstDoor = _context.Gebruikers.Where(g=>g.GebruikersNummer==3).First();
            reactie.GeplaatstOp = DateTime.Now;
            reactie.Bericht ="Een reactie";
            reactie.Likes = 0;
            reacties.Add(reactie);
            return reacties;
        }

        public ActionResult Like(long id)
        {
            var CurrentSessionUserId = this.HttpContext.Session.GetInt32("IdGebruiker").Value;
            Gebruiker IngelogdeGebruiker = _context.Gebruikers.Where(p => p.GebruikersNummer == CurrentSessionUserId).FirstOrDefault();
            Melding _melding = _context.Meldingen.Where(p => p.MeldingId == id).FirstOrDefault();
            Liked _liked = _context.Liked.Where(p => p.MeldingId == id && p.GebruikersNummer == IngelogdeGebruiker.GebruikersNummer).FirstOrDefault();
            Liked _newLiked = new Liked();
            _newLiked.GebruikersNummer = IngelogdeGebruiker.GebruikersNummer;
            _newLiked.MeldingId = _melding.MeldingId;
                        
            var CurrentSession = this.HttpContext.Session.GetString("Naam");
            var DeveloperSession = "Developer";

            if (CurrentSession != null || DeveloperSession != null)
            {
                if (_liked == null)
                {
                    _melding.Likes++;
                    _newLiked.heeftGeliked = true;
                    _context.Liked.Add(_newLiked);
                    _context.SaveChanges();
                } else {
                    if (_liked.heeftGeliked == false)
                    {
                        _melding.Likes++;
                        _liked.heeftGeliked = true;
                        _context.Update(_melding);
                        _context.Update(_liked);
                        _context.SaveChanges();
                    } else {
                        _melding.Likes--;
                        _liked.heeftGeliked = false;
                        _context.Update(_melding);
                        _context.Update(_liked);
                        _context.SaveChanges();
                    }
                }
            } else {
                return RedirectToAction("Login");
            }
            return RedirectToAction(nameof(Meldingen)); //TODO: op dezelfde pagina blijven wanneer een bericht is geliked
        }

        public IActionResult PlaatsBericht()
        {
            try{
            int? isNull = this.HttpContext.Session.GetInt32("IdGebruiker").Value;
            return View();
            }catch
            {
                return RedirectToAction(nameof(Meldingen)); //todo dit moet inlog scherm worden
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PlaatsBericht([Bind("Bericht")] Reactie reactie){
            if (ModelState.IsValid)
            {
                //dit zorgt ervoor dat de momenteele auteursnummer wordt opgeroepen
                int auteur = this.HttpContext.Session.GetInt32("IdGebruiker").Value;
                reactie.GeplaatstDoor = _context.Gebruikers.Where(g=>g.GebruikersNummer==auteur).First();
                var inladen = _context.Meldingen.Where(m=>m.MeldingId==15).First().Reacties.First().ReactieId;

                //dit maakt het id vaan de melding aan
                reactie.ReactieId =UniekMeldingID;
                UniekMeldingID++;

                reactie.GeplaatstOp = DateTime.Now;
                reactie.Likes = 0;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Meldingen));
            }
            return View(nameof(Meldingen));
        }
        
        public IActionResult CreateMelding()
        {
            try{
            int? isNull = this.HttpContext.Session.GetInt32("IdGebruiker").Value;
            return View();
            }catch
            {
                return RedirectToAction(nameof(Meldingen));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateMelding([Bind("Titel,Inhoud,Categorie,IsPrive")] Melding melding)
        {
            if (ModelState.IsValid)
            {
                //dit zorgt ervoor dat de momenteele auteursnummer wordt opgeroepen
                if(!melding.IsPrive){
                int auteur = this.HttpContext.Session.GetInt32("IdGebruiker").Value;
                melding.Auteur = _context.Gebruikers.Where(g=>g.GebruikersNummer==auteur).First();
                }
                
                //dit zorgt ervoor dat er een uniek nummer wordt aangemaakt voor de meldingen
                melding.MeldingId = LaatstemeldingID;
                LaatstemeldingID++;

                melding.AangemaaktOp = DateTime.Now;
                melding.Reacties = MaakFakeReacties();
                melding.Likes=0;
                melding.Views=0;
                _context.Add(melding);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Meldingen));
            }
            return View(nameof(Meldingen));
        }
        
        public async Task<IActionResult> Delete(string titel)
        {
            if (titel == null)
            {
                return NotFound();
            }
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
            List<Gebruiker> gebruikers = _context.Gebruikers.ToList();
            var meldingen = _context.Meldingen;
            List<Melding> meldings = meldingen.ToList();

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