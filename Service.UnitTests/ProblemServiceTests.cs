using System.Collections.Generic;

using DataAccessLayer;
using DataAccessLayer.Interfaces;

using Domain.Models;

using Moq;

using NUnit.Framework;

using OnlineJudge.Service;
using OnlineJudge.Service.Interfaces;

namespace Service.UnitTests
{
    [TestFixture]
    public class ProblemServiceTests
    {
        private Mock<IProblemRepository> problemRepositoryMock;

        private Mock<IPathService> pathServiceMock;

        private ProblemService problemService;

        [SetUp]
        public void InitFields()
        {
            problemRepositoryMock = new Mock<IProblemRepository>();
            pathServiceMock = new Mock<IPathService>();

            problemService = new ProblemService(problemRepositoryMock.Object, pathServiceMock.Object);
        }

        [Test]
        public void ShouldGetProblemName()
        {
            const string ProblemCode = "problemName";
            var problemFromDb = new Problem { Code  = ProblemCode };

            problemRepositoryMock.Setup(repository => repository.Get(ProblemCode)).Returns(problemFromDb);

            var receivedProblem = problemService.GetProblem(ProblemCode);

            problemRepositoryMock.VerifyAll();
            Assert.That(receivedProblem.Code, Is.EqualTo(problemFromDb.Code));
        }

        [Test]
        public void ShouldGetAllProblems()
        {
            var problemsFromDb = new List<Problem>
                                     {
                                         new Problem { Code = "Problem1" },
                                         new Problem { Code = "Problem2" }
                                     };

            problemRepositoryMock.Setup(repository => repository.GetAll()).Returns(problemsFromDb);

            var receivedProblemsList = problemService.GetAllProblems();

            problemRepositoryMock.VerifyAll();
            Assert.That(receivedProblemsList.Count, Is.EqualTo(problemsFromDb.Count));
            Assert.That(receivedProblemsList[0].Code, Is.EqualTo("Problem1"));
            Assert.That(receivedProblemsList[1].Code, Is.EqualTo("Problem2"));
        }

        //TODO: Should test GetProblemWithStatement and GetAllProblemsWithStatement
    }
}
