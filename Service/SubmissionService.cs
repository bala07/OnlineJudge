using DataAccessLayer;
using DataAccessLayer.Interfaces;

using Domain.Models;

using OnlineJudge.Service.Interfaces;

namespace OnlineJudge.Service
{
    public class SubmissionService : ISubmissionService
    {
        private readonly ISubmissionRepository submissionRepository;

        public SubmissionService()
        {
            this.submissionRepository = new SubmissionRepository();
        }

        public void Save(Submission submission)
        {
            this.submissionRepository.Create(submission);    
        }
    }
}