using Domain.Models;

using Moq;

using NUnit.Framework;

using OnlineJudge.Service;
using OnlineJudge.Service.Interfaces;
using OnlineJudge.Service.Testers;

namespace Service.UnitTests
{
    [TestFixture]
    public class TesterServiceTests
    {
        private ITesterService testerService;

        private Mock<IFileService> fileServiceMock;

        private Mock<IPathService> pathServiceMock;

        private Mock<ITester> testerMock;

        [SetUp]
        public void InitFields()
        {
            fileServiceMock = new Mock<IFileService>();
            pathServiceMock = new Mock<IPathService>();
            testerMock = new Mock<ITester>();

            testerService = new TesterService(fileServiceMock.Object, pathServiceMock.Object, testerMock.Object);
        }

        [Test]
        public void ShouldTestCodeAndReturnCorrectAnswerInResult()
        {
            const string CodeFilePath = "codePath";
            const string ProblemCode = "problemCode";

            testerMock.Setup(service => service.GetTesterResult(CodeFilePath))
                .Returns(ExecutionResult.CorrectAnswer);
            testerMock.Setup(tester => tester.Test(CodeFilePath, ProblemCode));

            var returnedResult = testerService.TestCode(CodeFilePath, ProblemCode);

            Assert.That(returnedResult.ExecutionResult, Is.EqualTo(ExecutionResult.CorrectAnswer));
            fileServiceMock.VerifyAll();
            testerMock.VerifyAll();
        }

        [Test]
        public void ShouldTestCodeAndPopulateErrorDetailsInResultForCompilationError()
        {
            const string CodeFilePath = "codePath";
            const string ProblemCode = "problemCode";
            const string ErrorFilePath = "errorPath";
            const string CompilationErrorMessage = "compilationError";

            testerMock.Setup(service => service.GetTesterResult(CodeFilePath))
                .Returns(ExecutionResult.CompilationError);
            fileServiceMock.Setup(service => service.ReadFromFile(ErrorFilePath)).Returns(CompilationErrorMessage);
            pathServiceMock.Setup(service => service.GetErrorFilePath(CodeFilePath)).Returns(ErrorFilePath);
            testerMock.Setup(tester => tester.Test(CodeFilePath, ProblemCode));

            var returnedResult = testerService.TestCode(CodeFilePath, ProblemCode);

            Assert.That(returnedResult.ExecutionResult, Is.EqualTo(ExecutionResult.CompilationError));
            Assert.That(returnedResult.ErrorMessage, Is.EqualTo(CompilationErrorMessage));
            fileServiceMock.VerifyAll();
            pathServiceMock.VerifyAll();
            testerMock.VerifyAll();
        }

        [Test]
        public void ShouldTestCodeAndPopulateErrorDetailsInResultForRuntimeError()
        {
            const string CodeFilePath = "codePath";
            const string ProblemCode = "problemCode";
            const string ErrorFilePath = "errorPath";
            const string RuntimeErrorMessage = "runtimeError";

            testerMock.Setup(service => service.GetTesterResult(CodeFilePath))
                .Returns(ExecutionResult.RuntimeError);
            fileServiceMock.Setup(service => service.ReadFromFile(ErrorFilePath)).Returns(RuntimeErrorMessage);
            pathServiceMock.Setup(service => service.GetErrorFilePath(CodeFilePath)).Returns(ErrorFilePath);
            testerMock.Setup(tester => tester.Test(CodeFilePath, ProblemCode));

            var returnedResult = testerService.TestCode(CodeFilePath, ProblemCode);

            Assert.That(returnedResult.ExecutionResult, Is.EqualTo(ExecutionResult.RuntimeError));
            Assert.That(returnedResult.ErrorMessage, Is.EqualTo(RuntimeErrorMessage));
            fileServiceMock.VerifyAll();
            pathServiceMock.VerifyAll();
            testerMock.VerifyAll();
        }
    }
}
