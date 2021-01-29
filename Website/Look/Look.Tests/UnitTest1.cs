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

namespace Look.Tests
{
    
    public class UnitTest1
    {
        private string databaseName;

        //deze methode vult de database aan
        public LookIdentityDbContext getContext(){
            LookIdentityDbContext context = getNewContext(true);
            context.Meldingen.Add(new Melding {Titel="Ik eet pizza",Inhoud="ik heb lekker een pizza gegeten",Likes=3,Views=5,Categorie="pizzas"});
            context.Meldingen.Add(new Melding {Titel="Ik bak taarten",Inhoud="ik heb goede taart recepten",Likes=5,Views=24,Categorie="Taarten"}); 
            context.Meldingen.Add(new Melding {Titel="Ik heb een jas gevonden",Inhoud="Er lag een gele jas in het speeltuintje",Likes=76,Views=145,Categorie="Gevonden Voorwerp"}); 
            context.Meldingen.Add(new Melding {Titel="Testen Schrijven",Inhoud="Poeh hey wat was ik hier lang mee bezig",Likes=0,Views=1,Categorie="Testen"}); 
            context.SaveChanges();
            return getNewContext(false);
        }
        //deze methode maakt een lege database
        private LookIdentityDbContext getNewContext(bool NewDb){
            if(NewDb) this.databaseName = Guid.NewGuid().ToString();
            
            var options = new DbContextOptionsBuilder<LookIdentityDbContext>()
            .UseInMemoryDatabase(databaseName)
            .Options;
            
            return new LookIdentityDbContext(options);
        }
        //dit maakt een user manager aan.
        private UserManager<ApplicationUser> GetUserManager(){
            var UserStore = new Mock<IUserStore<ApplicationUser>>();
            var UserManager = new Mock<UserManager<ApplicationUser>>(UserStore.Object,null,null,null,null,null,null,null,null);
            return UserManager.Object;
        }
        //deze methode maakt een MeldingCreator aan dit zorgt ervoor dat er minder code duplication is.
        public MeldingController createMeldingController(){
            var context = getContext();
            var usermanager = GetUserManager();
            return new MeldingController(context,usermanager);
        }

        //deze eerste test is meer om te testen of we in de MeldingController konden komen.
        [Fact]
        public void IndexTest(){
            var MeldingController = createMeldingController();
            Assert.IsType<ViewResult>(MeldingController.bekijk());
        }
        //in de onderstaande test hebben wij gest of het sorteren en het filteren werkt.

        [Fact] //Deze checkt of de SorteerOpFilter de volledige lijst terug geeft.
        public void SorteerOpFilterTest1(){
            var context = getContext();
            var usermanager = GetUserManager();
            var MeldingController = new MeldingController(context,usermanager);
            
            var Meldingen = new List<Melding>();
               Meldingen.Add(new Melding {Titel="bericht1",Inhoud="inhoud1",Views=20});
               Meldingen.Add( new Melding {Titel="bericht2",Inhoud="inhoud2",Views=10});
               Meldingen.Add( new Melding {Titel="bericht3",Inhoud="inhoud3",Views=30});


            var query = MeldingController.SorteerOpFiler("Meeste Weergaven",Meldingen);
            Assert.True(query.Count==3);
        }
        [Fact]
        public void TestDatabase(){
            var context = getContext();
            var usermanager = GetUserManager();
            var MeldingController = new MeldingController(context,usermanager);

            var melding = MeldingController.ReturnEerste();
            Assert.True(context.Meldingen.FirstOrDefault()==null);
        }
    }
}
