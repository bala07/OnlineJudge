using System.Collections.Generic;
using System.Web;

using DataAccessLayer;
using DataAccessLayer.RepositoryInterfaces;

using Domain.Models;

using OnlineJudge.Service.Interfaces;

namespace OnlineJudge.Service
{
    public class ProblemService : IProblemService
    {
        private readonly IProblemRepository problemRepository;

        private readonly IFileService fileService;

        public ProblemService()
        {
            problemRepository = new ProblemRepository();
            fileService = new FileService();
        }

        public Problem GetProblem(string name)
        {
            var problem = problemRepository.Get(name);

            return problem;
        }

        public IList<Problem> GetAllProblems()
        {
            var problems = problemRepository.GetAll();

            return problems;
        }

        public Problem GetProblemWithStatement(string problemCode)
        {
            var problem = this.GetProblem(problemCode);

            var location = HttpContext.Current.Server.MapPath("~/App_Data" + problem.Location);
            problem.Statement = fileService.ReadFromFile(location);

            return problem;
        }

        public IList<Problem> GetAllProblemsWithStatements()
        {
            var problems = this.GetAllProblems();

            foreach (var problem in problems)
            {
                var location = HttpContext.Current.Server.MapPath("~/App_Data" + problem.Location);
                problem.Statement = fileService.ReadFromFile(location);
            }

            return problems;
        }


    }
}
