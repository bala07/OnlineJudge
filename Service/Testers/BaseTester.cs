using Domain.Models;

using OnlineJudge.Service.CodeExecutionService;
using OnlineJudge.Service.Interfaces;

namespace OnlineJudge.Service.Testers
{
    public class BaseTester : ITester
    {
        protected readonly ICodeExecutionService CodeExecutionService;

        protected readonly IPathService PathService;

        protected readonly IFileService FileService;

        public BaseTester()
        {
            CodeExecutionService = new CodeExecutionServiceClient();
            PathService = new PathService();
            FileService = new FileService();
        }

        public virtual void Test(string codeFilePath, string resultFilePath)
        {
        }

        protected bool Compile(string codeFilePath)
        {
            return CodeExecutionService.Compile(codeFilePath);
        }

        protected bool Run(string codeFilePath)
        {
            return CodeExecutionService.Run(codeFilePath);
        }

        protected void HandleCompilationError(string codeFilePath)
        {
            var errorFilePath = PathService.GetErrorFilePath(codeFilePath);
            var compilationErrorFilePath = PathService.GetCompilationErrorFilePath(codeFilePath);
            var resultFilePath = PathService.GetResultFilePath(codeFilePath);

            var compilationError = FileService.ReadFromFile(errorFilePath);

            FileService.WriteToFile(compilationErrorFilePath, compilationError);
            FileService.WriteToFile(resultFilePath, "CompilationError");
        }

        protected void HandleSuccessFulExecution(string codeFilePath, ExecutionResult result)
        {
            var resultFilePath = PathService.GetResultFilePath(codeFilePath);

            FileService.WriteToFile(resultFilePath, result.ToString());
        }

        protected void HandleRuntimeError(string codeFilePath)
        {
            var errorFilePath = PathService.GetErrorFilePath(codeFilePath);
            var runtimeErrorFilePath = PathService.GetRuntimeErrorFilePath(codeFilePath);
            var resultFilePath = PathService.GetResultFilePath(codeFilePath);

            var runtimeError = FileService.ReadFromFile(errorFilePath);

            FileService.WriteToFile(runtimeErrorFilePath, runtimeError);
            FileService.WriteToFile(resultFilePath, "RuntimeError");
        }
    }
}
