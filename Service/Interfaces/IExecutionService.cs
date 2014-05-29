using Domain.Models;

namespace OnlineJudge.Service.Interfaces
{
    public interface IExecutionService
    {
        Result Compile(string filePath);

        Result Run(string codeFilePath);
    }
}
