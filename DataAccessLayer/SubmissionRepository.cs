using DataAccessLayer.Interfaces;

using Domain.Models;

namespace DataAccessLayer
{
    public class SubmissionRepository : ISubmissionRepository
    {
        private readonly OnlineJudgeContext onlineJudgeContext;

        public SubmissionRepository(OnlineJudgeContext onlineJudgeContext)
        {
            this.onlineJudgeContext = onlineJudgeContext;
        }

        public void Add(Submission submission)
        {
            this.onlineJudgeContext.Submissions.Add(submission);
            this.onlineJudgeContext.SaveChanges();
        }
    }
}