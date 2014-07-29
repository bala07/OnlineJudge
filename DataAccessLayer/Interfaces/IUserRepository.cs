using System.Collections.Generic;

using Domain.Models;

namespace DataAccessLayer.Interfaces
{
    public interface IUserRepository : IRepository
    {
        User Get(string email);

        IList<User> GetAll();

        void Create(User user);
    }
}
