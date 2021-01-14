using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Look.Models
{
    public class Liked
    {
        [Key]
        public long LikedId { get; set; }

        public int GebruikersNummer { get; set; }
        [NotMapped]
        public Gebruiker Gebruiker { get; set; }

        public long MeldingId { get; set; }
        [NotMapped]
        public Melding Melding { get; set; }
        public bool heeftGeliked { get; set; }
    }
}