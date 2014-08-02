using System.IO;

using Domain.Models;

using OnlineJudge.Service.CodeExecutionService;
using OnlineJudge.Service.Interfaces;

namespace OnlineJudge.Service.Testers
{
    //TODO: Needs refactoring - is BaseTester required?? Can it be made a TestHelper??
    public class Tester : BaseTester
    {
        public Tester(ICodeExecutionService codeExecutionService, IPathService pathService, IFileService fileService)
            : base(codeExecutionService, pathService, fileService)
        {
        }

        public override void Test(string codeFilePath, TestSuite testSuite)
        {

            var compilationSuccessful = this.Compile(codeFilePath);

            if (!compilationSuccessful)
            {
                this.HandleCompilationError(codeFilePath);

                return;
            }

            foreach (var testCase in testSuite.TestCases)
            {
                WriteInputToFile(codeFilePath, testCase);

                var executionSuccessful = this.Run(codeFilePath, new string[] { });

                if (!executionSuccessful)
                {
                    this.HandleRuntimeError(codeFilePath);

                    return;
                }

                var correctAnswer = CompareOutputs(codeFilePath, testCase);

                if (!correctAnswer)
                {
                    this.HandleSuccessFulExecution(codeFilePath, ExecutionResult.WrongAnswer);

                    return;
                }
            }

            this.HandleSuccessFulExecution(codeFilePath, ExecutionResult.CorrectAnswer);
        }

        private void WriteInputToFile(string codeFilePath, TestCase testCase)
        {
            var directory = Path.GetDirectoryName(codeFilePath);
            var inputFilePath = directory + "\\input.txt";

            FileService.WriteLinesToFile(inputFilePath, testCase.Input);
        }

        private bool CompareOutputs(string codeFilePath, TestCase testCase)
        {
            var directory = Path.GetDirectoryName(codeFilePath);
            var outputFilePath = directory + "\\output.txt";

            var outputContents = FileService.ReadLinesFromFile(outputFilePath);

            for (var idx = 0; idx < testCase.Output.Length; ++idx)
            {
                if (!outputContents[idx].Equals(testCase.Output[idx]))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
