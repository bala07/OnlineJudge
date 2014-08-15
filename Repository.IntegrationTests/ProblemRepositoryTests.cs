using System.Collections;
using System.Collections.Generic;

using DataAccessLayer;

using Domain.Models;

using NUnit.Framework;

namespace Repository.IntegrationTests
{
    [TestFixture]
    public class ProblemRepositoryTests : BaseIntegrationTestFixture
    {
        private ProblemRepository problemRepository;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            problemRepository = new ProblemRepository(this.OnlineJudgeContext);
        }

        [Test]
        public void ShouldGetProblemGivenProblemCode()
        {
            const string ProblemCode = "problemCode";
            var expectedProblem = new Problem { Code = ProblemCode };
            OnlineJudgeContext.Problems.Add(expectedProblem);
            OnlineJudgeContext.SaveChanges();

            var receivedProblem = problemRepository.Get(ProblemCode);

            Assert.NotNull(receivedProblem);
            Assert.That(receivedProblem.Code, Is.EqualTo(expectedProblem.Code));
        }

        [Test]
        public void ShouldGetAllProblems()
        {
            IList<Problem> expectedProblems = new List<Problem>
                                                  {
                                                      new Problem { Code = "Problem1" },
                                                      new Problem { Code = "Problem2" }
                                                  };
            OnlineJudgeContext.Problems.AddRange(expectedProblems);
            OnlineJudgeContext.SaveChanges();

            var receivedProblems = problemRepository.GetAll();

            Assert.That(receivedProblems.Count, Is.EqualTo(expectedProblems.Count));
            Assert.That(receivedProblems[0].Code, Is.EqualTo(expectedProblems[0].Code));
            Assert.That(receivedProblems[1].Code, Is.EqualTo(expectedProblems[1].Code));
        }
    }
}