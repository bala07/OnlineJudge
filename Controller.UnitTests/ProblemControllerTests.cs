using System;
using System.Collections.Generic;
using System.Web.Mvc;

using Domain.Models;

using Moq;

using NUnit.Framework;

using OnlineJudge.Service.Interfaces;
using OnlineJudge.Web.Controllers;

namespace Controller.UnitTests
{
    [TestFixture]
    public class ProblemControllerTests
    {
        private ProblemController problemController;

        private Mock<IProblemService> problemServiceMock;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            problemServiceMock = new Mock<IProblemService>();

            problemController = new ProblemController(problemServiceMock.Object);
        }

        [Test]
        public void ShouldListAllProblems()
        {
            const string ExpectedViewName = "List";
            IList<Problem> expectedProblemList = new List<Problem>
                                            {
                                                new Problem { Code = "Problem1" },
                                                new Problem { Code = "Problem2" }
                                            };

            problemServiceMock.Setup(service => service.GetAllProblems()).Returns(expectedProblemList);

            var result = problemController.List() as ViewResult;
            var receivedProblemList = result.ViewData.Model as IList<Problem>;

            Assert.NotNull(result);
            Assert.AreEqual(result.ViewName, ExpectedViewName);
            Assert.AreEqual(receivedProblemList.Count, (expectedProblemList.Count));
            Assert.AreEqual(receivedProblemList[0].Code, expectedProblemList[0].Code);
            Assert.AreEqual(receivedProblemList[1].Code, expectedProblemList[1].Code);
            problemServiceMock.VerifyAll();
        }

        [Test]
        public void ShouldGetProblemDetails()
        {
            const string ProblemCode = "ProblemCode";
            var expectedProblem = new Problem { Code = ProblemCode };
            const string ExpectedViewName = "Problem";

            problemServiceMock.Setup(service => service.GetProblemWithStatement(ProblemCode)).Returns(expectedProblem);

            var result = problemController.GetProblemDetails(ProblemCode) as ViewResult;
            var receivedProblem = result.ViewData.Model as Problem;

            Assert.AreEqual(result.ViewName, ExpectedViewName);
            Assert.AreEqual(receivedProblem.Code, expectedProblem.Code);
        }
    }
}