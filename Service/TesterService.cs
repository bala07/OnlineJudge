using System.IO;

using Domain.Models;

using OnlineJudge.Service.Interfaces;
using OnlineJudge.Service.Testers;

namespace OnlineJudge.Service
{
    public class TesterService : ITesterService
    {
        private readonly IFileService fileService;

        private readonly IPathService pathService;

        private readonly ITester tester;

        public TesterService(IFileService fileService, IPathService pathService, ITester tester)
        {
            this.fileService = fileService;
            this.pathService = pathService;
            this.tester = tester;
        }

        public Result TestCode(string codeFilePath, string problemCode)
        {
            tester.Test(codeFilePath, problemCode);

            var result = new Result { 
                                        ProblemName = problemCode,
                                        Language = Utilities.GetLangaugeNameFromExtension(Path.GetExtension(codeFilePath)),
                                        ExecutionResult = tester.GetTesterResult(codeFilePath)
                                    };

            if (!(result.ExecutionResult.Equals(ExecutionResult.CorrectAnswer) || result.ExecutionResult.Equals(ExecutionResult.WrongAnswer)))
            {
                PopulateErrorDetailsToResult(result, codeFilePath);
            }

            return result;

        }

        private void PopulateErrorDetailsToResult(Result result, string codeFilePath)
        {
            var errorMessage = fileService.ReadFromFile(pathService.GetErrorFilePath(codeFilePath));

            result.ErrorMessage = errorMessage;
        }
    }
}
