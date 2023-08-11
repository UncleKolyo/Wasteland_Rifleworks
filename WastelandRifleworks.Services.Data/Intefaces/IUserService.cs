namespace WastelandRifleworks.Services.Data.Intefaces
{
    using Web.ViewModels.User;

    public interface IUserService
    {
        Task<string> GetFullUsernameNameByIdAsync(string userId);

        Task<IEnumerable<UserViewModel>> AllAsync();
    }
}
