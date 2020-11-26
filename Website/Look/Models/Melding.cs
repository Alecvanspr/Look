using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Look.Models
{
    public class Melding
    {
        public bool IsPrive {get; set;}
        public DateTime AangemaaktOp {get; set;}
        public string Titel {get; set;}
        public int Likes {get; set;}
        public int Views {get; set;}
        public List<Reactie> Reacties {get; set;}
        public List<string> Categorieen {get; set;}
    }
}