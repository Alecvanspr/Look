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
        public long MeldingId {get; set;}
        public bool IsPrive { get; set; }
        public DateTime AangemaaktOp { get; set; }
        public string Titel { get; set; }
        public string Inhoud { get; set; }
        public string AfbeeldingTitel { get; set; }
        public byte[] AfbeeldingData { get; set; }
        public int Likes { get; set; }
        public int Views { get; set; }
        public bool IsActief { get; set; }
        public Nullable<Guid> PriveCode { get; set; }
        public ApplicationUser Auteur { get; set; }
        public List<Reactie> Reacties { get; set; }
        public string Categorie { get; set; }
    }
}
