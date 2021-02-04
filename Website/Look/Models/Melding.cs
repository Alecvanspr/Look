using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Look.Areas.Identity.Data;

namespace Look.Models
{
    public class Melding
    {
        [Key]
        public long MeldingID {get; set;}
        public string ApplicationUserID {get; set;}
        public ApplicationUser Auteur { get; set; }
        public bool IsPrive { get; set; }
        public DateTime AangemaaktOp { get; set; }
        public string Titel { get; set; }
        public string Inhoud { get; set; }
        public string AfbeeldingTitel { get; set; }
        public byte[] AfbeeldingData { get; set; }
        public int Likes { get; set; }
        public int Views { get; set; }
        public bool IsActief { get; set; }
        public string Categorie { get; set; }


        public ICollection<Reactie> Reacties {get; set;}
        public ICollection<MeldingRapport> Rapporten {get; set;}
        public ICollection<Liked> Likeds {get; set;}

    }
}
