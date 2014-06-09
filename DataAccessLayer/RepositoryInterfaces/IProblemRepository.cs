using System.Collections;
using System.Collections.Generic;

using Domain.Models;

namespace DataAccessLayer.RepositoryInterfaces
{
    public interface IProblemRepository
    {
        Problem Get(int id);

        IList<Problem> GetAll();
    }
}

