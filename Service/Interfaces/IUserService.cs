using System.Collections.Generic;

using Domain.Models;

namespace OnlineJudge.Service.Interfaces
{
    public interface IUserService
    {
        User GetUser(string email);

        IList<User> GetAllUsers();

        void AddUser(User user);
    }
}
