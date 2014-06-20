using System.Web.Mvc;

using OnlineJudge.Service;
using OnlineJudge.Service.Interfaces;

namespace OnlineJudge.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFileService fileService;

        private readonly ITesterService testerService;

        private readonly IPathService pathService;

        public HomeController()
        {
            this.fileService = new FileService();
            this.testerService = new TesterService();
            this.pathService = new PathService();
        }

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
