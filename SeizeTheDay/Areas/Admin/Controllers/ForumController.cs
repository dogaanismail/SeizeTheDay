using System.Web.Mvc;

namespace SeizeTheDay.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ForumController : Controller
    {
        // GET: Admin/Forum
        public ActionResult Index()
        {
            return View();
        }
    }
}