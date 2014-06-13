using System.Collections.Generic;
using System.Linq;

using DataAccessLayer.RepositoryInterfaces;

using Domain.Models;

namespace DataAccessLayer
{
    public class UserRepository : IUserRepository
    {
        private readonly OnlineJudgeContext onlineJudgeContext;

        public UserRepository()
        {
            this.onlineJudgeContext = new OnlineJudgeContext();
        }

        public User Get(string email)
        {
            var user = this.onlineJudgeContext.Users.FirstOrDefault(u => u.Email == email);

            return user;
        }

        public IList<User> GetAll()
        {
            return this.onlineJudgeContext.Users.ToList();
        }

        public void Insert(User user)
        {
            this.onlineJudgeContext.Users.Add(user);
            this.onlineJudgeContext.SaveChanges();
        }
    }
}