using DataAccessLayer.Interfaces;

using Domain.Models;

namespace DataAccessLayer
{
    public class SubmissionRepository : ISubmissionRepository
    {
        private readonly OnlineJudgeContext onlineJudgeContext;

        public SubmissionRepository()
        {
            this.onlineJudgeContext = new OnlineJudgeContext();
        }

        public void Create(Submission submission)
        {
            this.onlineJudgeContext.Submissions.Add(submission);
            this.onlineJudgeContext.SaveChanges();
        }
    }
}