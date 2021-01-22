using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Look.Areas.Identity.Data
{
    public class ApplicationErrorDescriber : IdentityErrorDescriber
{
    public override IdentityError DefaultError() { return new IdentityError { Code = nameof(DefaultError), Description = $"Er heeft zich een onbekende fout voorgedaan." }; }
    public override IdentityError ConcurrencyFailure() { return new IdentityError { Code = nameof(ConcurrencyFailure), Description = "Optimistic concurrency failure, object has been modified." }; }
    public override IdentityError PasswordMismatch() { return new IdentityError { Code = nameof(PasswordMismatch), Description = "Het ingevoerde wachtwoord en/of e-mailadres is incorrect." }; }
    public override IdentityError InvalidToken() { return new IdentityError { Code = nameof(InvalidToken), Description = "Incorrecte token." }; }
    public override IdentityError LoginAlreadyAssociated() { return new IdentityError { Code = nameof(LoginAlreadyAssociated), Description = "Er is al een gebruiker bij ons bekend met deze gegevens. Wil je <a asp-area='Identity' asp-page='Login' >inloggen</a>?" }; }
    public override IdentityError InvalidUserName(string userName) { return new IdentityError { Code = nameof(InvalidUserName), Description = $"De gebruikersnaam '{userName}' is niet toegestaan, een gebruikersnaam mag alleen letters en cijfers bevatten." }; }
    public override IdentityError InvalidEmail(string email) { return new IdentityError { Code = nameof(InvalidEmail), Description = $"Het e-mailadres '{email}' is niet toegestaan."  }; }
    public override IdentityError DuplicateUserName(string userName) { return new IdentityError { Code = nameof(DuplicateUserName), Description = $"De gebruikersnaam '{userName}' is al in gebruik."  }; }
    public override IdentityError DuplicateEmail(string email) { return new IdentityError { Code = nameof(DuplicateEmail), Description = $"Het e-mailadres '{email}' is al in gebruik."  }; }
    public override IdentityError InvalidRoleName(string role) { return new IdentityError { Code = nameof(InvalidRoleName), Description = $"De naam voor rol '{role}' is niet toegestaan."  }; }
    public override IdentityError DuplicateRoleName(string role) { return new IdentityError { Code = nameof(DuplicateRoleName), Description = $"De naam voor rol '{role}' is al in gebruik."  }; }
    public override IdentityError UserAlreadyHasPassword() { return new IdentityError { Code = nameof(UserAlreadyHasPassword), Description = "Gebruiker heeft al een wachtwoord." }; }
    public override IdentityError UserLockoutNotEnabled() { return new IdentityError { Code = nameof(UserLockoutNotEnabled), Description = "Lockout is niet actief voor deze gebruiker." }; }
    public override IdentityError UserAlreadyInRole(string role) { return new IdentityError { Code = nameof(UserAlreadyInRole), Description = $"Gebruiker is al in rol '{role}'."  }; }
    public override IdentityError UserNotInRole(string role) { return new IdentityError { Code = nameof(UserNotInRole), Description = $"Gebruiker is niet in rol '{role}'."  }; }
    public override IdentityError PasswordTooShort(int length) { return new IdentityError { Code = nameof(PasswordTooShort), Description = $"Wachtwoorden moeten tenminste {length} tekens bevatten."  }; }
    public override IdentityError PasswordRequiresNonAlphanumeric() { return new IdentityError { Code = nameof(PasswordRequiresNonAlphanumeric), Description = "Wachtwoord moet minstens één alfanumeriek karakter bevatten." }; }
    public override IdentityError PasswordRequiresDigit() { return new IdentityError { Code = nameof(PasswordRequiresDigit), Description = "Wachtwoord moet minstens één cijfer bevatten ('0'-'9')." }; }
    public override IdentityError PasswordRequiresLower() { return new IdentityError { Code = nameof(PasswordRequiresLower), Description = "Wachtwoord moet minstens één kleine letter bevatten ('a'-'z')." }; }
    public override IdentityError PasswordRequiresUpper() { return new IdentityError { Code = nameof(PasswordRequiresUpper), Description = "Wachtwoord moet minstens één hoofdletter bevatten ('A'-'Z')." }; }
}
}