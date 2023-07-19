namespace WastelandRifleworks.Services.Data.Intefaces
{
    using WastelandRifleworks.Web.ViewModels.Home;

    public interface IWeaponService
    {
        Task<IEnumerable<IndexViewModel>> LastTwentyWeapon(string wwwrootPath);
    }
}
