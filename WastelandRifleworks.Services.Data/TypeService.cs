namespace WastelandRifleworks.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Wasteland_Rifleworks.Data;
    using WastelandRifleworks.Services.Data.Intefaces;
    using WastelandRifleworks.Web.ViewModels.Type;
    using WastelandRifleworks.Web.ViewModels.Weapon;


    public class TypeService : ITypeService
    {
        private readonly WastelandRifleworksDbContext dbContext;

        public TypeService(WastelandRifleworksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<string>> AllNamesAsync()
        {
            IEnumerable<string> names = await this.dbContext
                .Types
                .Select(t => t.Name)
                .ToArrayAsync();

            return names;
        }

        public async Task<IEnumerable<WeaponTypeFormModel>> AllTypesAsync()
        {
            IEnumerable<WeaponTypeFormModel> allTypes = await dbContext
                .Types
                .AsNoTracking()
                .Select(t => new WeaponTypeFormModel()
                {
                    Id = t.Id,
                    Name = t.Name,
                })
                .ToArrayAsync();

            return allTypes;
        }

        public async Task<bool> ExistsById(int Id)
        {
            bool result = await this.dbContext
                .Types
                .AnyAsync(t => t.Id == Id);

            return result;
        }
    }
}
