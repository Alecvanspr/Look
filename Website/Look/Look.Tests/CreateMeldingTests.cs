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
    
    public class CreateMeldingTest
    {
        private string databaseName;

        public LookIdentityDbContext getContext(){
            LookIdentityDbContext context = getNewContext(true);

            context.Meldingen.Add(new Melding {Titel="Ik eet pizza",Inhoud="ik heb lekker een pizza gegeten",Likes=3,Views=5,Categorie="pizzas"});          
            context.Meldingen.Add(new Melding {Titel="Ik bak taarten",Inhoud="ik heb goede taart recepten",Likes=5,Views=156,Categorie="Taarten"}); 
            context.Meldingen.Add(new Melding {Titel="Ik heb een jas gevonden",Inhoud="Er lag een gele jas in het speeltuintje",Likes=76,Views=78,Categorie="Gevonden Voorwerp"}); 
            context.Meldingen.Add(new Melding {Titel="Testen Schrijven",Inhoud="Poeh hey wat was ik hier lang mee bezig",Likes=0,Views=1,Categorie="Testen"}); 
            
            context.Users.Add(new ApplicationUser { FirstName = "Bastiaan", LastName = "van Toor", UserName="Bassie",Email="Grappenmaker@gmail.com"});
            context.Users.Add(new ApplicationUser { FirstName = "Adriaan" ,LastName = "van Toor" , UserName="Adriaan",Email="Acrobaatje"});
            
            context.SaveChanges();

            return getNewContext(false);
        }

        private LookIdentityDbContext getNewContext(bool NewDb){
            if(NewDb) this.databaseName = "MockDatabase";
            
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


        [Theory]
        [InlineData("Ik eet pizza",1)]
        [InlineData("Ik bak taarten",2)]
        [InlineData("Ik heb een jas gevonden",3)]
        [InlineData("Testen Schrijven",4)]
        public void MockDataBaseTest(string expected,long id){
            var context = getContext();
            var output = context.Meldingen.Where(m=>m.MeldingId==id).FirstOrDefault().Titel;
            Assert.True(output==expected);
        }
        [Theory]
        [InlineData("TestMelding1","TestMelding1")]
        [InlineData("TestMelding2","TestMelding2")]
        public void testCreateMelding(string expected,string titel){
            MeldingController meldingController = new MeldingController(getContext(),GetUserManager());
            Melding melding = new Melding{Titel=titel,Inhoud="dit is een test demo", Categorie="Een leuke catagorie", IsPrive=false};
            meldingController.CreateMelding(melding,null);
            var meldingen = getContext(); 
            Assert.True(meldingen.Meldingen.Where(m=>m.Titel==expected).FirstOrDefault().Titel==expected);
        }
        [Theory]
        [InlineData("Demobericht","TestMelding1","Demobericht")]
        [InlineData("Demobericht2","TestMelding2","Demobericht2")]
        public void testEditMelding(string expected,string titelMelding1,string nieuweTitel){
            MeldingController meldingController = new MeldingController(getContext(),GetUserManager());
            Melding CreatedMelding = new Melding{Titel=titelMelding1,Inhoud="dit is een test demo", Categorie="Een leuke catagorie", IsPrive=false};
            meldingController.CreateMelding(CreatedMelding,null);
            var meldingen = getContext(); 
            Melding editedMelding = CreatedMelding;
            editedMelding.Titel = nieuweTitel;
            meldingController.Edit(editedMelding);
            Assert.True(meldingen.Meldingen.Where(m=>m.Titel==expected).FirstOrDefault().Titel==expected);
        }
        

    }
}
