using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SeizeTheDay.Admin.Startup))]
namespace SeizeTheDay.Admin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
