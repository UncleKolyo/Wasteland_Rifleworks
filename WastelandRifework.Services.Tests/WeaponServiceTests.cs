
using Microsoft.EntityFrameworkCore;
using Wasteland_Rifleworks.Data;
using WastelandRifleworks.Services.Data;
using WastelandRifleworks.Services.Data.Intefaces;
using WastelandRifework.Services.Tests;
using WastelandRifleworks.Web.ViewModels.Weapon;
using WastelandRifleworks.Web.ViewModels.Weapon.Enums;
using WastelandRilfeworks.Data.Models;

namespace WastelandRifeworks.Services.Tests
{
    [TestFixture]
    public class WeaponServiceTests
    {
        private WastelandRifleworksDbContext dbContext;
        private IWeaponService weaponService;
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
            this.tagService = new TagService(this.dbContext);
            this.weaponService = new WeaponService(this.dbContext, this.engineerService);

            DatabaseSeeder.SeedDatabase(this.dbContext);
        }

        [Test]
        public async Task AllByEngineerIdAsync_WithValidEngineerId_ShouldReturnCorrectWeapons()
        {
            // Arrange
            var engineerId = Guid.NewGuid().ToString();
            var engineer = new Engineer { Id = Guid.Parse(engineerId), Username = "TestEngineer" };
            var weapon1 = new Weapon { Id = 1, Name = "Weapon1", EngineerId = Guid.Parse(engineerId) };
            var weapon2 = new Weapon { Id = 2, Name = "Weapon2", EngineerId = Guid.Parse(engineerId) };
            dbContext.Engineers.Add(engineer);
            dbContext.Weapons.Add(weapon1);
            dbContext.Weapons.Add(weapon2);
            await dbContext.SaveChangesAsync();

            // Act
            var result = await weaponService.AllByEngineerIdAsync(engineerId);

            // Assert
            Assert.IsNotNull(result);
            var weaponViewModels = result.ToList();
            Assert.AreEqual(2, weaponViewModels.Count);
            Assert.AreEqual(weapon1.Name, weaponViewModels[0].Name);
            Assert.AreEqual(weapon2.Name, weaponViewModels[1].Name);

           
        }
    }
}