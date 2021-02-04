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
    public class ModeratorController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly LookIdentityDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ModeratorController(ILogger<AdminController> logger, LookIdentityDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
        }

        public async Task<List<MeldingRapport>> GetMeldingRapportenAsync()
        {
            List<MeldingRapport> meldingRapporten = new List<MeldingRapport>();
            var alleMeldingRapporten = await _context.MeldingRapporten.ToListAsync();

            if(alleMeldingRapporten?.Any() == true)
            {
                foreach(var melding in alleMeldingRapporten)
                {
                    var Rapporteur = _context.Users.Where(u => u.Id == melding.GemaaktDoor.Id).First();
                    Console.WriteLine(Rapporteur.FullName());

                    meldingRapporten.Add(new MeldingRapport() {
                        RapportId = melding.RapportId,
                        // GemaaktDoor = melding.GemaaktDoor.FullName(),
                        // MeldingTitel = melding.GerapporteerdeMelding.Titel,
                        // MeldingID = melding.GerapporteerdeMelding.MeldingId,
                        // MeldingAuteur = melding.GerapporteerdeMelding.Auteur.FullName(),
                        // MeldingAuteurID = melding.GerapporteerdeMelding.Auteur.Id,
                        GeplaatstOp = melding.GeplaatstOp,
                        Categorie = melding.Categorie
                    });
                }
            }

            return meldingRapporten;
        }

        public async Task<List<ReactieRapport>> GetReactieRapportenAsync()
        {
            List<ReactieRapport> reactieRapporten = new List<ReactieRapport>();
            var alleReactieRapporten = await _context.ReactieRapporten.ToListAsync();

            if(alleReactieRapporten?.Any() == true)
            {

                foreach(var reactie in alleReactieRapporten)
                {
                    reactieRapporten.Add(new ReactieRapport() {
                        RapportId = reactie.RapportId,
                        // GemaaktDoor = reactie.GemaaktDoor.FullName(),
                        // ReactieAuteur = reactie.GerapporteerdeReactie.GeplaatstDoor.FullName(),
                        // ReactieAuteurID = reactie.GerapporteerdeReactie.GeplaatstDoor.Id,
                        // Reactie = reactie.GerapporteerdeReactie.Bericht,
                        // ReactieID = reactie.GerapporteerdeReactie.ReactieId,
                        GeplaatstOp = reactie.GeplaatstOp,
                        Categorie = reactie.Categorie
                    });
                }
            }

            return reactieRapporten;
        }

        public async Task<IActionResult> Rapporten() 
        {
            RapportViewModel rapportViewModel = new RapportViewModel();
            rapportViewModel.meldingRapporten = await GetMeldingRapportenAsync();
            rapportViewModel.reactieRapporten = await GetReactieRapportenAsync();

            return View(rapportViewModel);
        }
    }
}