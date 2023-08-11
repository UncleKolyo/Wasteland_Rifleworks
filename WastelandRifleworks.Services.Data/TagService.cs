namespace WastelandRifleworks.Services.Data
{
    using Microsoft.EntityFrameworkCore;

    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Wasteland_Rifleworks.Data;
    using WastelandRifleworks.Services.Data.Intefaces;
    using WastelandRifleworks.Web.ViewModels.Tag;

    public class TagService : ITagService
    {
        private readonly WastelandRifleworksDbContext dbContext;

        public TagService(WastelandRifleworksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<string>> AllNamesAsync()
        {
            IEnumerable<string> names = await this.dbContext
                .Tags
                .Select(t => t.Name)
                .ToArrayAsync();

            return names;
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

            List<WeaponTagFormModel> tagFormModels = await dbContext.Tags
              .Where(tag => tagIds.Contains(tag.Id)) 
                .Select(tag => new WeaponTagFormModel
        {
            Id = tag.Id,
            Name = tag.Name
        })
        .ToListAsync();

            return tagFormModels;

        }

        public async Task<int> GetFirstTagIdAsync()
        {
            var tags = await AllTagsAsync(); 
            var firstTag = tags.FirstOrDefault(); 

            if (firstTag != null)
            {
                return firstTag.Id; 
            }


            return 1; 
        }
    }
}
