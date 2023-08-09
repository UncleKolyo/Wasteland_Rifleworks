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
    using static WastelandRilfeworks.Common.NotificationMessagesConstants;
    using WastelandRifleworks.Web.ViewModels.Weapon.Enums;
    using System.Linq;

    public class WeaponService : IWeaponService
    {

        private readonly WastelandRifleworksDbContext dbContext;

        private readonly IEngineerService engineerService;

        public WeaponService(WastelandRifleworksDbContext dbContext, IEngineerService engineerService)
        {
            this.dbContext = dbContext;
            this.engineerService = engineerService;
        }

        public async Task<AllWeaponsFilteredAndPagedServiceModel> AllAsync(AllWeaponsQueryModel queryModel, string wwwrootPath)
        {
            IQueryable<Weapon> weaponsQuery = this.dbContext
                 .Weapons
                 .AsQueryable();

            if (!string.IsNullOrWhiteSpace(queryModel.Type))
            {
                weaponsQuery = weaponsQuery
                    .Where(w => w.Type.Name == queryModel.Type);
            }

            if (!string.IsNullOrWhiteSpace(queryModel.Tag))
            {
                weaponsQuery = weaponsQuery
                    .Where(w => w.Tags.Any(t => t.Name == queryModel.Tag));
            }

            if (!string.IsNullOrWhiteSpace(queryModel.SearchTerm))
            {
                string wildCard = $"%{queryModel.SearchTerm.ToLower()}%";

                weaponsQuery = weaponsQuery
                    .Where(w => EF.Functions.Like(w.Name, wildCard) ||
                                EF.Functions.Like(w.ShortDescription, wildCard));
            }

            weaponsQuery = queryModel.Sorting switch
            {
                WeaponSorting.Newest => weaponsQuery
                    .OrderByDescending(h => h.CreatedOn),
                WeaponSorting.Oldest => weaponsQuery
                    .OrderBy(h => h.CreatedOn),
                WeaponSorting.MostComplex => weaponsQuery
                    .OrderBy(h => h.Complexity),
                WeaponSorting.LeastComplex => weaponsQuery
                    .OrderByDescending(h => h.Complexity),
                WeaponSorting.TopRated => weaponsQuery
                    .OrderBy(h => h.Rating),
                WeaponSorting.LeastRated => weaponsQuery
                    .OrderByDescending(h => h.Rating),
                _ => weaponsQuery
                    .OrderBy(h => h.CreatedOn)
            };

            IEnumerable<AllWeaponViewModel> allweapons = await weaponsQuery
               .Skip((queryModel.CurrentPage - 1) * queryModel.WeaponPerPage)
               .Take(queryModel.WeaponPerPage)
               .Select(w => new AllWeaponViewModel
               {
                   Id = w.Id,
                   Name = w.Name,
                   Engineer = w.Engineer.Username,
                   Rating = w.Rating,
                   Type = w.Type.Name,
                   Complexity = w.Complexity,
                   Description = w.ShortDescription,
                   TagNames = w.Tags.Select(w => w.Name).OrderBy(w => w).ToList(),
                   ImagesPaths = w.Images.Select(w => w.FileName).OrderBy(w => w).ToList(),

               })
               .ToArrayAsync();

            int totalWeapons = weaponsQuery.Count();

            return new AllWeaponsFilteredAndPagedServiceModel()
            {
                TotalWeaponsCount = totalWeapons,
                Weapons = allweapons
            };
        }

        public async Task<IEnumerable<AllWeaponViewModel>> AllByEngineerIdAsync(string engineerId)
        {
            IEnumerable<AllWeaponViewModel> allWeaponsByEngineer = await dbContext
                .Weapons
                .Where(h => h.EngineerId.ToString() == engineerId)
                .Select(w => new AllWeaponViewModel
                {
                    Id = w.Id,
                    Name = w.Name,
                    Engineer = w.Engineer.Username,
                    Rating = w.Rating,
                    Type = w.Type.Name,
                    Complexity = w.Complexity,
                    Description = w.ShortDescription,
                    TagNames = w.Tags.Select(w => w.Name).OrderBy(w => w).ToList(),
                    ImagesPaths = w.Images.Select(w => w.FileName).OrderBy(w => w).ToList(),
                })
                .ToArrayAsync();

            return allWeaponsByEngineer;
        }

        public async Task<WeaponDetailsViewModel> GetDetailsByIdAsync(string weaponId)
        {
            Weapon weapon = await this.dbContext
                .Weapons
                .FirstAsync(w => w.Id.ToString() == weaponId);

            Engineer engineer = await this.dbContext
                .Engineers
                .FirstAsync(e => e.EngineeredWeapons.Contains(weapon));

            Type type = await this.dbContext
                .Types
                .FirstAsync(t => t.Weapons.Contains(weapon));

            Schematic chema = await this.dbContext
                .Schematics
                .FirstAsync(sc => sc.Id == weapon.WeaponSchematicId);

            string[] imagesPaths = await this.dbContext
                .Images
                .Select(w => w.FileName).OrderBy(w => w).ToArrayAsync();

            List<string> tagNames = await this.dbContext
                .Tags
                .Where(w => w.Weapons.Contains(weapon))
                .Select(w => w.Name)
                .OrderBy(w => w)
                .ToListAsync();

            WeaponDetailsViewModel weaponDetailsViewModel = new WeaponDetailsViewModel
            {
                Id = weapon.Id,
                Name = weapon.Name,
                Engineer = engineer.Username,
                Rating = weapon.Rating,
                Type = type.Name,
                Complexity = weapon.Complexity,
                ShortDescription = weapon.ShortDescription,
                FullDescription = weapon.FullDescription,
                TagNames = tagNames,
                ImagesPaths = imagesPaths,
                SchematicPath = chema.FileName
            };

            return weaponDetailsViewModel;
        }

        public async Task InsertWeaponAsync(Weapon weapon)
        {
            await this.dbContext.Weapons.AddAsync(weapon);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<AllWeaponViewModel>> LastTwentyWeapon(string wwwrootPath)
        {


            Console.WriteLine(Path.Combine(wwwrootPath, "Uploads", "1_1_WeaponPic.jpg"));
            IEnumerable<AllWeaponViewModel> lastTwentyWeapons = await this.dbContext
                .Weapons
                .Include(w => w.Images)
                .Select(w => new AllWeaponViewModel()
                {
                    Id = w.Id,
                    Name = w.Name,
                    Engineer = w.Engineer.Username,
                    Rating = w.Rating,
                    Type = w.Type.Name,
                    Complexity = w.Complexity,
                    Description = w.ShortDescription,
                    TagNames = w.Tags.Select(w => w.Name).OrderBy(w => w).ToList(),
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
