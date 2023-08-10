namespace WastelandRilfeworks.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Common.EntityValidationConstraints.Type;

    public class Type
    {
        public Type()
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
