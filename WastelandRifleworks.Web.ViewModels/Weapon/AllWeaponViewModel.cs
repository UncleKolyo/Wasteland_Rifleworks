namespace WastelandRifleworks.Web.ViewModels.Weapon
{

    public class AllWeaponViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Engineer { get; set; } = null!;

        public int Rating { get; set; }

        public string Type { get; set; } = null!;

        public int Complexity { get; set; }

        public string Description { get; set; } = null!;

        public List<string> TagNames { get; set; } = null!;
        public ICollection<string> ImagesPaths { get; set; } = null!;
    }
}
