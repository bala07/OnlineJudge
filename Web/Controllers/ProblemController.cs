using System.Web.Mvc;

using OnlineJudge.Service.Interfaces;

namespace OnlineJudge.Web.Controllers
{
    [Authorize]
    public class ProblemController : Controller
    {
        private readonly IProblemService problemService;

        public ProblemController(IProblemService problemService)
        {
            this.problemService = problemService;
        }

        public ActionResult List()
        {
            var problems = problemService.GetAllProblems();

            return this.View("List", problems);
        }

        public ActionResult GetProblemDetails(string problemCode)
        {
            var problem = problemService.GetProblemWithStatement(problemCode);

            return this.View("Problem", problem);
        }
    }
}
