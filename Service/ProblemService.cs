using System.Collections.Generic;

using DataAccessLayer;
using DataAccessLayer.RepositoryInterfaces;

using Domain.Models;

using OnlineJudge.Service.Interfaces;

namespace OnlineJudge.Service
{
    public class ProblemService : IProblemService
    {
        private readonly IProblemRepository problemRepository;

        public ProblemService()
        {
            problemRepository = new ProblemRepository();
        }

        public Problem GetProblem(int id)
        {
            var problem = problemRepository.Get(id);

            return problem;
        }

        public IList<Problem> GetAllProblems()
        {
            var problems = problemRepository.GetAll();

            return problems;
        }
    }
}
