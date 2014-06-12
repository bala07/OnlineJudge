using System.Collections.Generic;
using System.IO;
using System.Web;

using Domain.Models;

using Newtonsoft.Json;

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

        public Result TestCode(string codeFilePath, string problemCode)
        {
            var tester = TesterFactory.GetTester(problemCode);
            var testSuite = this.GetTestSuite(problemCode);

            tester.Test(codeFilePath, testSuite);

            var result = new Result { 
                                        ProblemName = problemCode,
                                        Language = Utilities.GetLangaugeNameFromExtension(Path.GetExtension(codeFilePath)),
                                        ExecutionResult = fileService.GetTesterResult(codeFilePath)
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

        private TestSuite GetTestSuite(string problemCode)
        {
            var testSuitePath = HttpContext.Current.Server.MapPath("~/App_Data/TestSuites");
            testSuitePath += "/" + problemCode + "_TEST_SUITE.json";

            IList<TestCase> testCases = JsonConvert.DeserializeObject<List<TestCase>>(File.ReadAllText(testSuitePath));

            return new TestSuite { TestCases = testCases };
        }
    }
}
