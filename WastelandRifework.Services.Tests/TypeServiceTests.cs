using Microsoft.EntityFrameworkCore;

using Wasteland_Rifleworks.Data;
using WastelandRifework.Services.Tests;
using WastelandRifleworks.Services.Data;
using WastelandRifleworks.Services.Data.Intefaces;
using WastelandRifleworks.Web.ViewModels.Type;

namespace YourNamespace.Tests
{
    [TestFixture]
    public class TypeServiceTests
    {
        private WastelandRifleworksDbContext dbContext;
        private ITypeService typeService;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<WastelandRifleworksDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            this.dbContext = new WastelandRifleworksDbContext(options);
            this.typeService = new TypeService(dbContext);

            DatabaseSeeder.SeedDatabase(dbContext);
        }

        [Test]
        public async Task AllNamesAsync_ShouldReturnAllTypeNames()
        {
            var expectedNames = dbContext.Types.Select(t => t.Name).ToList();

            var actualNames = await typeService.AllNamesAsync();

            CollectionAssert.AreEqual(expectedNames, actualNames);
        }

        [Test]
        public async Task ExistsById_ShouldReturnTrue_WhenTypeExists()
        {
            int typeId = dbContext.Types.First().Id;

            bool exists = await typeService.ExistsById(typeId);

            Assert.IsTrue(exists);
        }

        [Test]
        public async Task ExistsById_ShouldReturnFalse_WhenTypeDoesNotExist()
        {
            bool exists = await typeService.ExistsById(-1);

            Assert.IsFalse(exists);
        }

      
    }
}