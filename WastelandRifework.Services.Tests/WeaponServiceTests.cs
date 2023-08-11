
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

        //[Test]
        //public async Task AllAsync_ShouldReturnAllWeaponsFilteredAndPaged()
        //{
        //    // Arrange
        //    var queryModel = new AllWeaponsQueryModel
        //    {
        //        Type = "Assault Rifle",
        //        Tag = "Sniper Rifle",
        //        SearchTerm = "example",
        //        Sorting = WeaponSorting.MostComplex,
        //        CurrentPage = 1,
        //        WeaponPerPage = 10
        //    };

        //    // Act
        //    var result = await this.weaponService.AllAsync(queryModel, "wwwrootPath");

        //    // Assert
        //    Assert.NotNull(result);
        //    Assert.AreEqual(0, result.TotalWeaponsCount);
        //    Assert.AreEqual(0, result.Weapons.Count());

        //}

        //[Test]
        //public async Task AllByEngineerIdAsync_ShouldReturnWeaponsForEngineer()
        //{
        //    // Arrange
        //    string engineerId = DatabaseSeeder.EngineerUser.Id.ToString();

        //    // Act
        //    var result = await this.weaponService.AllByEngineerIdAsync(engineerId);

        //    // Assert
        //    Assert.NotNull(result);
        //    Assert.AreEqual(0, result.Count());

        //}

        //[Test]
        //public async Task GetDetailsByIdAsync_ShouldReturnWeaponDetails()
        //{
        //    // Arrange
        //    var weapon = dbContext.Weapons.First(); // Use the first weapon from the seeder data

        //    var weaponId = weapon.Id.ToString();

        //    // Act
        //    var result = await weaponService.GetDetailsByIdAsync(weaponId);

        //    // Assert
        //    Assert.NotNull(result);
        //    Assert.AreEqual(weapon.Name, result.Name);
        //    Assert.AreEqual(weapon.Complexity, result.Complexity);
        //    Assert.AreEqual(weapon.ShortDescription, result.ShortDescription);
        //    Assert.AreEqual(weapon.FullDescription, result.FullDescription);
        //    Assert.AreEqual(weapon.Type.Name, result.Type);
        //    Assert.AreEqual(weapon.Rating, result.Rating);
        //    Assert.AreEqual(weapon.Engineer.Username, result.Engineer);
        //    // Assert other properties and collections as needed
        //}

        //[OneTimeTearDown]
        //public void OneTimeTearDown()
        //{
        //    this.dbContext.Dispose();
        //}
    }
}