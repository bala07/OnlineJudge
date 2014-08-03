using System.IO;

using Domain.Models;

using Moq;

using NUnit.Framework;

using OnlineJudge.Service.Interfaces;
using OnlineJudge.Service.Testers;

namespace Service.UnitTests
{
    [TestFixture]
    public class TesterHelperTests
    {
        private Mock<IFileService> fileServiceMock;

        private Mock<IPathService> pathServiceMock;

        private ITesterHelper testerHelper;

        [SetUp]
        public void InitFields()
        {
            fileServiceMock = new Mock<IFileService>();
            pathServiceMock = new Mock<IPathService>();

            testerHelper = new TesterHelper(fileServiceMock.Object, pathServiceMock.Object);
        }

        [Test]
        public void ShouldHandleCompilationError()
        {
            const string CodeFilePath = "codeFilePath";
            const string ErrorFilePath = "errorFilePath";
            const string LocalErrorFilePath = "localErrorFilePath";
            const string ResultFilePath = "resultFilePath";
            const string CompilationError = "compilationError";

            pathServiceMock.Setup(service => service.GetErrorFilePath(CodeFilePath)).Returns(ErrorFilePath);
            pathServiceMock.Setup(service => service.GetLocalErrorFilePath(CodeFilePath)).Returns(LocalErrorFilePath);
            pathServiceMock.Setup(service => service.GetResultFilePath(CodeFilePath)).Returns(ResultFilePath);
            fileServiceMock.Setup(service => service.ReadFromFile(ErrorFilePath)).Returns(CompilationError);
            fileServiceMock.Setup(service => service.WriteToFile(LocalErrorFilePath, CompilationError));
            fileServiceMock.Setup(service => service.WriteToFile(ResultFilePath, "CompilationError"));

            testerHelper.HandleCompilationError(CodeFilePath);

            pathServiceMock.VerifyAll();
            fileServiceMock.VerifyAll();
        }

        [Test]
        public void ShouldHandleRuntimeError()
        {
            const string CodeFilePath = "codeFilePath";
            const string ErrorFilePath = "errorFilePath";
            const string LocalErrorFilePath = "localErrorFilePath";
            const string ResultFilePath = "resultFilePath";
            const string RuntimeError = "runtimeError";

            pathServiceMock.Setup(service => service.GetErrorFilePath(CodeFilePath)).Returns(ErrorFilePath);
            pathServiceMock.Setup(service => service.GetLocalErrorFilePath(CodeFilePath)).Returns(LocalErrorFilePath);
            pathServiceMock.Setup(service => service.GetResultFilePath(CodeFilePath)).Returns(ResultFilePath);
            fileServiceMock.Setup(service => service.ReadFromFile(ErrorFilePath)).Returns(RuntimeError);
            fileServiceMock.Setup(service => service.WriteToFile(LocalErrorFilePath, RuntimeError));
            fileServiceMock.Setup(service => service.WriteToFile(ResultFilePath, "RuntimeError"));

            testerHelper.HandleRuntimeError(CodeFilePath);

            pathServiceMock.VerifyAll();
            fileServiceMock.VerifyAll();
        }

        [Test]
        public void ShouldHandleSuccessfulExecution()
        {
            const string CodeFilePath = "codeFilePath";
            const ExecutionResult ExecutionResult = ExecutionResult.WrongAnswer;
            const string ResultFilePath = "resultFilePath";

            pathServiceMock.Setup(service => service.GetResultFilePath(CodeFilePath)).Returns(ResultFilePath);
            fileServiceMock.Setup(service => service.WriteToFile(ResultFilePath, ExecutionResult.ToString()));
            
            testerHelper.HandleSuccessfulExecution(CodeFilePath, ExecutionResult);

            pathServiceMock.VerifyAll();
            fileServiceMock.VerifyAll();
        }

        [Test]
        public void ShouldWriteTestInputToFile()
        {
            const string CodeFilePath = @"someDir\someCode.cs";
            var testCase = new TestCase { Input = new []{ "input1"} };
            var expectedInputFilePath = Path.GetDirectoryName(CodeFilePath) + "\\input.txt";

            fileServiceMock.Setup(service => service.WriteLinesToFile(expectedInputFilePath, testCase.Input));

            testerHelper.WriteTestInputToFile(CodeFilePath, testCase);

            fileServiceMock.VerifyAll();
        }

        [Test]
        public void ShouldReturnTrueWhenOutputsMatch()
        {
            const string CodeFilePath = @"someDir\someCode.cs";
            var testcase = new TestCase { Output = new []{ "output1", "output2" } };
            var expectedOutputFilePath = Path.GetDirectoryName(CodeFilePath) + "\\output.txt";
            var receivedOutputs = new []{ "output1", "output2" };

            fileServiceMock.Setup(service => service.ReadLinesFromFile(expectedOutputFilePath)).Returns(receivedOutputs);

            var receivedResult = testerHelper.CompareOutputs(CodeFilePath, testcase);

            Assert.True(receivedResult);
            fileServiceMock.VerifyAll();
        }

        [Test]
        public void ShouldReturnFalseWhenOutputsDoNotMatch()
        {
            const string CodeFilePath = @"someDir\someCode.cs";
            var testcase = new TestCase { Output = new[] { "output1", "output2" } };
            var expectedOutputFilePath = Path.GetDirectoryName(CodeFilePath) + "\\output.txt";
            var receivedOutputs = new[] { "output3", "output2" };

            fileServiceMock.Setup(service => service.ReadLinesFromFile(expectedOutputFilePath)).Returns(receivedOutputs);

            var receivedResult = testerHelper.CompareOutputs(CodeFilePath, testcase);

            Assert.False(receivedResult);
            fileServiceMock.VerifyAll();
        }
    }
}
