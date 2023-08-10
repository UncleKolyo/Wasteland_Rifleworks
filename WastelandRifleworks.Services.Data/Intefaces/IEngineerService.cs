namespace WastelandRifleworks.Services.Data.Intefaces
{
    using WastelandRifleworks.Web.ViewModels.Engineer;

    public interface IEngineerService
    {
        Task<bool> EngineerExistsByUserIdAsync(string userId);

        Task Create(string userId, BecomeEngineerFormModel model);

        Task<string?> GetEnginnerIdByUserIdAsync(string userId);

        Task<string?> GetEnginnerUsernameByEnginnerIdAsync(string userId);
    }
}
