using System.Collections.Generic;

using Domain.Models;

namespace DataAccessLayer.RepositoryInterfaces
{
    public interface IUserRepository
    {
        User Get(string email);

        IList<User> GetAll();

        void Insert(User user);
    }
}
