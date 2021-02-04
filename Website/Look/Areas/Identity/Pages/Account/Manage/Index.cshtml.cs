using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Look.Areas.Identity.Data;
using Look.Services;

namespace Look.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Het {0} veld is verplicht.")]
            [DataType(DataType.Text)]
            [Display(Name = "Gebruikersnaam")]
            public string UserName {get; set;}

            [Required(ErrorMessage = "Het {0} veld is verplicht.")]
            [DataType(DataType.Text)]
            [Display(Name = "Voornaam")]
            public string FirstName {get; set;}
            [Required(ErrorMessage = "Het {0} veld is verplicht.")]
            [DataType(DataType.Text)]
            [Display(Name = "Achternaam")]
            public string LastName {get; set;}
            [Required(ErrorMessage = "Het {0} veld is verplicht.")]
            [DataType(DataType.Text)]
            [Display(Name = "Straat")]
            public string Street { get; set; }
            [Required(ErrorMessage = "Het {0} veld is verplicht.")]
            [DataType(DataType.Text)]
            [Display(Name = "Huisnummer")]
            public string HouseNumber {get; set;}
            
            [Required(ErrorMessage = "Het {0} veld is verplicht.")]
            [DataType(DataType.Text)]
            [Display(Name = "Prive Code")]
            public Guid PriveCode {get; set;}
            [DataType(DataType.Text)]
            [Display(Name = "Toevoeging")]
            public string HouseNumberAddition {get; set;}
            [Required(ErrorMessage = "Het {0} veld is verplicht.")]
            [DataType(DataType.Text)]
            [Display(Name = "Woonplaats")]
            public string City { get; set; }
            [Required(ErrorMessage = "Het {0} veld is verplicht.")]
            [DataType(DataType.Text)]
            [Display(Name = "Postcode")]
            [RegularExpression(@"[1-9][0-9]{3}\s?[a-zA-Z]{2}", ErrorMessage = "De postcode moet bestaan uit een combinatie van 4 cijfers en 2 letters. VB: 1234AB")]
            public string ZipCode { get; set; }
            public bool IsAnonymous {get; set;}
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Input = new InputModel
            {
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Street = user.Street,
                HouseNumber = user.HouseNumber,
                HouseNumberAddition = user.HouseNumberAddition,
                City = user.City,
                ZipCode = user.ZipCode,
                IsAnonymous = user.IsAnonymous,
                PriveCode = user.PriveCode
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            bool IsAddressValid = (AddressCheck.Validate(Input.ZipCode, Input.HouseNumber, Input.HouseNumberAddition, Input.Street, Input.City).status == "ok" ? true : false);

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            //username error message in profile
            if(Input.UserName != user.UserName)
            {
                user.UserName = Input.UserName;
            }

            //firstname error message in profile
            if(Input.FirstName != user.FirstName)
            {
                user.FirstName = Input.FirstName;
            }

            //lastname error message in profile
            if(Input.LastName != user.LastName)
            {
                user.LastName = Input.LastName;
            }

            //street error message in profile
            if(Input.Street != user.Street)
            {
                user.Street = Input.Street;
            }

            //housenumber error message in profile
            if(Input.HouseNumber != user.HouseNumber)
            {
                user.HouseNumber = Input.HouseNumber;
            }

            //housenumberaddition error message in profile
            if(Input.HouseNumberAddition != user.HouseNumberAddition)
            {
                user.HouseNumberAddition = Input.HouseNumberAddition;
            }

            //city error message in profile
            if(Input.City != user.City)
            {
                user.City = Input.City;
            }

            //privecode error message in profile
            if(Input.PriveCode != user.PriveCode)
            {
                user.PriveCode = Input.PriveCode;
            }

            //zipcode error message in profile
            if(Input.ZipCode != user.ZipCode)
            {
                user.ZipCode = Input.ZipCode;
            }

            //isanonymous error message in profile
            if(Input.IsAnonymous != user.IsAnonymous)
            {
                user.IsAnonymous = Input.IsAnonymous;
            }

            if(IsAddressValid)
            {
                await _userManager.UpdateAsync(user);

                await _signInManager.RefreshSignInAsync(user);
                StatusMessage = "Je profiel is succesvol gewijzigd.";
                return RedirectToPage();
            }
            else
            {
                StatusMessage = "Error, je hebt een ongeldig adres opgegeven.";
                return RedirectToPage();
            }
        }
    }
}
