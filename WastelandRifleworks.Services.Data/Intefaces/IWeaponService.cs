namespace WastelandRifleworks.Services.Data.Intefaces
{
    using WastelandRifleworks.Web.ViewModels.Home;
    using WastelandRilfeworks.Data.Models;

    public interface IWeaponService
    {
        Task<IEnumerable<IndexViewModel>> LastTwentyWeapon(string wwwrootPath);

        Task InsertWeaponAsync(Weapon weapon);

        Task UpdateWeaponAsync(Weapon weapon);



    }
}
