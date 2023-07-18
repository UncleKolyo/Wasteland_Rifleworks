namespace WastelandRifleworks.Services.Data
{
	using Microsoft.EntityFrameworkCore;
	using System.Collections.Generic;
    using System.Threading.Tasks;
    using Wasteland_Rifleworks.Data;
    using WastelandRifleworks.Services.Data.Intefaces;
    using WastelandRifleworks.Web.ViewModels.Home;

    internal class WeaponService : IWeaponService
    {

        private readonly WastelandRifleworksDbContext dbContext;

        public WeaponService(WastelandRifleworksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IEnumerable<IndexViewModel>> LastTwentyWeapon()
        {

            IEnumerable<IndexViewModel> lastTwentyWeapons = this.dbContext
                .Weapons
                .OrderByDescending(w => w.CreatedOn)
                .Take(20)
                .Select(w => new IndexViewModel()
                {
                    Id = w.Id,
                    Name = w.Name,
                    TitleImagePathString = Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"), $"{w.Id}*")[0]
                })
				.ToArray();
            return lastTwentyWeapons;
        }
    }
}
