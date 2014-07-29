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

        public BaseTester(ICodeExecutionService codeExecutionService, IPathService pathService, IFileService fileService)
        {
            this.CodeExecutionService = codeExecutionService;
            this.PathService = pathService;
            this.FileService = fileService;
        }

        public virtual void Test(string codeFilePath, TestSuite testSuite)
        {
        }

        protected bool Compile(string codeFilePath)
        {
            return this.CodeExecutionService.Compile(codeFilePath);
        }

        protected bool Run(string codeFilePath, string[] commandLineArguments)
        {
            return this.CodeExecutionService.Run(codeFilePath, commandLineArguments);
        }

        protected void HandleCompilationError(string codeFilePath)
        {
            var errorFilePath = this.PathService.GetErrorFilePath(codeFilePath);
            var compilationErrorFilePath = this.PathService.GetErrorFilePath(codeFilePath);
            var resultFilePath = this.PathService.GetResultFilePath(codeFilePath);

            var compilationError = this.FileService.ReadFromFile(errorFilePath);

            this.FileService.WriteToFile(compilationErrorFilePath, compilationError);
            this.FileService.WriteToFile(resultFilePath, "CompilationError");
        }

        protected void HandleSuccessFulExecution(string codeFilePath, ExecutionResult result)
        {
            var resultFilePath = this.PathService.GetResultFilePath(codeFilePath);

            this.FileService.WriteToFile(resultFilePath, result.ToString());
        }

        protected void HandleRuntimeError(string codeFilePath)
        {
            var errorFilePath = this.PathService.GetErrorFilePath(codeFilePath);
            var runtimeErrorFilePath = this.PathService.GetErrorFilePath(codeFilePath);
            var resultFilePath = this.PathService.GetResultFilePath(codeFilePath);

            var runtimeError = this.FileService.ReadFromFile(errorFilePath);

            this.FileService.WriteToFile(runtimeErrorFilePath, runtimeError);
            this.FileService.WriteToFile(resultFilePath, "RuntimeError");
        }
    }
}
