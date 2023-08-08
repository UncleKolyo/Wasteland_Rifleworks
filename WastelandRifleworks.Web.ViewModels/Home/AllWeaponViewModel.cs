namespace WastelandRifleworks.Web.ViewModels.Home
{

    public class AllWeaponViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

		public string Engineer { get; set; }

        public int Rating { get; set; }

        public string Type { get; set; }

        public int Complexity { get; set; }

        public string Description { get; set; }

        public List<string> TagNames { get; set; }
        public ICollection<string> ImagesPaths { get; set; }
    }
}
