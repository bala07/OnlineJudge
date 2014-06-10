using System.Collections.Generic;
using System.Web.Http;
using System.Web.Mvc;

using Domain.Models;

using OnlineJudge.Service;
using OnlineJudge.Service.Interfaces;

namespace OnlineJudge.Web.Controllers
{
    [RoutePrefix("/problems")]
    public class ProblemController : Controller
    {
        private readonly IProblemService problemService;

        public ProblemController()
        {
            problemService = new ProblemService();
        }

        [Route("/problems/list")]
        public ActionResult List()
        {
            var problems = problemService.GetAllProblems();

            return this.View(problems);
        }

//        [Route("/problems/{name : string}")]
//        public ActionResult Get(string name)
//        {
//            var problem = problemService.GetProblem(name);
//        }
    }
}
