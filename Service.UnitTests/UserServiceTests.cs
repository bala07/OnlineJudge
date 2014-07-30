using System.Collections.Generic;

using DataAccessLayer.Interfaces;

using Domain.Models;

using Moq;

using NUnit.Framework;

using OnlineJudge.Service;
using OnlineJudge.Service.Interfaces;

namespace Service.UnitTests
{
    [TestFixture]
    public class UserServiceTests
    {
        private Mock<IUserRepository> userRepositoryMock;

        private IUserService userService;

        [SetUp]
        public void InitFields()
        {
            userRepositoryMock = new Mock<IUserRepository>();

            this.userService = new UserService(userRepositoryMock.Object);
        }

        [Test]
        public void ShouldReturnUserGivenEmail()
        {
            var email = "foo@bar.com";
            var userFromDb = new User { Email = email };

            userRepositoryMock.Setup(repository => repository.Get(email)).Returns(userFromDb);

            userService.GetUser(email);

            userRepositoryMock.VerifyAll();
        }

        [Test]
        public void ShouldReturnAllUsers()
        {
            var users = new List<User>();

            userRepositoryMock.Setup(repository => repository.GetAll()).Returns(users);

            userService.GetAllUsers();

            userRepositoryMock.VerifyAll();
        }

        [Test]
        public void ShouldAddUser()
        {
            var newUser = new User { Email = "foo@bar.com" };

            userRepositoryMock.Setup(repository => repository.Create(newUser));

            userService.AddUser(newUser);

            userRepositoryMock.VerifyAll();
        }
    }
}
