using System.Collections.Generic;
using System.Web.Mvc;

using Domain.Models;

using OnlineJudge.Service;
using OnlineJudge.Service.Interfaces;

namespace OnlineJudge.Web.Controllers
{
    public class ProblemsController : Controller
    {
        private readonly IProblemService problemService;

        public ProblemsController()
        {
            problemService = new ProblemService();
        }

        [HttpGet]
        public ActionResult List()
        {
            var problems = problemService.GetAllProblems();

            return this.View(problems);
        }

    }
}
