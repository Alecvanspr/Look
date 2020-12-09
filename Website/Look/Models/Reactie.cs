using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Look.Models
{
    public class Reactie
    {
        public Gebruiker GeplaatstDoor {get; set;}
        public DateTime GeplaatstOp {get; set;}
        public string Bericht {get; set;}
    }
}
