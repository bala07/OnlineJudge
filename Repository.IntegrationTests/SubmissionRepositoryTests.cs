using System.Linq;

using DataAccessLayer;

using Domain.Models;

using NUnit.Framework;

namespace Repository.IntegrationTests
{
    [TestFixture]
    public class SubmissionRepositoryTests : BaseIntegrationTestFixture
    {
        private SubmissionRepository submissionRepository;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            submissionRepository = new SubmissionRepository(this.OnlineJudgeContext);
        }

        [Test]
        public void ShouldAddNewSubmission()
        {
            var  submission = new Submission { ProblemCode = "ProblemCode", UserEmail = "Email" };

            submissionRepository.Add(submission);

            Assert.That(OnlineJudgeContext.Submissions.Count(), Is.EqualTo(1));
            Assert.NotNull(OnlineJudgeContext.Submissions.FirstOrDefault(submission1 => submission1.ProblemCode == submission.ProblemCode));
            Assert.NotNull(OnlineJudgeContext.Submissions.FirstOrDefault(submission1 => submission1.UserEmail == submission.UserEmail));

        }
    }
}