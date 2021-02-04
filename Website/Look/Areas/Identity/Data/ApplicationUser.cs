using System;
using System.Collections.Generic;
using Look.Models;
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
        public override string UserName { get; set; }
        [PersonalData]
        public string FirstName { get; set; }
        [PersonalData]
        public string LastName { get; set; }
        [PersonalData]
        public string Street { get; set; }
        [PersonalData]
        public string HouseNumber {get; set;}
        [PersonalData]
        public string HouseNumberAddition {get; set;}
        [PersonalData]
        public string City { get; set; }
        [PersonalData]
        public string ZipCode { get; set; }
        [PersonalData]
        public bool IsAnonymous {get; set;}
        [PersonalData]
        public Guid PriveCode { get; set; }
        public Nullable<DateTime> LastLogin { get; set; }
        
        public ICollection<ReactieRapport> ReactieRapporten {get; set;}
        public ICollection<MeldingRapport> MeldingRapporten {get; set;}

        public string FullName()
        {
            return FirstName + " " + LastName;
        }

        public string FullHouseNumber()
        {
            if(HouseNumberAddition == null)
            {
                return HouseNumber;
            }
            else
            {
                return HouseNumber + "-" + HouseNumberAddition;
            }
            
        }

        public string FullAddress()
        {
            return Street + " " + FullHouseNumber() + ", " + ZipCode + " " + City;
        }

    }
}