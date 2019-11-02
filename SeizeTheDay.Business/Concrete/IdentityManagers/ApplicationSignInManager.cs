using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using SeizeTheDay.Entities.Identity.Entities;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SeizeTheDay.Business.Concrete.IdentityManagers
{
    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.

    // Configure the application sign-in manager which is used in this application.
    public class ApplicationSignInManager : SignInManager<User, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(User user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }
    }
}
