﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Look.Models
{
    public class Gebruiker
    {
        public int GebruikersNummer {get; set;}
        public string VoorNaam {get; set;}
        public string AchterNaam {get; set;}
        public string GebruikersNaam {get; set;}
        public string Adres {get; set;}
        public string EmailAdres {get; set;}
        public string WachtWoord {get; set;}
        public bool IsGeverifieerd {get; set;}
        public bool IsAnoniem {get; set;}
    }
}
