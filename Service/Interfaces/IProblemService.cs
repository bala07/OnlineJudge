using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

using Domain.Models;

namespace OnlineJudge.Service.Interfaces
{
    public interface IProblemService
    {
        Problem GetProblem(string name);

        IList<Problem> GetAllProblems();

        Problem GetProblemWithStatement(string name);

        IList<Problem> GetAllProblemsWithStatements();
    }
}
