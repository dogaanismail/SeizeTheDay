using System.Web.Mvc;

namespace SeizeTheDay.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ModuleController : Controller
    {
        // GET: Admin/Module    
        public ActionResult Index()
        {
            return View();
        }
    }
}