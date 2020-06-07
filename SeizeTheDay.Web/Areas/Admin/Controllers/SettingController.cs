using System.Web.Mvc;

namespace SeizeTheDay.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SettingController : Controller
    {
        // GET: Admin/Setting
        public ActionResult Index()
        {
            return View();
        }
    }
}