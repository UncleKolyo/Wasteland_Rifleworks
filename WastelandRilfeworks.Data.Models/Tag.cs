namespace WastelandRilfeworks.Data.Models
{
    public class Tag
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Weapon Weapon { get; set; }

        public ICollection<Weapon> Weapons { get; set; }
    }
}
