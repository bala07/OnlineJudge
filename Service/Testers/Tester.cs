using Domain.Models;

using OnlineJudge.Service.CodeExecutionService;

namespace OnlineJudge.Service.Testers
{
    public class Tester : ITester
    {
        private readonly ICodeExecutionService codeExecutionService;

        private readonly ITesterHelper testerHelper;

        public Tester(ICodeExecutionService codeExecutionService, ITesterHelper testerHelper)
        {
            this.codeExecutionService = codeExecutionService;
            this.testerHelper = testerHelper;
        }

        public void Test(string codeFilePath, string problemCode)
        {
            var compilationSuccessful = this.Compile(codeFilePath);

            if (!compilationSuccessful)
            {
                testerHelper.HandleCompilationError(codeFilePath);

                return;
            }

            var testSuite = testerHelper.GetTestSuite(problemCode);

            foreach (var testCase in testSuite.TestCases)
            {
                testerHelper.WriteTestInputToFile(codeFilePath, testCase);

                var executionSuccessful = this.Run(codeFilePath, new string[] { });

                if (!executionSuccessful)
                {
                    testerHelper.HandleRuntimeError(codeFilePath);

                    return;
                }

                var correctAnswer = testerHelper.CompareOutputs(codeFilePath, testCase);

                if (!correctAnswer)
                {
                    testerHelper.HandleSuccessfulExecution(codeFilePath, ExecutionResult.WrongAnswer);

                    return;
                }
            }

            testerHelper.HandleSuccessfulExecution(codeFilePath, ExecutionResult.CorrectAnswer);
        }

        public ExecutionResult GetTesterResult(string codeFilePath)
        {
            return testerHelper.GetTesterResult(codeFilePath);
        }

        private bool Compile(string codeFilePath)
        {
            return this.codeExecutionService.Compile(codeFilePath);
        }

        private bool Run(string codeFilePath, string[] commandLineArguments)
        {
            return this.codeExecutionService.Run(codeFilePath, commandLineArguments);
        }
    }
}
