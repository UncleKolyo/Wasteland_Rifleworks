namespace WastelandRifleworks.Web.ViewModels.Weapon
{
    using WastelandRifleworks.Web.ViewModels.Home;
    using WastelandRifleworks.Web.ViewModels.Tag;
    using WastelandRifleworks.Web.ViewModels.Weapon.Enums;

    public class AllWeaponsQueryModel
    {
        public AllWeaponsQueryModel()
        {
            this.CurrentPage = 1;
            this.WeaponPerPage = 20;

            this.Tags = new HashSet<string>();
            this.Types = new HashSet<string>();
            this.Weapons = new HashSet<AllWeaponViewModel>();
        }

        public string? Type { get; set; }

        public string Tag { get; set; }
        public string SearchTerm { get; set; }

        public WeaponSorting Sorting { get; set; }

        public int CurrentPage { get; set; }

        public int TotalWeapons { get; set; }

        public int WeaponPerPage { get; set; }

        public IEnumerable<string> Types { get; set; }

        public IEnumerable<string> Tags { get; set; }

        public IEnumerable<AllWeaponViewModel> Weapons { get; set; }
    }
}
