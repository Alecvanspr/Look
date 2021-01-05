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
        public async Task<IActionResult> CreateMelding([Bind("Titel,Inhoud,Categorie")] Melding melding)
        {
            if (ModelState.IsValid)
            {
                int autheur = this.HttpContext.Session.GetInt32("IdGebruiker").Value;
                Console.WriteLine("de autheur is"+autheur);
                melding.Auteur = _context.Gebruikers.Where(g=>g.GebruikersNummer==autheur).First();//dit werkt 
                Console.WriteLine("de autheur is"+  melding.Auteur.VoorNaam);
                LaatstemeldingID++;
                melding.MeldingId = LaatstemeldingID;
                melding.AangemaaktOp = DateTime.Now;
                melding.Likes = 0;
                melding.Views=0;
                _context.Add(melding);
                await _context.SaveChangesAsync();
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
            var meldingen = _context.Meldingen;
            List<Melding> meldings = meldingen.ToList();
            //Check of er een gebruiker is ingelogd.
            var CurrentSession = this.HttpContext.Session.GetString("Naam");
            var DeveloperSession = "Developer";
            LaatstemeldingID = meldings.Count();
            
            if(page==0){
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
            }
            const int pageSize = 3;
            var count = this._context.Meldingen.Count();
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