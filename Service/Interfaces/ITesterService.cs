using Domain.Models;

namespace OnlineJudge.Service.Interfaces
{
    public interface ITesterService : IService
    {
        Result TestCode(string codeFilePath, string problemCode);
    }
}
