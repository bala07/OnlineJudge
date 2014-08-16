using System.IO;
using System.Web;
using System.Web.Mvc;

using Domain.Models;

using Moq;

using NUnit.Framework;

using OnlineJudge.Service.Interfaces;
using OnlineJudge.Web.Controllers;
using OnlineJudge.Web.Exceptions;

namespace Controller.UnitTests
{
    [TestFixture]
    public class SubmissionControllerTests
    {
        private SubmissionController submissionController;

        private Mock<ITesterService> testerServiceMock;

        private Mock<IFileService> fileServiceMock;

        private Mock<ISubmissionService> submissionServiceMock;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            testerServiceMock = new Mock<ITesterService>();
            fileServiceMock = new Mock<IFileService>();
            submissionServiceMock = new Mock<ISubmissionService>();

            submissionController = new SubmissionController(testerServiceMock.Object, fileServiceMock.Object, submissionServiceMock.Object);
        }

        [Test]
        public void ShouldReturnSubmitViewWithProblemDetails()
        {
            const string ProblemCode = "ProblemCode";
            const string ExpectedViewName = "Submit";

            var result = submissionController.Submit(ProblemCode) as ViewResult;
            var receivedProblem = result.ViewData.Model as Problem;

            Assert.AreEqual(ExpectedViewName, result.ViewName);
            Assert.AreEqual(ProblemCode, receivedProblem.Code);
        }

        [Test]
        public void ShouldThrowExceptionForInvalidFileSubmission()
        {
            const string ProblemCode = "ProblemCode";
            const string ExpectedMessage = "Invalid file uploaded";
            const string UserDir = "userDir";
            const string TimeStamp = "timeStamp";
            const string ExpectedUserEmail = "userEmail";
            HttpPostedFileBase file = null;
            var dirToSaveFile = Path.Combine(UserDir, TimeStamp);

            var controllerContextMock = this.PrepareControllerContextMock(ExpectedUserEmail);
            this.submissionController.ControllerContext = controllerContextMock.Object;

            fileServiceMock.Setup(service => service.PrepareDirectoryForUser(It.IsAny<string>())).Returns(UserDir);
            fileServiceMock.Setup(service => service.PrepareDirectoryForCurrentSubmission(UserDir, It.IsAny<string>()))
                .Returns(dirToSaveFile);
            fileServiceMock.Setup(service => service.SaveUploadedFileToDisk(file, dirToSaveFile)).Returns((string)null);

            var thrownException = Assert.Throws<InvalidFileException>(() => submissionController.Submit(file, ProblemCode));

            Assert.That(thrownException.Message, Is.EqualTo(ExpectedMessage));
            fileServiceMock.VerifyAll();
        }

        [Test]
        public void ShouldSaveSubmissionForValidFiles()
        {
            const string ProblemCode = "problemCode";
            const string UserDir = "userDir";
            const string TimeStamp = "timeStamp";
            const string CodeFilePath = "codeFilePath";
            const string ExpectedUserEmail = "userEmail";
            var httpPostedFileMock = new Mock<HttpPostedFileBase>();
            var dirToSaveFile = Path.Combine(UserDir, TimeStamp);
            const ExecutionResult ExecutionResult = ExecutionResult.WrongAnswer;
            var result = new Result { ProblemName = ProblemCode, ExecutionResult = ExecutionResult };

            var controllerContextMock = this.PrepareControllerContextMock(ExpectedUserEmail);
            this.submissionController.ControllerContext = controllerContextMock.Object;

            fileServiceMock.Setup(service => service.PrepareDirectoryForUser(It.IsAny<string>())).Returns(UserDir);
            fileServiceMock.Setup(service => service.PrepareDirectoryForCurrentSubmission(UserDir, It.IsAny<string>()))
                .Returns(dirToSaveFile);
            fileServiceMock.Setup(service => service.SaveUploadedFileToDisk(httpPostedFileMock.Object, dirToSaveFile)).Returns(CodeFilePath);
            testerServiceMock.Setup(service => service.TestCode(CodeFilePath, ProblemCode)).Returns(result);
            submissionServiceMock.Setup(
                service => service.AddSubmission(It.Is<Submission>(submission =>
                        this.CheckSubmissionDetails(submission, ProblemCode, CodeFilePath, ExecutionResult, ExpectedUserEmail)
                    )));

            submissionController.Submit(httpPostedFileMock.Object, ProblemCode);

            fileServiceMock.VerifyAll();
            submissionServiceMock.VerifyAll();
        }

        [Test]
        public void ShouldReturnResultViewOnSuccessfulEvaluationOfCode()
        {
            const string ProblemCode = "problemCode";
            const string UserDir = "userDir";
            const string TimeStamp = "timeStamp";
            const string CodeFilePath = "codeFilePath";
            const string ExpectedUserEmail = "userEmail";
            const string ExpectedViewName = "Result";
            var httpPostedFileMock = new Mock<HttpPostedFileBase>();
            var dirToSaveFile = Path.Combine(UserDir, TimeStamp);
            const ExecutionResult ExecutionResult = ExecutionResult.WrongAnswer;
            var result = new Result { ProblemName = ProblemCode, ExecutionResult = ExecutionResult };

            var controllerContextMock = this.PrepareControllerContextMock(ExpectedUserEmail);
            this.submissionController.ControllerContext = controllerContextMock.Object;

            fileServiceMock.Setup(service => service.PrepareDirectoryForUser(It.IsAny<string>())).Returns(UserDir);
            fileServiceMock.Setup(service => service.PrepareDirectoryForCurrentSubmission(UserDir, It.IsAny<string>()))
                .Returns(dirToSaveFile);
            fileServiceMock.Setup(service => service.SaveUploadedFileToDisk(httpPostedFileMock.Object, dirToSaveFile)).Returns(CodeFilePath);
            testerServiceMock.Setup(service => service.TestCode(CodeFilePath, ProblemCode)).Returns(result);
            submissionServiceMock.Setup(service => service.AddSubmission(It.IsAny<Submission>()));

            var viewResult = submissionController.Submit(httpPostedFileMock.Object, ProblemCode) as ViewResult;
            var receivedResult = viewResult.ViewData.Model as Result;

            Assert.That(viewResult.ViewName, Is.EqualTo(ExpectedViewName));
            Assert.That(receivedResult.ProblemName, Is.EqualTo(result.ProblemName));
            Assert.That(receivedResult.ExecutionResult, Is.EqualTo(result.ExecutionResult));
        }

        private bool CheckSubmissionDetails(Submission submission, string problemCode, string fileName, ExecutionResult status, string userEmail)
        {
            //TODO: Unable to check timestamp values
            Assert.AreEqual(problemCode, submission.ProblemCode);
            Assert.AreEqual(fileName, submission.FileName);
            Assert.AreEqual(status, submission.Status);
            Assert.AreEqual(userEmail, submission.UserEmail);

            return true;
        }

        private Mock<ControllerContext> PrepareControllerContextMock(string userEmail)
        {
            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.SetupGet(context => context.HttpContext.User.Identity.Name).Returns(userEmail);
            controllerContextMock.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
            return controllerContextMock;
        }
    }
}
