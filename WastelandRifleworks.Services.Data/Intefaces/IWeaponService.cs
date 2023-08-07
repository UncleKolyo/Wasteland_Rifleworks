namespace WastelandRifleworks.Services.Data.Intefaces
{
    using WastelandRifleworks.Web.ViewModels.Home;
    using WastelandRifleworks.Web.ViewModels.Weapon;
    using WastelandRilfeworks.Data.Models;

    public interface IWeaponService
    {
        Task<IEnumerable<IndexViewModel>> LastTwentyWeapon(string wwwrootPath);

        Task InsertWeaponAsync(Weapon weapon);

        Task UpdateWeaponAsync(Weapon weapon);
        //Task UpdateWeaponAsync(WeaponFormModel model);
    }
}
