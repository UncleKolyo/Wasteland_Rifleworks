namespace WastelandRifleworks.Services.Data.Intefaces
{
    using System.Collections.Generic;
    using WastelandRifleworks.Web.ViewModels.Type;
    using WastelandRifleworks.Web.ViewModels.Weapon;

    public interface ITypeService
    {
        Task<IEnumerable<WeaponTypeFormModel>> AllTypesAsync();

        Task<bool> ExistsById(int Id);
    }
}
