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
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Look.Controllers
{
    public class MeldingController : Controller
    {
        private readonly LookIdentityDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        //private readonly SignInManager<ApplicationUser> _singInManager;
        public static bool Success;
        public static bool Error;
        public static string Message;

        public MeldingController(LookIdentityDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            //_singInManager = signInManager;
            CheckErrors();
            //CheckMeldingenOpDatum();
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
        
        public JsonResult Like(long id){
            var CurrentSessionUserId = _userManager.GetUserId(User);
            ApplicationUser IngelogdeGebruiker = _context.Users.Where(p => p.Id == CurrentSessionUserId).FirstOrDefault();
            Melding _melding = _context.Meldingen.Where(p => p.MeldingID == id).FirstOrDefault();
            Liked _liked = _context.Likes.Where(p => p.MeldingID == id && p.UserId == IngelogdeGebruiker.Id).FirstOrDefault();
            Liked _newLiked = new Liked();
            _newLiked.UserId = IngelogdeGebruiker.Id;
            _newLiked.MeldingID = _melding.MeldingID;
                        
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
            return Json(new IntInfo { aantal =_context.Meldingen.Where(m=>m.MeldingID==id).First().Likes});
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
        public async Task<IActionResult> CreateMelding([Bind("Titel,Inhoud,Categorie,PriveCode")] Melding melding, IFormFile Afbeelding)
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

                if(melding.PriveCode != null)
                {
                    melding.IsPrive = true;
                }
                else
                {
                    melding.IsPrive = false;
                }

                if(melding.IsPrive)
                {
                    melding.Auteur = null;
                }
                else
                {
                    melding.Auteur = await _userManager.GetUserAsync(User);
                }
                

                melding.AangemaaktOp = DateTime.Now;
                melding.Likes=0;
                melding.Views=0;
                melding.IsActief = true;
                
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
        
        public IActionResult Rapporteren(long Id) {
            var Melding = _context.Meldingen.Where(M => M.MeldingID == Id).First();

            return View(Melding);   
        }

        [Authorize(Roles = "Member")]
        [HttpPost]
        public async Task<IActionResult> Rapporteren([Bind("Categorie")] MeldingRapport rapport, string GerapporteerdeMelding) {
            if(ModelState.IsValid)
            {
                var _user = await _userManager.GetUserAsync(User);
                var _userId = await _userManager.GetUserIdAsync(_user);
                var BestaandRapport = _context.MeldingRapporten.Where(r => r.GemaaktDoor.Id == _userId && r.GerapporteerdeMelding.MeldingID.ToString() == GerapporteerdeMelding).ToList();

                if(!BestaandRapport.Any()) {
                    var Melding = _context.Meldingen.Where(M => M.MeldingID.ToString() == GerapporteerdeMelding).First();

                    rapport.GerapporteerdeMelding = Melding;
                    rapport.GemaaktDoor = _user;
                    rapport.GeplaatstOp = DateTime.Now;
                    _context.MeldingRapporten.Add(rapport);
                    _context.SaveChanges();
                
                    return RedirectToAction("Meldingen");
                }
            }
            return RedirectToAction("Meldingen");
        }

        public async Task<IActionResult> Delete(long id)
        {
            
            var melding = await _context.Meldingen
                .FirstOrDefaultAsync(m => m.MeldingID == id);
            if (melding == null)
            {
                return View(melding);
            }
            var CurrentSessionUserId = _userManager.GetUserName(User);
            List<ApplicationUser> gebruikers = _context.Users.ToList();
            Melding _melding = _context.Meldingen.Where(p => p.MeldingID == id).FirstOrDefault();
            
            var Gebruiker = CurrentSessionUserId.ToString();
            if(Gebruiker==_melding.Auteur.ToString()){ 
                return View(melding);
            }else{
                return Redirect("~/Identity/Account/AccessDenied"); 
            }
        }


        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long MeldingID)
        {
            var melding = await _context.Meldingen.FindAsync(MeldingID);
            _context.Meldingen.Remove(melding);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Meldingen));
        }

        [Authorize(Roles = "Moderator")]
        public async Task<IActionResult> ModeratorDelete(long id)
        {
            var melding = await _context.Meldingen
                .FirstOrDefaultAsync(m => m.MeldingID == id);
            if (melding == null)
            {
                return NotFound();
            }
            return View(melding);
        }

        [Authorize(Roles = "Moderator")]
        [HttpPost, ActionName("ModeratorDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ModeratorDeleteConfirmed(long MeldingID)
        {
            var melding = await _context.Meldingen.FindAsync(MeldingID);
            _context.Meldingen.Remove(melding);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Meldingen));
        }

         public static List<Melding> query;

        //s is sorteren, z is zoeken

public IActionResult Meldingen(string s,string z,string zo, string c, string v, int page = 0)
        {
            if (query==null){
                query = new List<Melding>();
            }
            //dit zijn de de termen waarop het gesorteerd wordt
            ViewData["Sorteer"] = s ?? "Nieuwste";
            ViewData["Zoek"] = z ?? "";
            ViewData["Categorie"] = c ?? "Zoek op een categorie";
            ViewData["ZoekenOp"] = zo ?? "Zoeken op";
            ViewData["Volgorde"] = v ?? "Volgorde";

            //dit maakt een lijst aan waarop het gesorteerd wordt
            List<ApplicationUser> gebruikers = _context.Users.ToList();
            List<Melding> meldings = _context.Meldingen.ToList();
            var ReactiesOphalen = _context.Reacties.ToList();
            var meldingenOphalen = _context.Meldingen;
            var CurrentSessionUserId = _userManager.GetUserId(User);
            if(CurrentSessionUserId==null){ 
                ViewData["login"] ="Login";
                return Redirect("~/Identity/Account/Login");
            }
            
            //dit zorgt ervoor dat je kan sorteren als je op pagina 0 zit zonder dat je lijst weg gaat als je naar andere paginas gaat
            if(page==0){
                //dit zorgt ervoor dat je kan zoeken
                query = ZoekenOpFilter(z,zo,query);

                //dit zorgt ervoor dat je kan sorteren
                query = SorteerOpFiler(s,v,query);

                //dit zorgt ervoor dat je kan sorteren op een categorie
                query = SorteerOpCategorie(c,query);
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
        public List<Melding> ZoekenOpFilter(string z,string zo, List<Melding> Lijst){
            var meldingenOphalen = _context.Meldingen.ToList();
            if(z!=null){
                if(zo=="t"){
                    return _context.Meldingen.Where(m=>m.Titel.Contains(z)).ToList();
                }else if(zo=="i"){
                    return _context.Meldingen.Where(m=>m.Inhoud.Contains(z)).ToList();
                } else{
                    return _context.Meldingen.Where(m=>m.Titel.Contains(z)||m.Inhoud.Contains(z)).ToList();
                }
                }else{
                    return _context.Meldingen.ToList();
                }
        }
        public List<Melding> SorteerOpFiler(string s,string v,List<Melding> Lijst){
            var ReactiesOphalen = _context.Reacties.ToList();
            var meldingenOphalen = _context.Meldingen.ToList();

            if(s!=null){
                if(v=="Oplopend"){
                        if(s.Equals("Meeste likes")){
                            return Lijst.OrderBy(M=>M.Likes).ToList();
                        }else if(s.Equals("Meeste weergaven")){
                            return Lijst.OrderBy(M=>M.Views).ToList();
                        }else if(s.Equals("Naam")){
                            return Lijst.OrderBy(M=>M.Titel).ToList();
                        }else if(s.Equals("Nieuwste")){
                            return Lijst.OrderBy(M=>M.AangemaaktOp).ToList();
                        }else if(s.Equals("Gelikete Berichten")){
                            return sorteerGelikedeMeldingen(query);
                        }
                    }else{
                        if(s.Equals("Meeste likes")){
                            return Lijst.OrderByDescending(M=>M.Likes).ToList();
                        }else if(s.Equals("Meeste weergaven")){
                            return Lijst.OrderByDescending(M=>M.Views).ToList();
                        }else if(s.Equals("Naam")){
                            return Lijst.OrderByDescending(M=>M.Titel).ToList();
                        }else if(s.Equals("Nieuwste")){
                            return Lijst.OrderByDescending(M=>M.AangemaaktOp).ToList();
                        }else if(s.Equals("Gelikete Berichten")){
                            return sorteerGelikedeMeldingen(Lijst);
                        }
                    }
                }
                return Lijst;
        }

        public List<Melding> SorteerOpCategorie(string c, List<Melding> Lijst)
        {
            var meldingenOphalen = _context.Meldingen.ToList();
            var CurrentSessionUserId = _userManager.GetUserId(User);
            if (c != null)
            {
                if (c.Equals("Bijeenkomst"))
                {
                    return Lijst.Where(p => p.Categorie=="Bijeenkomst").ToList();
                } 
                if (c.Equals("Overlast"))
                {
                    return Lijst.Where(p => p.Categorie=="Overlast").ToList();
                } 
                if (c.Equals("Verdachte Activiteit"))
                {
                    return Lijst.Where(p => p.Categorie=="Verdachte activiteit").ToList();
                } 
                if (c.Equals("Verloren Voorwerp"))
                {
                    return Lijst.Where(p => p.Categorie=="Verloren voorwerp").ToList();
                } 
                if (c.Equals("Gevonden Voorwerp"))
                {
                    return Lijst.Where(p => p.Categorie=="Gevonden voorwerp").ToList();
                }
            } 
            return Lijst;
        }
        public List<Melding> sorteerGelikedeMeldingen(List<Melding> query){
                var CurrentSessionUserId = _userManager.GetUserId(User);
                var _liked = _context.Likes.Where(p=>p.UserId == CurrentSessionUserId).ToList().Select(m=>m.MeldingID);
                var _MeldingIDs = _context.Meldingen.ToList().Select(m=>m.MeldingID);

                var GelikteMeldingIDs = _MeldingIDs.Except(_liked).ToList();
                foreach(var berichten in GelikteMeldingIDs){
                query.Remove(query.Where(m=>m.MeldingID==berichten).FirstOrDefault());
             }
             return query;
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

        public IActionResult Edit(int id)
        { 
            List<string> titels = new List<string>();
            foreach(var m in _context.Meldingen){
                titels.Add(m.Titel);
            }
            ViewBag.Titels = titels;
            ViewBag.registerErrorText = "Deze titel van een bericht is al in gebruik.";
            var melding = _context.Meldingen.Where(m => m.MeldingID == id).FirstOrDefault();
            var CurrentSessionUserId = _userManager.GetUserName(User);
            List<ApplicationUser> gebruikers = _context.Users.ToList();
            Melding _melding = _context.Meldingen.Where(p => p.MeldingID == id).FirstOrDefault();
            
            var Gebruiker = CurrentSessionUserId.ToString();
            if(Gebruiker==_melding.Auteur.ToString()){ 
                return View(melding);
            }else{
                 return Redirect("~/Identity/Account/AccessDenied"); 
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Titel,Inhoud,Categorie,MeldingID")] Melding melding)
        {
            if (ModelState.IsValid)
            {

                Console.WriteLine(await _userManager.GetUserAsync(User));
                if(_context.Meldingen.Where(m=>m.MeldingID==melding.MeldingID).ToList().First().Auteur == await _userManager.GetUserAsync(User)){
                    //dit kijkt of de titel van de gebruiker al in gebruik is
                    var data = _context.Meldingen.Where(m=>m.Titel.Equals(melding.Titel)).ToList().Count;
                
                    //dit checkt of de titel hetzelfde is als ervoor
                    if(_context.Meldingen.Where(m=>m.MeldingID==melding.MeldingID).First().Titel==melding.Titel){
                        data = 0;
                    }

                    if(data == 0) {
                    _context.Meldingen.Where(m=>m.MeldingID==melding.MeldingID).ToList().First().Titel = melding.Titel;
                    _context.Meldingen.Where(m=>m.MeldingID==melding.MeldingID).ToList().First().Inhoud = melding.Inhoud;
                    _context.Meldingen.Where(m=>m.MeldingID==melding.MeldingID).ToList().First().Categorie = melding.Categorie;
                    await _context.SaveChangesAsync();

                    //dit zorgt ervoor dat er een melding wordt weergegeven als het gewijzigd wordt
                    Success = true;
                    Message = "Bericht is successvol gewijzigd!";
                    }
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
            _context.Meldingen.Where(m=>m.MeldingID==id).First().Views+=1;
            _context.SaveChanges();
            return Json(new IntInfo { aantal =_context.Meldingen.Where(m=>m.MeldingID==id).First().Views});
        }

        public IActionResult PostComment(int? id, string inhoud){
            ViewBag.Comments = _context.Meldingen.Where(m=>m.MeldingID==id).FirstOrDefault();
            Reactie reactie = new Reactie();
            string auteur = _userManager.GetUserId(User);
            reactie.GeplaatstDoor = _context.Users.Where(g => g.Id == auteur).First();
            reactie.GeplaatstOp = DateTime.Now;
            reactie.Bericht=inhoud;
            List<Reactie> reacties;
            try{
                _context.Reacties.Add(reactie);
                 try{
                        reacties =_context.Meldingen.Where(m=>m.MeldingID==id).First().Reacties;
                        reacties.Add(reactie);
                 }catch{ //dit maakt een nieuwe list aan als deze er niet automatisch inzit
                        reacties = new List<Reactie>();
                        reacties.Add(reactie);
                        _context.Meldingen.Where(m=>m.MeldingID==id).First().Reacties=reacties;
                 }
                Console.WriteLine(reacties);
            }catch{
                Console.WriteLine("error"); 
            }
            Console.WriteLine("Comment geplaatst");
             _context.SaveChanges();

            Success = true;
            Message = "Uw reactie is geplaatst!";
            return Redirect("~/Melding/Meldingen#melding-"+id); 
        }
    }
}