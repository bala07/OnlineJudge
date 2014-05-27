using OnlineJudge.Service.CodeExecutionService;
using OnlineJudge.Service.Interfaces;

namespace OnlineJudge.Service
{
    public class CompilationService : ICompilationService
    {
        private readonly ICodeExecutionService codeExecutionService;

        public CompilationService()
        {
            codeExecutionService = new CodeExecutionServiceClient();
        }

        public bool Compile(string codeFilePath)
        {
            return codeExecutionService.Compile(codeFilePath);
        }
    }
}
