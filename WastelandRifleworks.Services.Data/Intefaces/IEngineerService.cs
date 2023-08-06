namespace WastelandRifleworks.Services.Data.Intefaces
{
    using WastelandRifleworks.Web.ViewModels.Home;
    using WastelandRilfeworks.Data.Models;

    public interface IEngineerService
    {
        Task<bool> EngineerExistsByUserIdAsync(string userId);

    }
}
