using System.Web.Mvc;

namespace OnlineJudge.Web.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Home()
        {
            return this.View("Home");
        }

        [HttpGet]
        public ActionResult About()
        {
            return this.View();
        }

        [HttpGet]
        public ActionResult ContactUs()
        {
            return this.View();
        }

    }
}
