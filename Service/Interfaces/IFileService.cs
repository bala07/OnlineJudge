using System.Web;

using Domain.Models;

namespace OnlineJudge.Service.Interfaces
{
    public interface IFileService : IService
    {
        string SaveUploadedFileToDisk(HttpPostedFileBase file, string userDirectory);

        string ReadFromFile(string filePath);

        void WriteToFile(string filePath, string contents);

        string[] ReadLinesFromFile(string filePath);

        void WriteLinesToFile(string filePath, string[] contents);

        string PrepareDirectoryForUser(string email);

        string PrepareDirectoryForCurrentSubmission(string baseDir, string timeStamp);
    }
}
