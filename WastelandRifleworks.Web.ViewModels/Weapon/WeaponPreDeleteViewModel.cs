namespace WastelandRifleworks.Web.ViewModels.Weapon
{

    public class WeaponPreDeleteViewModel
    {

        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public int Rating { get; set; }

        public string Type { get; set; } = null!;

        public int Complexity { get; set; }

        public string ShortDescription { get; set; } = null!;

        public string FullDescription { get; set; } = null!;

        public List<string> TagNames { get; set; } = null!;
        public ICollection<string> ImagesPaths { get; set; } = null!;

    }
}
