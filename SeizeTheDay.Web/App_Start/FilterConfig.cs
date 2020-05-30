using System.Web.Mvc;

namespace SeizeTheDay.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());          
        }
    }
}
