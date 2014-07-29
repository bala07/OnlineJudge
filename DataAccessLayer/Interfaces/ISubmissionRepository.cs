using Domain.Models;

namespace DataAccessLayer.Interfaces
{
    public interface ISubmissionRepository
    {
        void Create(Submission submission);
    }
}
