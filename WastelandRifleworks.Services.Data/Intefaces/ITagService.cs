namespace WastelandRifleworks.Services.Data.Intefaces
{
    using WastelandRifleworks.Web.ViewModels.Tag;
    using WastelandRilfeworks.Data.Models;

    public interface ITagService
    {
        Task<IEnumerable<WeaponTagFormModel>> AllTagsAsync();

        Task<bool> ExistsById(int Id);

        Task<IEnumerable<WeaponTagFormModel>> FiveSelectedTagsAsync(int FirstId, int SecondId, int ThirdId, int ForthId, int FifthId);

        Task<IEnumerable<string>> AllNamesAsync();

        Task<int> GetFirstTagIdAsync();
    }
}
