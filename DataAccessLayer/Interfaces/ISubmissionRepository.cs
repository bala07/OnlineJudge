using Domain.Models;

namespace DataAccessLayer.Interfaces
{
    public interface ISubmissionRepository : IRepository
    {
        void Create(Submission submission);
    }
}
