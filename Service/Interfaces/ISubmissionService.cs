using Domain.Models;

namespace OnlineJudge.Service.Interfaces
{
    public interface ISubmissionService
    {
        void Save(Submission submission);
    }
}
