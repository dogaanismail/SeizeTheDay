using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using SeizeTheDay.Core.Domain.Identity;
using SeizeTheDay.Data.EntityFramework;

namespace SeizeTheDay.Business.Concrete.IdentityManagers
{
    public class ApplicationRoleManager : RoleManager<AppRole, int>
    {
        public ApplicationRoleManager(IRoleStore<AppRole, int> roleStore)
            : base(roleStore)
        {
        }

        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context)
        {
            return new ApplicationRoleManager(new RoleStore<AppRole, int, AppUserRole>(context.Get<SeizeTheDayContext>()));
        }
    }
}
