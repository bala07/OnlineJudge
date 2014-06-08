using System.Collections.Generic;

using Domain.Models;

using OnlineJudge.Service.Interfaces;

namespace OnlineJudge.Service
{
    public class ProblemService : IProblemService
    {
        public Problem GetProblem(int id)
        {
            return new Problem();
        }

        public IList<Problem> GetAllProblems()
        {
            var problems = new List<Problem>
                               {
                                   new Problem { Id = 1, ProblemName = "ADDNUM", Difficulty = "Easy" },
                                   new Problem { Id = 2, ProblemName = "SUBNUM", Difficulty = "Easy" }
                               };

            return problems;
        }
    }
}
