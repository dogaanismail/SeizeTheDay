using System.Web.Http;
using System.Web.Http.Cors;

namespace SeizeTheDay.Api.Controllers
{
    [EnableCors("*", "*", "*")]
    public abstract class BaseController : ApiController
    {

    }
}
