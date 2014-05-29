using System.Web;

using Domain.Models;

namespace OnlineJudge.Service.Interfaces
{
    public interface IFileService
    {
        string SaveUploadedFileToDisk(HttpPostedFileBase file);

        ExecutionResult GetTesterResult(string codeFilePath);

        string GetCompilationErrorMessage(string codeFilePath);

        string GetRuntimeErrorMessage(string codeFilePath);

        string ReadFromFile(string filePath);

        void WriteToFile(string filePath, string contents);
    }
}
