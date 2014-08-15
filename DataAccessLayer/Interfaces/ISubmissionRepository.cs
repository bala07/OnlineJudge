using Domain.Models;

namespace DataAccessLayer.Interfaces
{
    public interface ISubmissionRepository : IRepository
    {
        void Add(Submission submission);
    }
}
