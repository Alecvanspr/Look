using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Look.Areas.Identity.Data;

namespace Look.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Voornaam")]
            public string FirstName {get; set;}
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Achternaam")]
            public string LastName {get; set;}
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Straat")]
            public string Street { get; set; }
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Huisnummer")]
            public string HouseNumber {get; set;}
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Woonplaats")]
            public string City { get; set; }
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Postcode")]
            [RegularExpression(@"[1-9][0-9]{3}\s?[a-zA-Z]{2}", ErrorMessage = "De postcode moet bestaan uit een combinatie van 4 cijfers en 2 letters. <i>VB: 1234AB</i>")]
            public string ZipCode { get; set; }
            public bool IsAnonymous {get; set;}

            [Required]
            [EmailAddress]
            [Display(Name = "E-mailadres")]
            public string Email { get; set; }

            [Required]
            [StringLength(20, ErrorMessage = "Het {0} moet uit minimaal {2} en maximaal {1} karakters bestaan.", MinimumLength = 8)]
            [DataType(DataType.Password)]
            [Display(Name = "Wachtwoord")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Verifieer Wachtwoord")]
            [Compare("Password", ErrorMessage = "Wachtwoorden komen niet overeen.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser 
                { 
                    FirstName = Input.FirstName,
                    LastName = Input.LastName,
                    Street = Input.Street,
                    HouseNumber = Input.HouseNumber,
                    City = Input.City,
                    ZipCode = Input.ZipCode,
                    IsAnonymous = false,
                    UserName = Input.Email,
                    Email = Input.Email
                };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Look E-mailadresverificatie",
                        $"Verifieer je e-mailadres door <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>hier te klikken</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
