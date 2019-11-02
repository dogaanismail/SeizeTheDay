using System.Web.Mvc;

namespace SeizeTheDay.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
              "Admin_default",
              "Admin/{controller}/{action}/{id}",
               defaults: new { controller = "AdminAccount", action = "Login", id = UrlParameter.Optional }

          );
        }
    }
}