namespace WastelandRifleworks.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Wasteland_Rifleworks.Data;
    using WastelandRifleworks.Services.Data.Intefaces;
    using WastelandRifleworks.Web.ViewModels.Engineer;
    using WastelandRifleworks.Web.ViewModels.Home;
    using WastelandRilfeworks.Data.Models;


    public class EngineerService : IEngineerService
    {
        private readonly WastelandRifleworksDbContext dbContext;

        public EngineerService(WastelandRifleworksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> EngineerExistsByUserIdAsync(string userId)
        {
            bool result = await this.dbContext.Engineers.AnyAsync(e => e.UserId.ToString() == userId);

            return result;
        }

        public async Task Create(string userId, BecomeEngineerFormModel model)
        {
            Engineer newEnginner = new Engineer()
            {
                Username = model.Username,
                UserId = Guid.Parse(userId),
                Aprovement = 30
            };

            await dbContext.Engineers.AddAsync(newEnginner);
            await dbContext.SaveChangesAsync();
        }

        public async Task<string?> GetEnginnerIdByUserIdAsync(string userId)
        {
            Engineer? engineer = await this.dbContext
                 .Engineers
                 .FirstOrDefaultAsync(e => e.UserId.ToString() == userId);

            if (engineer == null)
            {
                return null;
            }

            return engineer.Id.ToString();
        }
    }
}
