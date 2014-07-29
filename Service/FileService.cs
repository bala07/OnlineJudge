using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

using Domain.Models;

using OnlineJudge.Service.Interfaces;

namespace OnlineJudge.Service
{
    public class FileService : IFileService
    {
        private readonly IPathService pathService;

        public FileService(IPathService pathService)
        {
            this.pathService = pathService;
        }

        public string SaveUploadedFileToDisk(HttpPostedFileBase file, string userDirectory)
        {
            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(userDirectory, fileName);
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

        public string[] ReadLinesFromFile(string filePath)
        {
            IList<string> linesFromFile = new List<string>();

            using (var fileReader = new StreamReader(filePath))
            {
                var inputLine = fileReader.ReadLine();

                while (inputLine != null)
                {
                    linesFromFile.Add(inputLine);
                    inputLine = fileReader.ReadLine();
                }
            }

            return linesFromFile.ToArray();
        }

        public void WriteLinesToFile(string filePath, string[] contents)
        {
            using (var fileWriter = new StreamWriter(filePath))
            {
                foreach (string line in contents)
                {
                    fileWriter.WriteLine(line);
                }
            }
        }

        public string PrepareDirectoryForUser(string email)
        {
            var basePath = HttpContext.Current.Server.MapPath("~/App_Data/UserSubmissions");
            var userDir = Path.Combine(basePath, email);

            if (!Directory.Exists(userDir))
            {
                Directory.CreateDirectory(userDir);
            }

            return userDir;
        }

        public string PrepareDirectoryForCurrentSubmission(string baseDir, string timeStamp)
        {
            var finalDir = Path.Combine(baseDir, timeStamp);

            if (!Directory.Exists(finalDir))
            {
                Directory.CreateDirectory(finalDir);
            }

            return finalDir;
        }
    }
}
