namespace WastelandRifleworks.Services.Data
{
    using Microsoft.EntityFrameworkCore;

    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Wasteland_Rifleworks.Data;
    using WastelandRifleworks.Services.Data.Intefaces;
    using WastelandRifleworks.Web.ViewModels.Weapon;
    using WastelandRilfeworks.Data.Models;
    using WastelandRifleworks.Web.ViewModels.Weapon.Enums;
    using WastelandRifleworks.Web.ViewModels.Tag;


    public class WeaponService : IWeaponService
    {

        private readonly WastelandRifleworksDbContext dbContext;

        private readonly IEngineerService engineerService;

        private readonly ITagService tagService = null!;

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
                    .OrderByDescending(w => w.CreatedOn),
                WeaponSorting.Oldest => weaponsQuery
                    .OrderBy(w => w.CreatedOn),
                WeaponSorting.MostComplex => weaponsQuery
                    .OrderBy(w => w.Complexity),
                WeaponSorting.LeastComplex => weaponsQuery
                    .OrderByDescending(w => w.Complexity),
                WeaponSorting.TopRated => weaponsQuery
                    .OrderBy(w => w.Rating),
                WeaponSorting.LeastRated => weaponsQuery
                    .OrderByDescending(w => w.Rating),
                _ => weaponsQuery
                    .OrderBy(w => w.CreatedOn)
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
                .Where(e => e.EngineerId.ToString() == engineerId)
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

            Schematic schema = await this.dbContext
                .Schematics
                .FirstAsync(sc => sc.Id == weapon.WeaponSchematicId);

            string[] imagesPaths = await this.dbContext
                .Images
                   .Where(image => image.WeaponId == weapon.Id) // Filter by weapon ID
                     .Select(image => image.FileName)
                     .OrderBy(fileName => fileName)
                     .ToArrayAsync();

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
                SchematicPath = schema.FileName
            };

            return weaponDetailsViewModel;
        }

        public async Task InsertWeaponAsync(Weapon weapon)
        {
            await this.dbContext.Weapons.AddAsync(weapon);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task UpdateWeaponAsync(Weapon weapon)
        {
            this.dbContext.Weapons.Update(weapon);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<bool> ExistsByIdAsync(string weaponId)
        {
            bool result = await dbContext
                .Weapons
                .AnyAsync(w => w.Id.ToString() == weaponId);

            return result;
        }

        public async Task<WeaponFormModel> GetWeaponForEditbyIdAsync(string weaponId)
        {

            Weapon weapon = await dbContext
                .Weapons
                .Include(w => w.Type)
                .Include(w => w.Tags)
                .FirstAsync(w => w.Id.ToString() == weaponId);

            WeaponFormModel formModel = new WeaponFormModel
            {

                Name = weapon.Name,
                Rating = weapon.Rating,
                TypeId = weapon.Type.Id,
                Complexity = weapon.Complexity,
                ShortDescription = weapon.ShortDescription,
                FullDescription = weapon.FullDescription,
                Tags = weapon.Tags.Select(t => new WeaponTagFormModel
                {
                    Id = t.Id,
                    Name = t.Name
                }).ToList(),
                Images = weapon.Images,

            };

            return formModel;

        }

        public async Task EditWeaponByIdAndFormModelAsync(string weaponId, WeaponFormModel formModel)
        {
            Weapon weapon = await dbContext
                .Weapons
                .FirstAsync(w => w.Id.ToString() == weaponId);

            weapon.Name = formModel.Name;
            weapon.Complexity = formModel.Complexity;
            weapon.ShortDescription = formModel.ShortDescription;
            weapon.FullDescription = formModel.FullDescription;
            weapon.TypeId = formModel.TypeId;

            //weapon.Tags.Clear();
            //weapon.Images.Clear();

            //IEnumerable<WeaponTagFormModel> tagFormModels = await tagService.FiveSelectedTagsAsync(formModel.FirstTagId, formModel.SecondTagId, formModel.ThirdTagId, formModel.ForthTagId, formModel.FifthTagId);

            //foreach (var tagFormModel in tagFormModels)
            //{
            //    Tag existingTag = await dbContext.Tags.FindAsync(tagFormModel.Id);
            //    if (existingTag != null)
            //    {
            //        weapon.Tags.Add(existingTag);
            //    }
            //}

            weapon.Images = formModel.Images;


            await dbContext.SaveChangesAsync();
        }

        public async Task<bool> IsEngineertWithIdCreatorOfWeaponWithIdAsync(string weaponId, string engineerId)
        {
            Weapon weapon = await dbContext
                .Weapons
                .FirstAsync(w => w.Id.ToString() == weaponId);

            return weapon.EngineerId.ToString() == engineerId;
        }

        public async Task<WeaponPreDeleteViewModel> GetWeaponForDeleteByIdAsync(string weaponId)
        {
            Weapon weapon = await dbContext
       .Weapons
       .Include(w => w.Type)
       .Include(w => w.Tags)
       .Include(w => w.Images)
       .FirstAsync(w => w.Id.ToString() == weaponId);

            WeaponPreDeleteViewModel formModel = new WeaponPreDeleteViewModel
            {
                Name = weapon.Name,
                Rating = weapon.Rating,
                Type = weapon.Type.Name,
                Complexity = weapon.Complexity,
                ShortDescription = weapon.ShortDescription,
                FullDescription = weapon.FullDescription,
                TagNames = weapon.Tags.Select(tag => tag.Name).OrderBy(tag => tag).ToList(),
                ImagesPaths = weapon.Images.Select(image => image.FileName).OrderBy(image => image).ToList()
            };

            return formModel;

        }

        public async Task DeleteWeaponByIdAsync(string weaponId)
        {
            Weapon weaponToDie = await dbContext
                .Weapons
                .FirstAsync(h => h.Id.ToString() == weaponId);

            dbContext.Remove(weaponToDie);

            await dbContext.SaveChangesAsync();
        }

        public async Task ToggleUserReactionAsync(string engineerId, string weaponId, ReactionType reactionType)
        {
            var weapon = await dbContext.Weapons.FindAsync(int.Parse(weaponId));
            var existingReaction = await dbContext.UserReactions
                .FirstOrDefaultAsync(ur => ur.UserId == engineerId && ur.WeaponId == weaponId);

            if (existingReaction == null)
            {
                // Add new reaction
                var newUserReaction = new UserReaction { UserId = engineerId, WeaponId = weaponId, ReactionType = reactionType };
                dbContext.UserReactions.Add(newUserReaction);

                if (reactionType == ReactionType.Like)
                {
                    weapon.Rating++;
                }
                else if (reactionType == ReactionType.Dislike)
                {
                    weapon.Rating--;
                }
            }
            else
            {
                // Update existing reaction
                if (existingReaction.ReactionType == reactionType)
                {
                    dbContext.UserReactions.Remove(existingReaction);

                    if (reactionType == ReactionType.Like)
                    {
                        weapon.Rating--;
                    }
                    else if (reactionType == ReactionType.Dislike)
                    {
                        weapon.Rating++;
                    }
                }
                else
                {
                    existingReaction.ReactionType = reactionType;
                    dbContext.UserReactions.Update(existingReaction);

                    if (reactionType == ReactionType.Like)
                    {
                        weapon.Rating += 2;
                    }
                    else if (reactionType == ReactionType.Dislike)
                    {
                        weapon.Rating -= 2;
                    }
                }
            }

            await dbContext.SaveChangesAsync();
        }


    }
}


