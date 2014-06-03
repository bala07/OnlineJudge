using System;
using System.Web;
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

        public ActionResult Home()
        {
            return this.View("Home");
        }

        [HttpPost]
        public ActionResult Submit(HttpPostedFileBase file)
        {
            var codeFilePath = fileService.SaveUploadedFileToDisk(file);

            if (codeFilePath == null)
            {
                throw new Exception("Invalid file uploaded");
            }

            var results = this.testerService.TestCode(codeFilePath);

            return this.View("Result/Result");
        }

    }
}
