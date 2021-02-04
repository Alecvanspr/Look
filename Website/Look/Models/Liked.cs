using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Look.Areas.Identity.Data;

namespace Look.Models
{
    public class Liked
    {
        [Key]
        public string UserId { get; set; }
        [NotMapped]
        public ApplicationUser Gebruiker { get; set; }
        [Key]
        public long MeldingId { get; set; }
        [NotMapped]
        public Melding Melding { get; set; }
    }
}