using Domain.Models;

using Moq;

using NUnit.Framework;

using OnlineJudge.Service.CodeExecutionService;
using OnlineJudge.Service.Testers;

namespace Service.UnitTests
{
    [TestFixture]
    public class TesterTests
    {
        private Tester tester;

        private Mock<ICodeExecutionService> codeExecutionServiceMock;

        private Mock<ITesterHelper> testerHelperMock;

        [SetUp]
        public void InitFields()
        {
            codeExecutionServiceMock = new Mock<ICodeExecutionService>(MockBehavior.Strict);
            testerHelperMock = new Mock<ITesterHelper>(MockBehavior.Strict);

            tester = new Tester(codeExecutionServiceMock.Object, testerHelperMock.Object);
        }

        [Test]
        public void ShouldGetTesterResult()
        {
            const string CodeFilePath = "codeFile";
            const ExecutionResult ExpectedExecutionResult = ExecutionResult.CompilationError;

            testerHelperMock.Setup(helper => helper.GetTesterResult(CodeFilePath)).Returns(ExpectedExecutionResult);

            var receivedExecutionResult = tester.GetTesterResult(CodeFilePath);

            Assert.That(receivedExecutionResult, Is.EqualTo(ExpectedExecutionResult));
            testerHelperMock.VerifyAll();
        }

        [Test]
        public void ShouldHandleCompilationError()
        {
            const string CodeFilePath = "codeFile";
            const string ProblemCode = "problemCode";

            codeExecutionServiceMock.Setup(service => service.Compile(CodeFilePath)).Returns(false);
            testerHelperMock.Setup(helper => helper.HandleCompilationError(CodeFilePath));

            tester.Test(CodeFilePath, ProblemCode);

            codeExecutionServiceMock.VerifyAll();
            testerHelperMock.VerifyAll();
        }

        [Test]
        public void ShouldHandleRuntimeError()
        {
            const string CodeFilePath = "codeFile";
            const string ProblemCode = "problemCode";
            var testSuite = new TestSuite
                                      {
                                          TestCases = new []
                                                          {
                                                              new TestCase()
                                                          }
                                      };


            codeExecutionServiceMock.Setup(service => service.Compile(CodeFilePath)).Returns(true);
            codeExecutionServiceMock.Setup(service => service.Run(CodeFilePath, new string[] { })).Returns(false);
            testerHelperMock.Setup(helper => helper.GetTestSuite(ProblemCode)).Returns(testSuite);
            testerHelperMock.Setup(helper => helper.WriteTestInputToFile(CodeFilePath, testSuite.TestCases[0]));
            testerHelperMock.Setup(helper => helper.HandleRuntimeError(CodeFilePath));

            tester.Test(CodeFilePath, ProblemCode);

            codeExecutionServiceMock.VerifyAll();
            testerHelperMock.VerifyAll();
        }

        [Test]
        public void ShouldHandleSuccessfulExecutionWithWrongAnswerResultForSingleTestCase()
        {
            const string CodeFilePath = "codeFile";
            const string ProblemCode = "problemCode";
            var testSuite = new TestSuite
            {
                TestCases = new[]
                                                          {
                                                              new TestCase()
                                                          }
            };


            codeExecutionServiceMock.Setup(service => service.Compile(CodeFilePath)).Returns(true);
            this.testerHelperMock.Setup(helper => helper.GetTestSuite(ProblemCode)).Returns(testSuite);
            this.testerHelperMock.Setup(helper => helper.WriteTestInputToFile(CodeFilePath, testSuite.TestCases[0]));
            codeExecutionServiceMock.Setup(service => service.Run(CodeFilePath, new string[] { })).Returns(true);
            testerHelperMock.Setup(helper => helper.CompareOutputs(CodeFilePath, testSuite.TestCases[0])).Returns(false);
            testerHelperMock.Setup(
                helper => helper.HandleSuccessfulExecution(CodeFilePath, ExecutionResult.WrongAnswer));

            tester.Test(CodeFilePath, ProblemCode);

            codeExecutionServiceMock.VerifyAll();
            testerHelperMock.VerifyAll();
        }

        [Test]
        public void ShouldHandleSuccessfulExecutionWithCorrectAnswerResultForSingleTestCase()
        {
            const string CodeFilePath = "codeFile";
            const string ProblemCode = "problemCode";
            var testSuite = new TestSuite
            {
                TestCases = new[]
                                                          {
                                                              new TestCase()
                                                          }
            };


            codeExecutionServiceMock.Setup(service => service.Compile(CodeFilePath)).Returns(true);
            testerHelperMock.Setup(helper => helper.GetTestSuite(ProblemCode)).Returns(testSuite);
            testerHelperMock.Setup(helper => helper.WriteTestInputToFile(CodeFilePath, testSuite.TestCases[0]));
            codeExecutionServiceMock.Setup(service => service.Run(CodeFilePath, new string[] { })).Returns(true);
            testerHelperMock.Setup(helper => helper.CompareOutputs(CodeFilePath, testSuite.TestCases[0])).Returns(true);
            testerHelperMock.Setup(
                helper => helper.HandleSuccessfulExecution(CodeFilePath, ExecutionResult.CorrectAnswer));

            tester.Test(CodeFilePath, ProblemCode);

            codeExecutionServiceMock.VerifyAll();
            testerHelperMock.VerifyAll();
        }

        [Test]
        public void ShouldHandleRuntimeErrorEvenIfOneTestCaseThrowsRuntimeError()
        {
            const string CodeFilePath = "codeFile";
            const string ProblemCode = "problemCode";
            var testSuite = new TestSuite
            {
                TestCases = new[]
                                                          {
                                                              new TestCase(), new TestCase(), new TestCase()
                                                          }
            };

            codeExecutionServiceMock.Setup(service => service.Compile(CodeFilePath)).Returns(true);
            testerHelperMock.Setup(helper => helper.GetTestSuite(ProblemCode)).Returns(testSuite);
            testerHelperMock.Setup(helper => helper.WriteTestInputToFile(CodeFilePath, It.IsAny<TestCase>()));
            codeExecutionServiceMock.SetupSequence(service => service.Run(CodeFilePath, new string[] { }))
                .Returns(true)
                .Returns(true)
                .Returns(false);
            testerHelperMock.Setup(helper => helper.CompareOutputs(CodeFilePath, It.IsAny<TestCase>())).Returns(true);
            testerHelperMock.Setup(
                helper => helper.HandleRuntimeError(CodeFilePath));

            tester.Test(CodeFilePath, ProblemCode);

            codeExecutionServiceMock.Verify(service => service.Run(CodeFilePath, new string[] { }), Times.Exactly(3));
            codeExecutionServiceMock.VerifyAll();
            testerHelperMock.VerifyAll();
        }

        [Test]
        public void ShouldReturnWrongAnswerEvenIfOneTestCaseFails()
        {
            const string CodeFilePath = "codeFile";
            const string ProblemCode = "problemCode";
            var testSuite = new TestSuite
            {
                TestCases = new[]
                                                          {
                                                              new TestCase(), new TestCase(), new TestCase()
                                                          }
            };

            codeExecutionServiceMock.Setup(service => service.Compile(CodeFilePath)).Returns(true);
            testerHelperMock.Setup(helper => helper.GetTestSuite(ProblemCode)).Returns(testSuite);
            testerHelperMock.Setup(helper => helper.WriteTestInputToFile(CodeFilePath, It.IsAny<TestCase>()));
            codeExecutionServiceMock.Setup(service => service.Run(CodeFilePath, new string[] { })).Returns(true);
            testerHelperMock.SetupSequence(helper => helper.CompareOutputs(CodeFilePath, It.IsAny<TestCase>()))
                .Returns(true)
                .Returns(false)
                .Returns(true);
            testerHelperMock.Setup(
                helper => helper.HandleSuccessfulExecution(CodeFilePath, ExecutionResult.WrongAnswer));

            tester.Test(CodeFilePath, ProblemCode);

            codeExecutionServiceMock.Verify(service => service.Run(CodeFilePath, new string[] { }), Times.Exactly(2));
            codeExecutionServiceMock.VerifyAll();
            testerHelperMock.VerifyAll();
        }

        [Test]
        public void ShouldReturnCorrectAnswerWhenAllTestCasesPass()
        {
            const string CodeFilePath = "codeFile";
            const string ProblemCode = "problemCode";
            var testSuite = new TestSuite
            {
                TestCases = new[]
                                                          {
                                                              new TestCase(), new TestCase(), new TestCase()
                                                          }
            };

            codeExecutionServiceMock.Setup(service => service.Compile(CodeFilePath)).Returns(true);
            testerHelperMock.Setup(helper => helper.GetTestSuite(ProblemCode)).Returns(testSuite);
            testerHelperMock.Setup(helper => helper.WriteTestInputToFile(CodeFilePath, It.IsAny<TestCase>()));
            codeExecutionServiceMock.Setup(service => service.Run(CodeFilePath, new string[] { })).Returns(true);
            testerHelperMock.Setup(helper => helper.CompareOutputs(CodeFilePath, It.IsAny<TestCase>())).Returns(true);
            testerHelperMock.Setup(
                helper => helper.HandleSuccessfulExecution(CodeFilePath, ExecutionResult.CorrectAnswer));

            tester.Test(CodeFilePath, ProblemCode);

            codeExecutionServiceMock.Verify(service => service.Run(CodeFilePath, new string[] { }), Times.Exactly(3));
            codeExecutionServiceMock.VerifyAll();
            testerHelperMock.VerifyAll();
        }



    }
}
