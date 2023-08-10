namespace WastelandRifleworks.Services.Data.Intefaces
{
    using WastelandRifleworks.Web.ViewModels.Weapon;
    using WastelandRilfeworks.Data.Models;

    public interface IWeaponService
    {
        Task<AllWeaponsFilteredAndPagedServiceModel> AllAsync(AllWeaponsQueryModel queryModel, string wwwRootPath);

        Task InsertWeaponAsync(Weapon weapon);

        Task UpdateWeaponAsync(Weapon weapon);

        Task<IEnumerable<AllWeaponViewModel>> AllByEngineerIdAsync(string engineerId);

        Task<WeaponDetailsViewModel> GetDetailsByIdAsync(string weaponId);

        Task<bool> ExistsByIdAsync(string weaponId);

        Task<WeaponFormModel> GetWeaponForEditbyIdAsync(string weaponId);

        Task EditWeaponByIdAndFormModelAsync(string weaponId, WeaponFormModel formModel);

        Task<bool> IsEngineertWithIdCreatorOfWeaponWithIdAsync(string houseId, string agentId);

        Task<WeaponPreDeleteViewModel> GetWeaponForDeleteByIdAsync(string weaponId);

        Task DeleteWeaponByIdAsync(string weaponId);

        Task ToggleUserReactionAsync(string engineerId, string weaponId, ReactionType reactionType);
    }
}
