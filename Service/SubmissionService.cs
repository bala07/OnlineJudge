using DataAccessLayer;
using DataAccessLayer.Interfaces;

using Domain.Models;

using OnlineJudge.Service.Interfaces;

namespace OnlineJudge.Service
{
    public class SubmissionService : ISubmissionService
    {
        private readonly ISubmissionRepository submissionRepository;

        public SubmissionService(ISubmissionRepository submissionRepository)
        {
            this.submissionRepository = submissionRepository;
        }

        public void AddSubmission(Submission submission)
        {
            this.submissionRepository.Add(submission);    
        }
    }
}