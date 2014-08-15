using System.Collections.Generic;
using System.Linq;

using DataAccessLayer;

using Domain.Models;

using NUnit.Framework;

namespace Repository.IntegrationTests
{
    public class UserRepositoryTests : BaseIntegrationTestFixture
    {
        private UserRepository userRepository;

        [TestFixtureSetUp]
        public void FixtureSetUp()
        {
            userRepository = new UserRepository(this.OnlineJudgeContext);
        }

        [Test]
        public void ShouldGetUserGivenEmail()
        {
            const string Email = "email@email.com";
            var expectedUser = new User { Email = Email };
            this.OnlineJudgeContext.Users.Add(expectedUser);
            this.OnlineJudgeContext.SaveChanges();

            var receivedUser = userRepository.Get(Email);

            Assert.NotNull(receivedUser);
            Assert.That(receivedUser.Email, Is.EqualTo(expectedUser.Email));
        }

        [Test]
        public void ShouldReturnAllUsers()
        {
            var expectedUsers = new List<User> { new User { Email = "Email" }, new User { Email = "User2" } };
            OnlineJudgeContext.Users.AddRange(expectedUsers);
            OnlineJudgeContext.SaveChanges();

            var receivedUsers = userRepository.GetAll();

            Assert.That(receivedUsers.Count, Is.EqualTo(expectedUsers.Count));
            Assert.That(receivedUsers[0].Email, Is.EqualTo(expectedUsers[0].Email));
            Assert.That(receivedUsers[1].Email, Is.EqualTo(expectedUsers[1].Email));
        }

        [Test]
        public void ShouldAddUser()
        {
            var user = new User { Email = "Email"};

            userRepository.Add(user);

            Assert.That(OnlineJudgeContext.Users.Count(), Is.EqualTo(1));
            Assert.NotNull(OnlineJudgeContext.Users.FirstOrDefault(user1 => user1.Email == user.Email));
        }
    }
}