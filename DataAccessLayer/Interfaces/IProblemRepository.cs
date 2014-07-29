using System.Collections.Generic;

using Domain.Models;

namespace DataAccessLayer.Interfaces
{
    public interface IProblemRepository : IRepository
    {
        Problem Get(string problemCode);

        IList<Problem> GetAll();
    }
}

