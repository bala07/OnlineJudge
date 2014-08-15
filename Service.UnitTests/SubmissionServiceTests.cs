using DataAccessLayer.Interfaces;

using Domain.Models;

using Moq;

using NUnit.Framework;

using OnlineJudge.Service;
using OnlineJudge.Service.Interfaces;

namespace Service.UnitTests
{
    [TestFixture]
    public class SubmissionServiceTests
    {

        private Mock<ISubmissionRepository> SubmissionRepositoryMock;

        private ISubmissionService SubmissionService;

        [SetUp]
        public void InitFields()
        {
            SubmissionRepositoryMock = new Mock<ISubmissionRepository>();

            SubmissionService = new SubmissionService(SubmissionRepositoryMock.Object);
        }

        [Test]
        public void ShouldAddNewSubmission()
        {
            var submission = new Submission();

            SubmissionRepositoryMock.Setup(repository => repository.Add(submission));

            SubmissionService.AddSubmission(submission);

            SubmissionRepositoryMock.VerifyAll();
        }
    }
}
