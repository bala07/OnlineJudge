using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

using Domain.Models;

namespace OnlineJudge.Service.Interfaces
{
    public interface IProblemService
    {
        Problem GetProblem(int id);

        IList<Problem> GetAllProblems();
    }
}
