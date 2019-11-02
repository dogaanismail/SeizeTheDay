using System.Web.Mvc;

namespace SeizeTheDay.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        // GET: Admin/Dashboard
        public ActionResult Index()
        {
           return View();
                  
        }
    }
}