using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;
using SeizeTheDay.Core.Domain.Identity;
using System;

namespace SeizeTheDay.Business.Concrete.IdentityManagers
{
    public class ApplicationUserManager : UserManager<AppUser, int>
    {
        public static IDataProtectionProvider DataProtectionProvider { get; set; }
        public ApplicationUserManager(IUserStore<AppUser, int> store) : base(store)
        {
            // Configure validation logic for usernames
            this.UserValidator = new UserValidator<AppUser, int>(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            this.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            // Configure user lockout defaults
            this.UserLockoutEnabledByDefault = true;
            this.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            this.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            this.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<AppUser, int>
            {
                MessageFormat = "Your security code is {0}"
            });
            this.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<AppUser, int>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });
            this.EmailService = new EmailService();
            this.SmsService = new SmsService();

            var dataProtectionProvider = DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                IDataProtector dataProtector = dataProtectionProvider.Create("ASP.NET Identity");

                this.UserTokenProvider = new DataProtectorTokenProvider<AppUser, int>(dataProtector);
            }
        }
    }
}
