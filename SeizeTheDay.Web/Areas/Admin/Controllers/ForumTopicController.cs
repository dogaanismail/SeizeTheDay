using System.Web.Mvc;

namespace SeizeTheDay.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ForumTopicController : Controller
    {
        // GET: Admin/ForumTopic     
        public ActionResult Index()
        {
            return View();
        }
    }
}