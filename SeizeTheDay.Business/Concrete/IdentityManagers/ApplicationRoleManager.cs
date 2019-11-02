using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using SeizeTheDay.Entities.Identity;
using SeizeTheDay.Entities.Identity.Entities;

namespace SeizeTheDay.Business.Concrete.IdentityManagers
{
    public class ApplicationRoleManager : RoleManager<Roles>
    {
        public ApplicationRoleManager(IRoleStore<Roles, string> roleStore)
            : base(roleStore)
        {

        }
        
        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context)
        {
            return new ApplicationRoleManager(new RoleStore<Roles>(context.Get<IdentityContext>()));
        }
    }
}
