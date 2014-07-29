using System.Collections.Generic;

using Domain.Models;

namespace DataAccessLayer.Interfaces
{
    public interface IUserRepository
    {
        User Get(string email);

        IList<User> GetAll();

        void Create(User user);
    }
}
