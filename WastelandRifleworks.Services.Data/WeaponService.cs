namespace WastelandRifleworks.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Wasteland_Rifleworks.Data;
    using WastelandRifleworks.Services.Data.Intefaces;
    using WastelandRifleworks.Web.ViewModels.Home;
    using WastelandRifleworks.Web.ViewModels.Weapon;
    using WastelandRilfeworks.Data.Models;

    public class WeaponService : IWeaponService
    {

        private readonly WastelandRifleworksDbContext dbContext;

        public WeaponService(WastelandRifleworksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }



        public async Task InsertWeaponAsync(Weapon weapon)
        {
            await this.dbContext.Weapons.AddAsync(weapon);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<IndexViewModel>> LastTwentyWeapon(string wwwrootPath)
        {
            Console.WriteLine(Path.Combine(wwwrootPath, "Uploads", "1_1_WeaponPic.jpg"));
            IEnumerable<IndexViewModel> lastTwentyWeapons = await this.dbContext
                .Weapons
                .Include(w => w.Images)
                .Select(w => new IndexViewModel()
                {
                    Id = w.Id,
                    Name = w.Name,
                    Engineer = w.Engineer.User.UserName,
                    Rating = w.Rating,
                    Type = w.Type.Name,
                    Complexity = w.Complexity,
                    ImagesPaths = w.Images.Select(w => w.FileName).OrderBy(w => w).ToList(),
                })
                
                .OrderByDescending(w => w.Id)
                .Take(20)
                .ToListAsync();

            return lastTwentyWeapons;
        }

        public async Task UpdateWeaponAsync(Weapon weapon)
        {
            this.dbContext.Weapons.Update(weapon);
            await this.dbContext.SaveChangesAsync();
        }
    }
}
