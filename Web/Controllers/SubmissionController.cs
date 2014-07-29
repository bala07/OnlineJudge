using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

using Domain.Models;

using OnlineJudge.Service;
using OnlineJudge.Service.Interfaces;

namespace OnlineJudge.Web.Controllers
{
    [Authorize]
    public class SubmissionController : Controller
    {
        private readonly ITesterService testerService;
        private readonly IFileService fileService;
        private readonly ISubmissionService submissionService;

        public SubmissionController(ITesterService testerService, IFileService fileService, ISubmissionService submissionService)
        {
            this.testerService = testerService;
            this.fileService = fileService;
            this.submissionService = submissionService;
        }

        [HttpGet]
        public ActionResult Submit(string problemCode)
        {
            var problem = new Problem { Code = problemCode };
            return this.View(problem);
        }

        [HttpPost]
        public ActionResult Submit(HttpPostedFileBase file, string problemCode)
        {
            // TODO: Requires clean-up - seems convoluted

            var userDir = fileService.PrepareDirectoryForUser(this.User.Identity.Name);
            var timeStamp = DateTime.UtcNow.ToFileTime().ToString();
            var dirToSaveFile = fileService.PrepareDirectoryForCurrentSubmission(userDir, timeStamp);

            var codeFilePath = fileService.SaveUploadedFileToDisk(file, dirToSaveFile);
            if (codeFilePath == null)
            {
                throw new Exception("Invalid file uploaded");
            }

            var result = this.testerService.TestCode(codeFilePath, problemCode);

            var submission = this.PrepareNewSubmission(problemCode, this.User.Identity.Name, Path.GetFileNameWithoutExtension(codeFilePath), timeStamp, result.ExecutionResult);
            submissionService.Save(submission);

            return this.View("Result", result);
        }

        private Submission PrepareNewSubmission(string problemCode, string userEmail, string fileName, string timeStamp, ExecutionResult executionResult)
        {
            return new Submission
            {
                ProblemCode = problemCode,
                UserEmail = userEmail,
                FileName = fileName,
                SubmissionTimeStamp = timeStamp,
                Status = executionResult
            };
        }
    }
}
