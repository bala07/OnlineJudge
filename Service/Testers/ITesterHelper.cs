using Domain.Models;

namespace OnlineJudge.Service.Testers
{
    public interface ITesterHelper
    {
        TestSuite GetTestSuite(string problemCode);

        void WriteTestInputToFile(string codeFilePath, TestCase testcase);

        bool CompareOutputs(string codeFilePath, TestCase testcase);

        void HandleCompilationError(string codeFilePath);

        void HandleSuccessfulExecution(string codeFilePath, ExecutionResult result);

        void HandleRuntimeError(string codeFilePath);
    }
}