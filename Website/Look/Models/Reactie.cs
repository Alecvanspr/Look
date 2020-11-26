using System;
using System.ComponentModel.DataAnnotations;

namespace Look.Models
{
    public class Reactie
    {
        public Gebruiker GeplaatstDoor {get; set;}
        public DateTime GeplaatstOp {get; set;}
        public string Bericht {get; set;}
    }
}