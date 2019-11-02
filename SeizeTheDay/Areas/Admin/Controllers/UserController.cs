using System.Web.Mvc;

namespace SeizeTheDay.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        // GET: Admin/User

        public ActionResult Index(int? ID)
        {
            return View();                   
        }

        public ActionResult ChangingPassword()
        {
            return View();
        }
    }
}