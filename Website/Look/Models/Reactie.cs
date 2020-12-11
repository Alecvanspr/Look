using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Look.Models
{
    public class Reactie
    {
        public int ReactieId {get; set;}
        public Gebruiker GeplaatstDoor {get; set;}
        public DateTime GeplaatstOp {get; set;}
        public string Bericht {get; set;}
        public int likes{get;set;}
    }
}
