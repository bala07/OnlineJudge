using System.Collections.Generic;
using System.Linq;

using DataAccessLayer.RepositoryInterfaces;

using Domain.Models;

namespace DataAccessLayer
{
    public class ProblemRepository : IProblemRepository
    {
        private readonly OnlineJudgeContext onlineJudgeContext;

        public ProblemRepository()
        {
            onlineJudgeContext = new OnlineJudgeContext();
        }

        public Problem Get(string problemCode)
        {
            var problem = onlineJudgeContext.Problems.FirstOrDefault(problem1 => problem1.Code == problemCode);

            return problem;
        }

        public IList<Problem> GetAll()
        {
            return onlineJudgeContext.Problems.ToList();
        }
    }
}