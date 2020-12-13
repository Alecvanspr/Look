using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Look.Models
{
    public class Gebruiker
    {
        [Key]
        public int GebruikersNummer {get; set;}
        [Required]
        public string VoorNaam {get; set;}
        [Required]
        public string AchterNaam {get; set;}
        [Required]
        public string GebruikersNaam {get; set;}
        [Required]
        public string Straat { get; set; }
        [Required]
        public string HuisNummer {get; set;}
        [Required]
        public string Woonplaats { get; set; }
        [Required]
        public string PostCode { get; set; }
        [Required]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}")]
        public string EmailAdres {get; set;}
        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$")]
        public string WachtWoord {get; set;}
        [NotMapped]
        [Required]
        [Compare("WachtWoord")]
        public string VerifieerWachtWoord { get; set; }
        public bool IsGeverifieerd {get; set;}
        public bool IsAnoniem {get; set;}

        public string Naam() 
        {
            return this.VoorNaam + this.AchterNaam;
        }

        public string Adres()
        {
            return this.Straat + this.HuisNummer;
        }
    }
}
