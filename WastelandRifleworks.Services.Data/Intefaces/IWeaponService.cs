namespace WastelandRifleworks.Services.Data.Intefaces
{
    using WastelandRifleworks.Web.ViewModels.Home;
    using WastelandRifleworks.Web.ViewModels.Weapon;
    using WastelandRilfeworks.Data.Models;

    public interface IWeaponService
    {
        Task<IEnumerable<AllWeaponViewModel>> LastTwentyWeapon(string wwwrootPath);

        Task<AllWeaponsFilteredAndPagedServiceModel> AllAsync(AllWeaponsQueryModel queryModel, string wwwRootPath);

        Task InsertWeaponAsync(Weapon weapon);

        Task UpdateWeaponAsync(Weapon weapon);

        Task<IEnumerable<AllWeaponViewModel>> AllByEngineerIdAsync(string engineerId);

    }
}
