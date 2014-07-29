using Domain.Models;

namespace OnlineJudge.Service.Interfaces
{
    public interface ISubmissionService : IService
    {
        void Save(Submission submission);
    }
}
