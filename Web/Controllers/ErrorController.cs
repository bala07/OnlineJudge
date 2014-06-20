using System.Net;
using System.Web.Mvc;

namespace OnlineJudge.Web.Controllers
{
    public class ErrorController : Controller
    {
        //
        // GET: /Error/
        public ActionResult NotFound()
        {
            this.Response.StatusCode = (int)HttpStatusCode.NotFound;

            return this.View();
        }

        public ActionResult Unauthorized()
        {
            this.Response.StatusCode = (int)HttpStatusCode.Unauthorized;

            return this.View();
        }

        public ActionResult ServerError()
        {
            this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return this.View();
        }

    }
}
