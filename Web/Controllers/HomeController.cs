using System.Web.Mvc;

using OnlineJudge.Service;
using OnlineJudge.Service.Interfaces;

namespace OnlineJudge.Web.Controllers
{
    [HandleError(View = "Error")]
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

        public ActionResult Home()
        {
            return this.View("Home");
        }
    }
}
