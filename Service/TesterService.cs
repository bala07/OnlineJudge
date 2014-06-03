using Domain.Models;

using OnlineJudge.Service.CodeExecutionService;
using OnlineJudge.Service.Interfaces;
using OnlineJudge.Service.Testers;

namespace OnlineJudge.Service
{
    public class TesterService : ITesterService
    {
        private readonly IFileService fileService;

        private readonly IPathService pathService;

        public TesterService()
        {
            fileService = new FileService();
            pathService = new PathService();
        }

        public Result TestCode(string codeFilePath)
        {
            var tester = TesterFactory.GetTester(codeFilePath);

            tester.Test(codeFilePath);

            var result = new Result { ExecutionResult = fileService.GetTesterResult(codeFilePath) };

            if (!result.ExecutionResult.Equals(ExecutionResult.CorrectAnswer))
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
