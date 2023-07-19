namespace WastelandRifleworks.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Wasteland_Rifleworks.Data;
    using WastelandRifleworks.Services.Data.Intefaces;
    using WastelandRifleworks.Web.ViewModels.Home;

    public class WeaponService : IWeaponService
    {

        private readonly WastelandRifleworksDbContext dbContext;

        public WeaponService(WastelandRifleworksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IEnumerable<IndexViewModel>> LastTwentyWeapon(string wwwrootPath)
        {
            Console.WriteLine(Path.Combine(wwwrootPath, "Uploads", "1_1_WeaponPic.jpg"));
            IEnumerable<IndexViewModel> lastTwentyWeapons = await this.dbContext
                .Weapons
                .OrderByDescending(w => w.CreatedOn)
                .Take(20)
                .Select(w => new IndexViewModel()
                {
                    Id = w.Id,
                    Name = w.Name,
                    Engineer = w.Engineer.User.UserName,
                    Rating = w.Rating,
                    Type = w.Type.Name,
                    FrontImagePath = Path.Combine(wwwrootPath, "Uploads", "1_1_WeaponPic.jpg")
                    
                })
                .ToListAsync();

            return lastTwentyWeapons;
        }
    }
}
