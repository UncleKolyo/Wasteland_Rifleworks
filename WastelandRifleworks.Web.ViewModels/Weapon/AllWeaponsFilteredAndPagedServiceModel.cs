namespace WastelandRifleworks.Web.ViewModels.Weapon
{
    using WastelandRifleworks.Web.ViewModels.Home;

    public class AllWeaponsFilteredAndPagedServiceModel
    {

        public AllWeaponsFilteredAndPagedServiceModel()
        {
            this.Weapons = new HashSet<AllWeaponViewModel>();
        }
        public int TotalWeaponsCount { get; set; }

        public IEnumerable<AllWeaponViewModel> Weapons { get; set; }
    }
}
