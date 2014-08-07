using System.Collections.Generic;
using System.Linq;

using DataAccessLayer.Interfaces;

using Domain.Models;

namespace DataAccessLayer
{
    public class ProblemRepository : IProblemRepository
    {
        private readonly OnlineJudgeContext onlineJudgeContext;

        public ProblemRepository(OnlineJudgeContext onlineJudgeContext)
        {
            this.onlineJudgeContext = onlineJudgeContext;
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