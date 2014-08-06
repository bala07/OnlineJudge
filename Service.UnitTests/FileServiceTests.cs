using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

using Moq;

using NUnit.Framework;

using OnlineJudge.Service;
using OnlineJudge.Service.Interfaces;

namespace Service.UnitTests
{
    [TestFixture]
    public class FileServiceTests
    {
        private Mock<IPathService> pathServiceMock;

        private IFileService fileService;

        private const string BaseTestDirectory = "FileServiceTestsDir";

        private const string TestFile = "TestFile.txt";

        [SetUp]
        public void InitFieldsAndCreateDirectory()
        {
            pathServiceMock = new Mock<IPathService>();
            fileService = new FileService(pathServiceMock.Object);

            Directory.CreateDirectory(BaseTestDirectory);
        }

        [TearDown]
        public void CleanUp()
        {
            this.DeleteSubDirectoriesInDirectory(BaseTestDirectory);
            this.DeleteFilesInADirectory(BaseTestDirectory);

            Directory.Delete(BaseTestDirectory);
        }

        [Test]
        public void ShouldSaveFileIfFileNotEmpty()
        {
            var fileBaseMock = new Mock<HttpPostedFileBase>();
            const string UserDirectory = "userDir";
            const string FileName = "fileName";
            const string ExpectedFilePath = @"userDir\fileName";

            fileBaseMock.Setup(fileBase => fileBase.ContentLength).Returns(1);
            fileBaseMock.Setup(fileBase => fileBase.FileName).Returns(FileName);
            fileBaseMock.Setup(fileBase => fileBase.SaveAs(ExpectedFilePath));

            var receivedFilePath = fileService.SaveUploadedFileToDisk(fileBaseMock.Object, UserDirectory);

            Assert.That(receivedFilePath, Is.EqualTo(ExpectedFilePath));
            fileBaseMock.VerifyAll();
        }

        [Test]
        public void ShouldNotSaveFileIfFileIsEmpty()
        {
            var fileBaseMock = new Mock<HttpPostedFileBase>(MockBehavior.Strict);
            const string UserDirectory = "userDir";

            fileBaseMock.Setup(fileBase => fileBase.ContentLength).Returns(0);

            var receivedFilePath = fileService.SaveUploadedFileToDisk(fileBaseMock.Object, UserDirectory);

            Assert.IsNull(receivedFilePath);
            fileBaseMock.VerifyAll();   
        }

        [Test]
        public void ShouldNotSaveFileIfFileIsNull()
        {
            HttpPostedFileBase fileBase = null;
            const string UserDirectory = "userDir";

            var receivedFilePath = fileService.SaveUploadedFileToDisk(fileBase, UserDirectory);

            Assert.IsNull(receivedFilePath);
        }

        [Test]
        public void ShouldReadFromFile()
        {
            var filePath = Path.Combine(BaseTestDirectory, TestFile);
            const string ExpectedFileContent = "Hello World!";

            WriteContentToFile(filePath, ExpectedFileContent);

            var receivedFileContent = fileService.ReadFromFile(filePath).Replace(Environment.NewLine, "");

            Assert.That(receivedFileContent, Is.EqualTo(ExpectedFileContent));
        }

        [Test]
        public void ShouldWriteToFile()
        {
            var filePath = Path.Combine(BaseTestDirectory, TestFile);
            const string ContentToBeWrittenToFile = "fileContents";

            fileService.WriteToFile(filePath, ContentToBeWrittenToFile);

            var contentReadFromFile = ReadFromFile(filePath).Replace(Environment.NewLine, "");

            Assert.That(contentReadFromFile, Is.EqualTo(ContentToBeWrittenToFile));
        }

        [Test]
        public void ShouldReadLinesFromFile()
        {
            var filePath = Path.Combine(BaseTestDirectory, TestFile);
            var linesToBeWrittenToFile = new[] { "Line1", "Line2" };

            WriteLinesToFile(filePath, linesToBeWrittenToFile);

            var receivedLinesFromFile = fileService.ReadLinesFromFile(filePath);

            Assert.That(receivedLinesFromFile.Length, Is.EqualTo(linesToBeWrittenToFile.Length));
            Assert.That(receivedLinesFromFile[0], Is.EqualTo(linesToBeWrittenToFile[0]));
            Assert.That(receivedLinesFromFile[1], Is.EqualTo(linesToBeWrittenToFile[1]));
        }

        [Test]
        public void ShouldWriteLinesToFile()
        {
            var filePath = Path.Combine(BaseTestDirectory, TestFile);
            var linesToBeWrittenToFile = new [] { "Line1", "Line2" };

            fileService.WriteLinesToFile(filePath, linesToBeWrittenToFile);

            var receivedLinesFromFile = ReadLinesFromFile(filePath);

            Assert.That(receivedLinesFromFile.Length, Is.EqualTo(linesToBeWrittenToFile.Length));
            Assert.That(receivedLinesFromFile[0], Is.EqualTo(linesToBeWrittenToFile[0]));
            Assert.That(receivedLinesFromFile[1], Is.EqualTo(linesToBeWrittenToFile[1]));
        }

        [Test]
        public void ShouldPrepareDirectoryForUserIfTheDirectoryDoesNotExist()
        {
            const string Email = "userEmail";
            var expectedUserDirectoryPath = Path.Combine(BaseTestDirectory, Email);

            pathServiceMock.Setup(service => service.GetAppDataPath()).Returns(BaseTestDirectory);

            var returnedUserDirectoryPath = fileService.PrepareDirectoryForUser(Email);

            Assert.That(returnedUserDirectoryPath, Is.EqualTo(expectedUserDirectoryPath));
            Assert.True(Directory.Exists(expectedUserDirectoryPath));
            pathServiceMock.VerifyAll();
        }

        [Test]
        public void ShouldPrepareDirectoryForCurrentSubmission()
        {
            const string TimeStamp = "timeStamp";
            var expectedDirectoryPath = Path.Combine(BaseTestDirectory, TimeStamp);

            var returnedDirectoryPath = fileService.PrepareDirectoryForCurrentSubmission(BaseTestDirectory, TimeStamp);

            Assert.That(returnedDirectoryPath, Is.EqualTo(expectedDirectoryPath));
            Assert.True(Directory.Exists(expectedDirectoryPath));
        }

        private string[] ReadLinesFromFile(string filePath)
        {
            var linesFromFile = new List<string>();

            using (var fileReader = new StreamReader(filePath))
            {
                var line = fileReader.ReadLine();

                while (line != null)
                {
                    linesFromFile.Add(line);
                    line = fileReader.ReadLine();

                }
            }

            return linesFromFile.ToArray();
        }

        private void WriteLinesToFile(string filePath, string[] contentsToBeWrittenToFile)
        {
            using (var fileWriter = new StreamWriter(filePath))
            {
                foreach (var line in contentsToBeWrittenToFile)
                {
                    fileWriter.WriteLine(line);
                }
            }
        }

        private string ReadFromFile(string filePath)
        {
            string fileContents;

            using (var fileReader = new StreamReader(filePath))
            {
                fileContents = fileReader.ReadToEnd();
            }

            return fileContents;
        }

        private void WriteContentToFile(string filePath, string fileContent)
        {
            using (var fileWriter = new StreamWriter(filePath))
            {
                fileWriter.WriteLine(fileContent);
            }
        }

        private void DeleteFilesInADirectory(string directoryPath)
        {
            foreach (var filePath in Directory.EnumerateFiles(directoryPath))
            {
                File.Delete(filePath);
            }
        }

        private void DeleteSubDirectoriesInDirectory(string directoryPath)
        {
            foreach (var directory in Directory.EnumerateDirectories(directoryPath))
            {
                Directory.Delete(directory);
            }
        }
    }
}
