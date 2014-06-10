using System.Collections.Generic;
using System.IO;

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

        public Problem GetProblemWithStatement(string name)
        {
            var problem = this.GetProblem(name);
            problem.Statement = fileService.ReadFromFile(problem.Location);

            return problem;
        }

        public IList<Problem> GetAllProblemsWithStatements()
        {
            var problems = this.GetAllProblems();

            foreach (var problem in problems)
            {
                problem.Statement = fileService.ReadFromFile(problem.Location);
            }

            return problems;
        }


    }
}
