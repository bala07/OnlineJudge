using System.Collections.Generic;

using DataAccessLayer;
using DataAccessLayer.Interfaces;

using Domain.Models;

using OnlineJudge.Service.Interfaces;

namespace OnlineJudge.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public User GetUser(string email)
        {
            return this.userRepository.Get(email);
        }

        public IList<User> GetAllUsers()
        {
            return this.userRepository.GetAll();
        }

        public void AddUser(User user)
        {
            this.userRepository.Add(user);
        }
    }
}