// using System;
// using Xunit;
// using Look.Models;
// using Look.Controllers;
// using Look.Areas.Identity.Data;
// using Microsoft.Extensions.Logging;
// using Microsoft.Extensions.DependencyInjection;
// using Microsoft.EntityFrameworkCore;
// using Moq;
// using System.Linq;
// using Microsoft.EntityFrameworkCore.InMemory;
// using Microsoft.AspNetCore.Identity;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Http;
// using Microsoft.Extensions.Options;
// using Microsoft.AspNetCore.Authentication;
// using Microsoft.AspNetCore.Mvc;
// using System.Collections.Generic;
// using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
// using Microsoft.AspNetCore.Http.Abstractions;
// using Microsoft.Extensions.Logging.Abstractions;
// using Autofac.Extras.Moq;


// namespace Look.Tests
// {
    
//     public class SorteerTests
//     {
//         private string databaseName;

//         public LookIdentityDbContext getContext(){
//             LookIdentityDbContext context = getNewContext(true);

//             context.Meldingen.Add(new Melding {Titel="Ik eet pizza",Inhoud="ik heb lekker een pizza gegeten",Likes=3,Views=5,Categorie="Overlast"});
//             context.Meldingen.Add(new Melding {Titel="Wat ga ik doen",Inhoud="a pizza",Likes=7,Views=8,Categorie="Bijeenkomst"});          
//             context.Meldingen.Add(new Melding {Titel="Ik bak taarten",Inhoud="z ik heb goede taart recepten",Likes=5,Views=156,Categorie="Overlast"}); 
//             context.Meldingen.Add(new Melding {Titel="Ik ben een jas kwijt",Inhoud="Het was een gele",Likes=77,Views=78,Categorie="Verloren voorwerp"});
//             context.Meldingen.Add(new Melding {Titel="Ik heb een gele jas gevonden",Inhoud="Er lag een jas in het speeltuintje",Likes=76,Views=78,Categorie="Gevonden Voorwerp"}); 
//             context.Meldingen.Add(new Melding {Titel="Testen Schrijven",Inhoud="Poeh hey wat was ik hier lang mee bezig",Likes=0,Views=1,Categorie="Verdachte Activiteit"}); 
            
//             context.Users.Add(new ApplicationUser { FirstName = "Bastiaan", LastName = "van Toor", UserName="Bassie",Email="Grappenmaker@gmail.com"});
//             context.Users.Add(new ApplicationUser { FirstName = "Adriaan" ,LastName = "van Toor" , UserName="Adriaan",Email="Acrobaatje"});
            
//             context.SaveChanges();

//             return getNewContext(false);
//         }

//         private LookIdentityDbContext getNewContext(bool NewDb){
//             if(NewDb) this.databaseName = "MockDatabase";
            
//             var options = new DbContextOptionsBuilder<LookIdentityDbContext>()
//             .UseInMemoryDatabase(databaseName)
//             .Options;
            
//             return new LookIdentityDbContext(options);
//         }

//         private UserManager<ApplicationUser> GetUserManager(){
//             var UserStore = new Mock<IUserStore<ApplicationUser>>();
//             var UserManager = new Mock<UserManager<ApplicationUser>>(UserStore.Object,null,null,null,null,null,null,null,null);
//             return UserManager.Object;
//         }


//         [Theory]
//         [InlineData("Ik eet pizza",1)]
//         [InlineData("Ik bak taarten",2)]
//         [InlineData("Ik heb een jas gevonden",3)]
//         [InlineData("Testen Schrijven",4)]
//         public void MockDataBaseTest(string expected,long id){
//             var context = getContext();
//             var output = context.Meldingen.Where(m=>m.MeldingID==id).FirstOrDefault().Titel;
//             Assert.True(output==expected);
//         }

//         [Theory]
//         [InlineData("pizza","t","Ik eet pizza")] 
//         [InlineData("gele","t","Ik heb een gele jas gevonden")]
//         [InlineData("pizza","i","Ik eet pizza")] 
//         [InlineData("gele","i","Ik ben een jas kwijt")]
//         [InlineData("pizza","ti","Ik eet pizza")] 
//         [InlineData("gele","ti","Ik ben een jas kwijt")] 
//         public void TestSorteren(string zoekopdracht,string zo,string expected){
//             MeldingController meldingController = new MeldingController(getContext(),GetUserManager());
//             List<Melding> output = meldingController.ZoekenOpFilter(zoekopdracht,zo,getContext().Meldingen.ToList());
//             var database = getContext();
//             Assert.True(output.FirstOrDefault().Titel==expected);
//         }

//         [Theory]
//         [InlineData("Meeste likes","Aflopend","Ik ben een jas kwijt")]
//         [InlineData("Naam","Aflopend","Wat ga ik doen")] 
//         [InlineData("Meeste weergaven","Aflopend","Ik bak taarten")] 
//         [InlineData("Meeste likes","Oplopend","Testen Schrijven")]
//         [InlineData("Naam","Oplopend","Ik bak taarten")]
//         [InlineData("Meeste weergaven","Oplopend","Testen Schrijven")]
//         public void TestFilteren(string sorteerVar,string v,string expected){
//             MeldingController meldingController = new MeldingController(getContext(),GetUserManager());
//             List<Melding> output = meldingController.SorteerOpFiler(sorteerVar,v,getContext().Meldingen.ToList());
//             Assert.True(output.FirstOrDefault().Titel==expected);
//         }
//     }
// }
