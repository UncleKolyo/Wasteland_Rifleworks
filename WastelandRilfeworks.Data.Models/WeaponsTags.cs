namespace WastelandRilfeworks.Data.Models
{
    public class WeaponsTags
    {
        public int WeaponId { get; set; }
        public Weapon Weapon { get; set; } = null!;

        public int TagId { get; set; }
        public Tag Tag { get; set; } = null!;
    }
}
