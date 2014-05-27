using System.Security.Cryptography.X509Certificates;

namespace OnlineJudge.Service.Interfaces
{
    public interface ICompilationService
    {
        bool Compile(string codeFilePath);
    }
}
