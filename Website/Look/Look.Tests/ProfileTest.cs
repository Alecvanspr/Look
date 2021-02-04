using System;
using Xunit;
using Look.Models;
using Look.Controllers;
using Look.Areas.Identity.Data;
using Look.Areas.Identity.Pages.Account;
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
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Web;
using System.Net;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Look.Services;
using Newtonsoft.Json;

namespace Look.Tests
{
    
    public class ProfileTests
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
        private RoleManager<ApplicationRole> GetRoleManager(){
            var UserStore = new Mock<IUserStore<ApplicationRole>>();
            var RoleManager = new Mock<RoleManager<ApplicationRole>>(UserStore.Object,null,null,null,null,null,null,null,null);
            return RoleManager.Object;
        }
        private SignInManager<ApplicationUser> GetSignInManager(){
           var UserStore = new Mock<IUserStore<ApplicationUser>>();
           var SignInManager = new Mock<SignInManager<ApplicationUser>>(UserStore.Object,null,null,null,null,null,null,null,null);
           return SignInManager.Object; 
        }
        private IEmailSender GetIEmailManager(){
            var services = new ServiceCollection();
            var serviceProvider = services.BuildServiceProvider();
            var emailSender = serviceProvider.GetService<IEmailSender>();
          return emailSender; 
        }
        public ILogger<RegisterModel> GetLogger(){
            var mock = new Mock<ILogger<RegisterModel>>();
            ILogger<RegisterModel> logger = mock.Object;
            return logger;
        }

        //dit is handiger om visueel te testen
    }
}