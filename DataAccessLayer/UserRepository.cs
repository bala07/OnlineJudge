using System.Collections.Generic;
using System.Configuration;
using System.Linq;

using DataAccessLayer.Interfaces;

using Domain.Models;

namespace DataAccessLayer
{
    public class UserRepository : IUserRepository
    {
        private readonly OnlineJudgeContext onlineJudgeContext;

        public UserRepository(OnlineJudgeContext onlineJudgeContext)
        {
            this.onlineJudgeContext = onlineJudgeContext;
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

        public void Add(User user)
        {
            this.onlineJudgeContext.Users.Add(user);
            this.onlineJudgeContext.SaveChanges();
        }
    }
}