using System;
using Xunit;
using Look.Models;
using Look.Controllers;
using Look.Areas.Identity.Data;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Linq;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.Abstractions;
using Microsoft.Extensions.Logging.Abstractions;
using Autofac.Extras.Moq;


namespace Look.Tests
{
    
    public class UnitTest1
    {
        private string databaseName;


        public LookIdentityDbContext getContext(){
            LookIdentityDbContext context = getNewContext(true);

            context.Meldingen.Add(new Melding {Titel="Ik eet pizza",Inhoud="ik heb lekker een pizza gegeten",Likes=3,Views=5,Categorie="pizzas"});
            context.Meldingen.Add(new Melding {Titel="Ik bak taarten",Inhoud="ik heb goede taart recepten",Likes=5,Views=24,Categorie="Taarten"}); 
            context.Meldingen.Add(new Melding {Titel="Ik heb een jas gevonden",Inhoud="Er lag een gele jas in het speeltuintje",Likes=76,Views=145,Categorie="Gevonden Voorwerp"}); 
            context.Meldingen.Add(new Melding {Titel="Testen Schrijven",Inhoud="Poeh hey wat was ik hier lang mee bezig",Likes=0,Views=1,Categorie="Testen"}); 
           

            context.Users.Add(new ApplicationUser { FirstName = "Bastiaan", LastName = "van Toor", UserName="Bassie",Email="Grappenmaker@gmail.com"});
            context.Users.Add(new ApplicationUser { FirstName = "Adriaan" ,LastName = "van Toor" , UserName="Adriaan",Email="Acrobaatje"});
            

            var liked1 = new Liked();
            liked1.UserId= context.Users.FirstOrDefault().Id;
            liked1.MeldingId = context.Meldingen.FirstOrDefault().MeldingId;
            var liked2 = new Liked();
            liked2.UserId= context.Users.FirstOrDefault().Id;
            liked2.MeldingId = context.Meldingen.Where(m=>m.Titel=="Testen Schrijven").FirstOrDefault().MeldingId;
            var liked3 = new Liked();
            liked3.UserId= context.Users.Where(u=>u.UserName=="Bassie").FirstOrDefault().Id;
            liked3.MeldingId = context.Meldingen.FirstOrDefault().MeldingId;
            
            context.SaveChanges();
            return getNewContext(false);
        }

        private LookIdentityDbContext getNewContext(bool NewDb){
            if(NewDb) this.databaseName = Guid.NewGuid().ToString();
            
            var options = new DbContextOptionsBuilder<LookIdentityDbContext>()
            .UseInMemoryDatabase(databaseName)
            .Options;
            
            return new LookIdentityDbContext(options);
        }


        private UserManager<ApplicationUser> GetUserManager(){
            var UserStore = new Mock<IUserStore<ApplicationUser>>();
            var UserManager = new Mock<UserManager<ApplicationUser>>(UserStore.Object,null,null,null,null,null,null,null,null);
            return UserManager.Object;
        }


        public MeldingController createMeldingController(){
            var context = getContext();
            var usermanager = GetUserManager();
            return new MeldingController(context,usermanager);
        }
        

    }
}
