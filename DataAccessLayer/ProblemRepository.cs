using System.Collections.Generic;
using System.Linq;

using DataAccessLayer.RepositoryInterfaces;

using Domain.Models;

namespace DataAccessLayer
{
    public class ProblemRepository : IProblemRepository
    {

        public ProblemRepository()
        {
        }

        public Problem Get(string name)
        {
            return null;
        }

        public IList<Problem> GetAll()
        {
            return null;
        }
    }
}