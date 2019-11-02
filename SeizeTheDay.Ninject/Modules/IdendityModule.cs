using Microsoft.Owin.Security;
using Ninject.Modules;
using SeizeTheDay.Business.Concrete.IdentityManagers;
using SeizeTheDay.Core.CrossCuttingConcerns.Security.AspNetIdentity;
using System.Web;


namespace SeizeTheDay.Ninject.Modules
{
    public class IdendityModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ApplicationUserManager>().ToMethod(GetOwin.GetOwinInjection<ApplicationUserManager>);
            Bind<ApplicationSignInManager>().ToMethod(GetOwin.GetOwinInjection<ApplicationSignInManager>);
            Bind<ApplicationRoleManager>().ToMethod(GetOwin.GetOwinInjection<ApplicationRoleManager>);

            Bind<IAuthenticationManager>().ToMethod(context =>
            {
                var contextBase = new HttpContextWrapper(HttpContext.Current);
                return contextBase.GetOwinContext().Authentication;
            });

        }
    }
}