namespace WastelandRifework.Services.Tests
{
    using Microsoft.EntityFrameworkCore;
    using Wasteland_Rifleworks.Data;
    using WastelandRifleworks.Services.Data;
    using WastelandRifleworks.Services.Data.Intefaces;

    public class Tests
    {
        public DbContextOptions<WastelandRifleworksDbContext> dbOptions;
        public WastelandRifleworksDbContext dbContext;
        public IEngineerService engineerService;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            this.dbOptions = new DbContextOptionsBuilder<WastelandRifleworksDbContext>()
                .UseInMemoryDatabase("WastelandRifleworksInMemory" + Guid.NewGuid().ToString())
                .Options;
            this.dbContext = new WastelandRifleworksDbContext(this.dbOptions);

            this.dbContext.Database.EnsureCreated();
            DatabaseSeeder.SeedDatabase(this.dbContext);

            this.engineerService = new EngineerService(this.dbContext);
        }

        [Test]
        public async Task EngineerExistsByUserIdAsync_ShouldReturnTrue_WhenEngineerExists()
        {
            string existingEngineerUserId = DatabaseSeeder.EngineerUser.Id.ToString();

            bool result = await this.engineerService.EngineerExistsByUserIdAsync(existingEngineerUserId);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task EngineerExistsByUserIdAsync_ShouldReturnFalse_WhenEngineerNotExists()
        {
            string nonExistingEngineerUserId = DatabaseSeeder.User.Id.ToString();

            bool result = await this.engineerService.EngineerExistsByUserIdAsync(nonExistingEngineerUserId);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task GetEnginnerIdByUserIdAsync_ShouldReturnId_WhenEngineerExists()
        {
            string existingEngineerUserId = DatabaseSeeder.EngineerUser.Id.ToString();

            string? result = await this.engineerService.GetEnginnerIdByUserIdAsync(existingEngineerUserId);

            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetEnginnerIdByUserIdAsync_ShouldReturnNull_WhenEngineerNotExists()
        {
            string nonExistingEngineerUserId = DatabaseSeeder.User.Id.ToString();

            string? result = await this.engineerService.GetEnginnerIdByUserIdAsync(nonExistingEngineerUserId);

            Assert.IsNull(result);
        }

        [Test]
        public async Task GetEnginnerUsernameByEnginnerIdAsync_ShouldReturnNull_WhenEngineerNotExists()
        {
            string nonExistingEngineerId = Guid.NewGuid().ToString();

            string? result = await this.engineerService.GetEnginnerUsernameByEnginnerIdAsync(nonExistingEngineerId);

            Assert.IsNull(result);
        }
    }
}