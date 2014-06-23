using System;
using System.Web;
using System.Web.Mvc;

using OnlineJudge.Service;
using OnlineJudge.Service.Interfaces;

namespace OnlineJudge.Web.Controllers
{
    [Authorize]
    public class ProblemController : Controller
    {
        private readonly IProblemService problemService;
        private readonly ITesterService testerService;
        private readonly IFileService fileService;

        public ProblemController()
        {
            problemService = new ProblemService();
            fileService = new FileService();
            testerService = new TesterService();
        }

        public ActionResult List()
        {
            var problems = problemService.GetAllProblems();

            return this.View("List", problems);
        }

        public ActionResult Get(string problemCode)
        {
            var problem = problemService.GetProblemWithStatement(problemCode);

            return this.View("Problem", problem);
        }

        [HttpPost]
        public ActionResult Submit(HttpPostedFileBase file, string problemCode)
        {
            var userDir = fileService.PrepareDirectoryForUser(this.User.Identity.Name);
            var codeFilePath = fileService.SaveUploadedFileToDisk(file, userDir);

            if (codeFilePath == null)
            {
                throw new Exception("Invalid file uploaded");
            }

            var result = this.testerService.TestCode(codeFilePath, problemCode);

            return this.View("Result", result);
        }


    }
}
