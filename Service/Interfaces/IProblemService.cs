using System.Collections.Generic;

using Domain.Models;

namespace OnlineJudge.Service.Interfaces
{
    public interface IProblemService : IService
    {
        Problem GetProblem(string problemCode);

        IList<Problem> GetAllProblems();

        Problem GetProblemWithStatement(string problemCode);

        IList<Problem> GetAllProblemsWithStatements();
    }
}
