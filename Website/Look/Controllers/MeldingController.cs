using System;
using System.IO;
using System.Drawing;
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
using Microsoft.AspNetCore.Authorization;

namespace Look.Controllers
{
    public class MeldingController : Controller
    {
        private readonly ILogger<MeldingController> _logger;
        private readonly LookIdentityDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _singInManager;
        public static bool Success;
        public static bool Error;
        public static string Message;

        public MeldingController(ILogger<MeldingController> logger, LookIdentityDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
            _singInManager = signInManager;
            CheckErrors();
            CheckMeldingenOpDatum();
        }
    
         public void CheckErrors(){
             if(Message==null){
                Success = false;
                Error = false;
                Message = "Error";
             }
        }

        public class IntInfo
        {
            public int aantal{get;set;}
        }
        public class StringInfo
        {
            public string bericht{get;set;}
        }

        // public List<Reactie> MaakFakeReacties(){
        //     List<Reactie> reacties = new List<Reactie>();
        //     Reactie reactie = new Reactie();
        //     reactie.ReactieId = UniekMeldingID;
        //     UniekMeldingID++;
        //     reactie.GeplaatstDoor = _context.Users.Where( g=> g.Id==3).First();
        //     reactie.GeplaatstOp = DateTime.Now;
        //     reactie.Bericht ="Een reactie";
        //     reactie.Likes = 0;
        //     reacties.Add(reactie);
        //     return reacties;
        // }
        
        public JsonResult Like(long id){
            var CurrentSessionUserId = _userManager.GetUserId(User);
            ApplicationUser IngelogdeGebruiker = _context.Users.Where(p => p.Id == CurrentSessionUserId).FirstOrDefault();
            Melding _melding = _context.Meldingen.Where(p => p.MeldingId == id).FirstOrDefault();
            Liked _liked = _context.Likes.Where(p => p.MeldingId == id && p.UserId == IngelogdeGebruiker.Id).FirstOrDefault();
            Liked _newLiked = new Liked();
            _newLiked.UserId = IngelogdeGebruiker.Id;
            _newLiked.MeldingId = _melding.MeldingId;
                        
            var CurrentSession = _userManager.GetUserId(User);
            var DeveloperSession = "Developer";

            if (CurrentSession != null || DeveloperSession != null)
            {
                if (_liked == null)
                {
                    _melding.Likes++;
                    _context.Likes.Add(_newLiked);
                    _context.SaveChanges();
                } else {
                    _melding.Likes--;
                    _context.Likes.Remove(_liked);
                    _context.SaveChanges();
                }
            } 
            return Json(new IntInfo { aantal =_context.Meldingen.Where(m=>m.MeldingId==id).First().Likes});
        }

        public IActionResult PlaatsBericht()
        {
            try{
            string isNull = _userManager.GetUserId(User);
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
                string auteur = _userManager.GetUserId(User);
                reactie.GeplaatstDoor = _context.Users.Where(g => g.Id == auteur).First();
                var inladen = _context.Meldingen.Where(m=>m.MeldingId==15).First().Reacties.First().ReactieId;

            }
            return View(nameof(Meldingen));
            // return Json(new IntInfo { aantal =_context.Meldingen.Where(m=>m.MeldingId==15).First().Likes});
        }
        
        public IActionResult CreateMelding()
        {
            try{
            string isNull = _userManager.GetUserId(User);
            List<string> titels = new List<string>();
            foreach(var m in _context.Meldingen){
                titels.Add(m.Titel);
            }
            ViewBag.Titels = titels;
            ViewBag.registerErrorText = "Deze titel van een bericht is al in gebruik.";
            return View();
            }catch
            {
                return RedirectToAction(nameof(Meldingen));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateMelding([Bind("Titel,Inhoud,Categorie,IsPrive")] Melding melding, IFormFile Afbeelding)
        {
            if (ModelState.IsValid)
            {
                //dit kijkt of de titel van de gebruiker al in gebruik is
                var data = _context.Meldingen.Where(m=>m.Titel.Equals(melding.Titel)).ToList();
                Console.WriteLine(data.Count+"Dit is de datacount");
                if(data.Count == 0) {
                Success = true;
                Error = false;
                Message = "Het Bericht is successvol geplaatst";
                try{
                melding.AfbeeldingTitel = Path.GetFileName(Afbeelding.FileName);
                
                MemoryStream ms = new MemoryStream();
                Afbeelding.CopyTo(ms);
                melding.AfbeeldingData = ms.ToArray();

                ms.Close();
                ms.Dispose();
                }catch{}

                //dit zorgt ervoor dat de momenteele auteursnummer wordt opgeroepen
                if(!melding.IsPrive){
                    var auteur = _userManager.GetUserId(User);
                    melding.Auteur = _context.Users.FirstOrDefault(g => g.Id == auteur);
                }
                

                melding.AangemaaktOp = DateTime.Now;
                // melding.Reacties = MaakFakeReacties();
                melding.Likes=0;
                melding.Views=0;
                _context.Add(melding);
                await _context.SaveChangesAsync();
            }
            else
            {
                //Toon de error message in de HTML
                ViewBag.CreateError = true;
                ViewBag.editSuccess = false;
                ViewBag.editErrorText = "Het bericht is niet geplaats.<br> Er was een bericht met dezelfde titel";
                Console.WriteLine("Het bericht is niet geplaatst");
                return RedirectToAction(nameof(CreateMelding));
            }
                return RedirectToAction(nameof(Meldingen));
                }
                return RedirectToAction(nameof(Meldingen));
        }
        
        public async Task<IActionResult> Delete(long id)
        {
            var melding = await _context.Meldingen
                .FirstOrDefaultAsync(m => m.MeldingId == id);
            if (melding == null)
            {
                return View(melding);
            }
            var CurrentSessionUserId = _userManager.GetUserName(User);
            List<ApplicationUser> gebruikers = _context.Users.ToList();
            Melding _melding = _context.Meldingen.Where(p => p.MeldingId == id).FirstOrDefault();
            
            var Gebruiker = CurrentSessionUserId.ToString();
            if(Gebruiker==_melding.Auteur.ToString()){ 
                return View(melding);
            }else{
                return RedirectToAction(nameof(Meldingen));
            }
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

        [Authorize(Roles = "Moderator")]
        public async Task<IActionResult> ModeratorDelete(long id)
        {
            var melding = await _context.Meldingen
                .FirstOrDefaultAsync(m => m.MeldingId == id);
            if (melding == null)
            {
                return NotFound();
            }
            return View(melding);
        }

        [Authorize(Roles = "Moderator")]
        [HttpPost, ActionName("ModeratorDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ModeratorDeleteConfirmed(long MeldingId)
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
            List<ApplicationUser> gebruikers = _context.Users.ToList();
            var meldingen = _context.Meldingen;
            List<Melding> meldings = meldingen.ToList();
            try{
            string auteur = _userManager.GetUserId(User);
            ViewBag.Gebruiker = _context.Users.Where(g => g.Id==auteur).First().Id;
            }catch{
                meldings = meldingen.Where(m=>m.Auteur!=null).ToList();
            }
            
            //dit zorgt ervoor dat je kan sorteren als je op pagina 0 zit zonder dat je lijst weg gaat als je naar andere pagias gaat
            if(page==0){
                //dit zorgt ervoor dat je kan zoeken
                if(z!=null){
                    query = meldings.Where(m=>m.Titel.Contains(z)||m.Inhoud.Contains(z)).ToList();
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
            const int pageSize = 10;
            var count = query.Count();
            var data = query.Skip(page * pageSize).Take(pageSize).ToList();
            this.ViewBag.MaxPage = (count / pageSize) - (count % pageSize == 0 ? 1 : 0);
            this.ViewBag.Page = page;
           
            ViewBag.Success = Success;
            ViewBag.Error = Error;
            ViewBag.Message = Message;
            
            Success = false;
            Error = false;
            Message = "Error";

            //Toon de views mits de gebruiker is ingelogd.
            if(User.Identity.IsAuthenticated)
            {
                return View(data);
            }

            return View(data);
        }


        [HttpPost]
        public async void CheckMeldingenOpDatum(){
            //hier wordt de verloopdatum gemaakt
            DateTime VerloopDatum = DateTime.Now;
            VerloopDatum = VerloopDatum.AddDays(-30);

            foreach (var melding in _context.Meldingen.Where(m=>m.AangemaaktOp<VerloopDatum))
            {
                _context.Meldingen.Remove(melding);
            }
            await _context.SaveChangesAsync();
        }

        public ActionResult Edit(int id)
        { 
            var CurrentSessionUserId = _userManager.GetUserName(User);
            List<ApplicationUser> gebruikers = _context.Users.ToList();
            Melding _melding = _context.Meldingen.Where(p => p.MeldingId == id).FirstOrDefault();
            
            ViewBag.Gebruiker = CurrentSessionUserId.ToString();
            ViewBag.Auteur = _melding.Auteur.ToString();      
             
            List<string> titels = new List<string>();
            foreach(var m in _context.Meldingen){
                titels.Add(m.Titel);
            }
            ViewBag.Titels = titels;
            ViewBag.registerErrorText = "Deze titel van een bericht is al in gebruik.";
             var melding = _context.Meldingen.Where(m => m.MeldingId == id).FirstOrDefault();
            return View(melding);
        }
       [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Titel,Inhoud,Categorie,MeldingId")] Melding melding)
        {
            if (ModelState.IsValid)
            {
                //dit kijkt of de titel van de gebruiker al in gebruik is
                var data = _context.Meldingen.Where(m=>m.Titel.Equals(melding.Titel)).ToList().Count;
                
                //dit checkt of de titel hetzelfde is als ervoor
                if(_context.Meldingen.Where(m=>m.MeldingId==melding.MeldingId).First().Titel==melding.Titel){
                    data = 0;
                }

                if(data == 0) {
                _context.Meldingen.Where(m=>m.MeldingId==melding.MeldingId).ToList().First().Titel = melding.Titel;
                 _context.Meldingen.Where(m=>m.MeldingId==melding.MeldingId).ToList().First().Inhoud = melding.Inhoud;
                _context.Meldingen.Where(m=>m.MeldingId==melding.MeldingId).ToList().First().Categorie = melding.Categorie;
                await _context.SaveChangesAsync();

                //dit zorgt ervoor dat er een melding wordt weergegeven als het gewijzigd wordt
                Success = true;
                Message = "Bericht is successvol gewijzigd!";

            }
            else
            {
                //Toon de error message in de HTML
                Error = true;
                Message = "Het bericht is niet geplaatst.";
                Console.WriteLine("Het bericht is niet geplaatst");
                return RedirectToAction(nameof(Edit));
            }
                return RedirectToAction(nameof(Meldingen));
                }
                return RedirectToAction((nameof(Edit)));
        }
        public JsonResult AddView(int? id)
        {
            _context.Meldingen.Where(m=>m.MeldingId==id).First().Views+=1;
            _context.SaveChanges();
            return Json(new IntInfo { aantal =_context.Meldingen.Where(m=>m.MeldingId==id).First().Views});
        }

        public JsonResult PostComment(int? id, string inhoud){
            Reactie reactie = new Reactie();
            string auteur = _userManager.GetUserId(User);
            reactie.GeplaatstDoor = _context.Users.Where(g => g.Id == auteur).First();
            reactie.GeplaatstOp = DateTime.Now;
            reactie.Bericht=inhoud;
            List<Reactie> reacties;
            try{
                _context.Reacties.Add(reactie);
                 Console.WriteLine("Comment Aan database toegevoegd ");
                 try{
                     
                        reacties =_context.Meldingen.Where(m=>m.MeldingId==id).First().Reacties;
                        reacties.Add(reactie);
                 }catch{ //dit maakt een nieuwe list aan als deze er niet automatisch inzit
                        reacties = new List<Reactie>();
                        reacties.Add(reactie);
                        _context.Meldingen.Where(m=>m.MeldingId==id).First().Reacties=reacties;
                 }
                Console.WriteLine(reacties);
            }catch{
                Console.WriteLine("error"); 
            }
            Console.WriteLine("Comment geplaatst");
             _context.SaveChanges();
            return Json(new StringInfo { bericht =inhoud});
        }
    }
}