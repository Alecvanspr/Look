using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Look.Areas.Identity.Data
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<ApplicationUserClaim> Claims { get; set; }
        public virtual ICollection<ApplicationUserLogin> Logins { get; set; }
        public virtual ICollection<ApplicationUserToken> Tokens { get; set; }
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
        
        [PersonalData]
        public string FirstName {get; set;}
        [PersonalData]
        public string LastName {get; set;}
        [PersonalData]
        public string Street { get; set; }
        [PersonalData]
        public string HouseNumber {get; set;}
        [PersonalData]
        public string City { get; set; }
        [PersonalData]
        public string ZipCode { get; set; }
        [PersonalData]
        public bool IsAnonymous {get; set;}

        public string FullName()
        {
            return FirstName + " " + LastName;
        }

    }
}