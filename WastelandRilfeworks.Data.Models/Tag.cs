namespace WastelandRilfeworks.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Common.EntityValidationConstraints.Tag;

    public class Tag
    {
        public Tag()
        {
            this.Weapons = new HashSet<Weapon>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLenght)]
        public string Name { get; set; } = null!;

        public virtual ICollection<Weapon> Weapons { get; set; }
    }
}
