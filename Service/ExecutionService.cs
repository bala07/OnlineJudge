using Domain.Models;

using OnlineJudge.Service.CodeExecutionService;
using OnlineJudge.Service.Interfaces;

namespace OnlineJudge.Service
{
    public class ExecutionService : IExecutionService
    {
        private readonly ICodeExecutionService codeExecutionService;

        private readonly IFileService fileService;

        public ExecutionService()
        {
            codeExecutionService = new CodeExecutionServiceClient();
            fileService = new FileService();
        }

        public Result Compile(string filePath)
        {
            var result = new Result
                             {
                                 ExecutionResult =
                                     this.codeExecutionService.Compile(filePath)
                                         ? ExecutionResult.CompilationSuccessful
                                         : ExecutionResult.TesterError
                             };

            return result;
        }

        public Result Run(string codeFilePath)
        {
            var isSuccessful = codeExecutionService.Run(codeFilePath);

            var result = new Result();
            if (!isSuccessful)
            {
                result.ExecutionResult = ExecutionResult.TesterError;
                return result;
            }

            result.ExecutionResult = fileService.GetTesterResult(codeFilePath);

            if (result.ExecutionResult.Equals(ExecutionResult.CompilationError))
            {
                result.CompilationErrorMessage = fileService.GetCompilationErrorMessage(codeFilePath);
            }
            else if (result.ExecutionResult.Equals(ExecutionResult.RuntimeError))
            {
                result.RuntimeErrorMessage = fileService.GetRuntimeErrorMessage(codeFilePath);
            }

            return result;
        }

    }
}
