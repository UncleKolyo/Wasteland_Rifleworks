namespace WastelandRifleworks.Web.ViewModels.Home
{

    public class IndexViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

		public string Engineer { get; set; }

        public int Rating { get; set; }

        public string Type { get; set; }

        public ICollection<string> ImagesPaths { get; set; }
    }
}
