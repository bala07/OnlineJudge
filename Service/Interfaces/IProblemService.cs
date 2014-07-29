using System.Collections.Generic;

using Domain.Models;

namespace OnlineJudge.Service.Interfaces
{
    public interface IProblemService : IService
    {
        Problem GetProblem(string name);

        IList<Problem> GetAllProblems();

        Problem GetProblemWithStatement(string problemCode);

        IList<Problem> GetAllProblemsWithStatements();
    }
}
