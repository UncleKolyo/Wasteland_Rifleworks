using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Wasteland_Rifleworks.Data;
using WastelandRilfeworks.Data.Models;
using WastelandRifleworks.Services.Data;
using WastelandRifleworks.Services.Data.Intefaces;
using WastelandRifework.Services.Tests;
using WastelandRifleworks.Web.ViewModels.User;

namespace WastelandRifeworks.Services.Tests
{
    [TestFixture]
    public class UserServiceTests
    {
        private WastelandRifleworksDbContext dbContext;
        private IUserService userService;
        private IEngineerService engineerService;
        private ITagService tagService;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var options = new DbContextOptionsBuilder<WastelandRifleworksDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            this.dbContext = new WastelandRifleworksDbContext(options);

            this.engineerService = new EngineerService(this.dbContext);
            this.userService = new UserService(this.dbContext, this.engineerService);
            this.tagService = new TagService(this.dbContext);

            DatabaseSeeder.SeedDatabase(this.dbContext);
        }

        [Test]
        public async Task GetFullUsernameNameByIdAsync_ShouldReturnEngineerUsername_WhenEngineerExists()
        {
            string engineerUserId = DatabaseSeeder.EngineerUser.Id.ToString();

            string result = await this.userService.GetFullUsernameNameByIdAsync(engineerUserId);

            Assert.AreEqual(DatabaseSeeder.engineer.Username, result);
        }

        [Test]
        public async Task GetFullUsernameNameByIdAsync_ShouldReturnUserEmailPrefix_WhenEngineerNotExists()
        {
            string regularUserId = DatabaseSeeder.User.Id.ToString();

            string result = await this.userService.GetFullUsernameNameByIdAsync(regularUserId);

            string expectedUsername = DatabaseSeeder.User.Email.Split('@')[0];
            Assert.AreEqual(expectedUsername, result);
        }

        [Test]
        public async Task AllAsync_ShouldReturnAllUsersWithEngineerUsernames()
        {
            var expectedUsers = await this.dbContext.Users
                .Select(u => new UserViewModel
                {
                    Id = u.Id.ToString(),
                    Email = u.Email,
                    Username = u.UserName
                })
                .ToListAsync();

            foreach (var user in expectedUsers)
            {
                if (await this.engineerService.EngineerExistsByUserIdAsync(user.Id))
                {
                    var engineer = await this.dbContext.Engineers
                        .FirstAsync(e => e.UserId.ToString() == user.Id);
                    user.Username = engineer.Username;
                }
            }

            var actualUsersList = await this.userService.AllAsync();
            var actualUsers = actualUsersList.ToList();

            Assert.AreEqual(expectedUsers.Count, actualUsers.Count);

            for (int i = 0; i < expectedUsers.Count; i++)
            {
                AssertUserViewModelEquality(expectedUsers[i], actualUsers[i]);
            }
        }

        private void AssertUserViewModelEquality(UserViewModel expected, UserViewModel actual)
        {
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Email, actual.Email);
            Assert.AreEqual(expected.Username, actual.Username);
        }
    }
}
