namespace WastelandRifleworks.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Wasteland_Rifleworks.Data;
    using WastelandRifleworks.Services.Data.Intefaces;
    using WastelandRifleworks.Web.ViewModels.Tag;
    using WastelandRilfeworks.Data.Models;

    public class TagService : ITagService
    {
        private readonly WastelandRifleworksDbContext dbContext;

        public TagService(WastelandRifleworksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IEnumerable<WeaponTagFormModel>> AllTagsAsync()
        {
            IEnumerable<WeaponTagFormModel> allTags = await dbContext
                .Tags
                .AsNoTracking()
                .Select(t => new WeaponTagFormModel()
                {
                    Id = t.Id,
                    Name = t.Name,
                })
                .ToArrayAsync();

            return allTags;
        }

        public async Task<bool> ExistsById(int Id)
        {
            bool result = await this.dbContext
                .Types
                .AnyAsync(t => t.Id == Id);

            return result;
        }

        public async Task<IEnumerable<WeaponTagFormModel>> FiveSelectedTagsAsync(int FirstId, int SecondId, int ThirdId, int ForthId, int FifthId)
        {
            var tagIds = new List<int> { FirstId, SecondId, ThirdId, ForthId, FifthId};

            IEnumerable<WeaponTagFormModel> fiveTags = await dbContext
                .Tags
                .AsNoTracking()
                .Select(t => new WeaponTagFormModel()
                {
                    Id = t.Id,
                    Name = t.Name,
                })
                .Where(t => tagIds.Contains(t.Id))
                .ToArrayAsync();

            return fiveTags;
        }
    }
}
