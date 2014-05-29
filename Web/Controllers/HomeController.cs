using System;
using System.Web;
using System.Web.Mvc;

using Domain.Models;

using OnlineJudge.Service;
using OnlineJudge.Service.Interfaces;

namespace OnlineJudge.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFileService fileService;

        private readonly IExecutionService executionService;

        private readonly IPathService pathService;

        public HomeController()
        {
            this.fileService = new FileService();
            this.executionService = new ExecutionService();
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
            var testerFilePath = pathService.GetTesterFilePath(codeFilePath);

            var testerCompilationResults = executionService.Compile(testerFilePath);

            if (testerCompilationResults.ExecutionResult.Equals(ExecutionResult.TesterError))
            {
                // Tester should be error free!!!
                throw new Exception();
            }

            var testerExecutionResults = executionService.Run(testerFilePath);

            if (testerExecutionResults.ExecutionResult.Equals(ExecutionResult.TesterError))
            {
                // Tester should be error free!!!
                throw new Exception();
            }

            return new ViewResult();
        }

    }
}
