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
        public ApplicationUser GemaaktDoor {get; set;}
        public DateTime GeplaatstOp {get; set;}
        public string Categorie {get; set;}
    }

    public class MeldingRapport : Rapport
    {
        public Melding GerapporteerdeMelding {get; set;}
    }

    public class ReactieRapport : Rapport
    {
        public Melding GerapporteerdeReactie {get; set;}
    }
}
