using Domain.Models;

namespace OnlineJudge.Service.Interfaces
{
    public interface ITesterService
    {
        Result TestCode(string codeFilePath, string problemCode);
    }
}
