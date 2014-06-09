using System.Collections;
using System.Collections.Generic;
using System.Linq;

using DataAccessLayer.RepositoryInterfaces;

using Domain.Models;

namespace DataAccessLayer
{
    public class ProblemRepository : IProblemRepository
    {
        private readonly OnlineJudgeEntities onlineJudgeContext = new OnlineJudgeEntities();

        public Problem Get(int id)
        {
            var problemFromDb = onlineJudgeContext.problems.FirstOrDefault(problem1 => problem1.Id == id);

            return problemFromDb == null ? null : this.ConvertToProblem(problemFromDb);
        }

        public IList<Problem> GetAll()
        {
            var problemsFromDb = onlineJudgeContext.problems;

            return problemsFromDb.Select(problem => this.ConvertToProblem(problem)).ToList();
        }

        private Problem ConvertToProblem(problem problem)
        {
            return new Problem
            {
                Id = problem.Id,
                Name = problem.Name,
                Location = problem.Location,
                Difficulty = problem.Difficulty
            };   
        }

    }
}