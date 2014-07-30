using NUnit.Framework;

using OnlineJudge.Service;
using OnlineJudge.Service.Interfaces;

namespace Service.UnitTests
{
    [TestFixture]
    public class PathServiceTests
    {
        private IPathService pathService;

        [SetUp]
        public void initFields()
        {
            this.pathService = new PathService();
        }

        [Test]
        public void ShouldReturnResultFilePath()
        {
            const string CodeFilePath = @"someDir\someFile.cs";
            const string ExpectedResultFilePath = @"someDir\someFile_result.txt";

            var receivedResultFilePath = pathService.GetResultFilePath(CodeFilePath);

            Assert.That(receivedResultFilePath, Is.EqualTo(ExpectedResultFilePath));
        }

        [Test]
        public void ShouldReturnErrorFilePath()
        {
            const string CodeFilePath = @"someDir\someFile.cs";
            const string ExpectedTesterFilePath = @"someDir\someFile_error.txt";

            var receivedErrorFilePath = pathService.GetErrorFilePath(CodeFilePath);

            Assert.That(receivedErrorFilePath, Is.EqualTo(ExpectedTesterFilePath));
        }

        // TODO: Should add test for getting AppData path
    }
}
