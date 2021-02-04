using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Look.Areas.Identity.Data;
using Look.Models;

namespace Look.Models
{
    public class Rapport
    {
        [Key]
        public long RapportId {get; set;}
        public string ApplicationUserID {get; set;}
        public ApplicationUser GemaaktDoor {get; set;}
        public DateTime GeplaatstOp {get; set;}
        public string Categorie {get; set;}
    }

    public class MeldingRapport : Rapport
    {
        public long MeldingID {get; set;}
        public Melding GerapporteerdeMelding {get; set;}
    }

    public class ReactieRapport : Rapport
    {
        public long ReactieID {get; set;}
        public Reactie GerapporteerdeReactie {get; set;}
    }

    public class RapportViewModel {
        public List<MeldingRapport> meldingRapporten {get; set;}
        public List<ReactieRapport> reactieRapporten {get; set;}
    }
}
