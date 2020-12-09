using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Look.Models
{
    public class Melding
    {
        [Key]
        public string MeldingId {get; set;}
        public bool IsPrive { get; set; }
        public DateTime AangemaaktOp { get; set; }
        public string Titel { get; set; }
        public string Inhoud {get; set;}
        public int Likes { get; set; }
        public int Views { get; set; }
        public Gebruiker Auteur {get;set;}
        public List<Reactie> Reacties { get; set; }
        public List<string> Categorieen { get; set; }
    }
}
