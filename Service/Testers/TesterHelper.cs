using System.Collections.Generic;
using System.IO;
using System.Web;

using Domain.Models;

using Newtonsoft.Json;

using OnlineJudge.Service.Interfaces;

namespace OnlineJudge.Service.Testers
{
    public class TesterHelper : ITesterHelper
    {
        private readonly IFileService fileService;

        private readonly IPathService pathService;

        public TesterHelper(IFileService fileService, IPathService pathService)
        {
            this.fileService = fileService;
            this.pathService = pathService;
        }

        //TODO: How to test this method?
        public TestSuite GetTestSuite(string problemCode)
        {
            var testSuitePath = HttpContext.Current.Server.MapPath("~/App_Data/TestSuites");
            testSuitePath += "/" + problemCode + "_TEST_SUITE.json";

            IList<TestCase> testCases = JsonConvert.DeserializeObject<List<TestCase>>(File.ReadAllText(testSuitePath));

            return new TestSuite { TestCases = testCases };
        }

        public void HandleCompilationError(string codeFilePath)
        {
            var errorFilePath = this.pathService.GetErrorFilePath(codeFilePath);
            var localErrorFilePath = this.pathService.GetLocalErrorFilePath(codeFilePath);
            var resultFilePath = this.pathService.GetResultFilePath(codeFilePath);

            var compilationError = this.fileService.ReadFromFile(errorFilePath);

            this.fileService.WriteToFile(localErrorFilePath, compilationError);
            this.fileService.WriteToFile(resultFilePath, "CompilationError");
        }

        public void HandleRuntimeError(string codeFilePath)
        {
            var errorFilePath = this.pathService.GetErrorFilePath(codeFilePath);
            var localErrorFilePath = this.pathService.GetLocalErrorFilePath(codeFilePath);
            var resultFilePath = this.pathService.GetResultFilePath(codeFilePath);

            var runtimeError = this.fileService.ReadFromFile(errorFilePath);

            this.fileService.WriteToFile(localErrorFilePath, runtimeError);
            this.fileService.WriteToFile(resultFilePath, "RuntimeError");
        }

        public void HandleSuccessfulExecution(string codeFilePath, ExecutionResult result)
        {
            var resultFilePath = this.pathService.GetResultFilePath(codeFilePath);

            this.fileService.WriteToFile(resultFilePath, result.ToString());
        }

        public void WriteTestInputToFile(string codeFilePath, TestCase testcase)
        {
            var directory = Path.GetDirectoryName(codeFilePath);
            var inputFilePath = directory + "\\input.txt";

            this.fileService.WriteLinesToFile(inputFilePath, testcase.Input);
        }

        public bool CompareOutputs(string codeFilePath, TestCase testCase)
        {
            var directory = Path.GetDirectoryName(codeFilePath);
            var outputFilePath = directory + "\\output.txt";

            var outputContents = this.fileService.ReadLinesFromFile(outputFilePath);

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
