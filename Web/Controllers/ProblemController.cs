using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

using OnlineJudge.Service;
using OnlineJudge.Service.Interfaces;

namespace OnlineJudge.Web.Controllers
{
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

        public ActionResult Get(string name)
        {
            var problem = problemService.GetProblemWithStatement(name);

            return this.View("Problem", problem);
        }

        [System.Web.Mvc.HttpPost]
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
