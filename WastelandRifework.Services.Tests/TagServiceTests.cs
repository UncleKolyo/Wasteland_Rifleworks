using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Wasteland_Rifleworks.Data;
using WastelandRifework.Services.Tests;
using WastelandRifleworks.Services.Data;
using WastelandRifleworks.Services.Data.Intefaces;
using WastelandRifleworks.Web.ViewModels.Tag;

namespace WastelandRifleworks.Tests.Services
{
    [TestFixture]
    public class TagServiceTests
    {
        private DbContextOptions<WastelandRifleworksDbContext> dbOptions;
        private WastelandRifleworksDbContext dbContext;
        private ITagService tagService;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            this.dbOptions = new DbContextOptionsBuilder<WastelandRifleworksDbContext>()
                .UseInMemoryDatabase("WastelandRifleworksInMemory" + Guid.NewGuid().ToString())
                .Options;
            this.dbContext = new WastelandRifleworksDbContext(this.dbOptions);

            this.dbContext.Database.EnsureCreated();
            DatabaseSeeder.SeedDatabase(this.dbContext);

            this.tagService = new TagService(this.dbContext);
        }

        [Test]
        public async Task AllNamesAsync_ShouldReturnAllTagNames()
        {
            var expectedTagNames = dbContext.Tags.Select(t => t.Name).ToList();

            var result = await this.tagService.AllNamesAsync();
            var resultList = result.ToList();

            CollectionAssert.AreEquivalent(expectedTagNames, resultList);
        }


        [Test]
        public async Task ExistsById_ShouldReturnTrue_WhenTagExists()
        {
            var existingTagId = dbContext.Tags.First().Id;

            var result = await this.tagService.ExistsById(existingTagId);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task ExistsById_ShouldReturnFalse_WhenTagDoesNotExist()
        {
            var nonExistingTagId = dbContext.Tags.Max(t => t.Id) + 1;

            var result = await this.tagService.ExistsById(nonExistingTagId);

            Assert.IsFalse(result);
        }


        [Test]
        public async Task GetFirstTagIdAsync_ShouldReturnFirstTagId()
        {
            var expectedFirstTagId = dbContext.Tags.Min(t => t.Id);

            var result = await this.tagService.GetFirstTagIdAsync();

            Assert.AreEqual(expectedFirstTagId, result);
        }
    }
}