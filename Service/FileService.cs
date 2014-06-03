using System;
using System.IO;
using System.Web;

using Domain.Models;

using OnlineJudge.Service.Constants;
using OnlineJudge.Service.Interfaces;

namespace OnlineJudge.Service
{
    public class FileService : IFileService
    {
        private readonly IPathService pathService;

        public FileService()
        {
            pathService = new PathService();
        }

        public string SaveUploadedFileToDisk(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(FileConstants.BaseDirectory, fileName);
                file.SaveAs(path);

                return path;
            }

            return null;
        }

        public ExecutionResult GetTesterResult(string codeFilePath)
        {
            var resultFilePath = pathService.GetResultFilePath(codeFilePath);
            string result;

            using (var fileReader = new StreamReader(resultFilePath))
            {
                result = fileReader.ReadLine();
            }

            var executionResult = (ExecutionResult)Enum.Parse(typeof(ExecutionResult), result);

            return executionResult;
        }

        public string ReadFromFile(string filePath)
        {
            string fileContents;

            using (var fileReader = new StreamReader(filePath))
            {
                fileContents = fileReader.ReadToEnd();
            }

            return fileContents;
        }

        public void WriteToFile(string filePath, string contents)
        {
            using (var fileWriter = new StreamWriter(filePath))
            {
                fileWriter.WriteLine(contents);
            }
        }
    }
}
