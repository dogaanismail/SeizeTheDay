using System.Web.Mvc;

namespace SeizeTheDay.Areas.Admin.Controllers
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