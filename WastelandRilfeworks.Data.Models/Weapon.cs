namespace WastelandRilfeworks.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static Common.EntityValidationConstraints.Weapon;

    public class Weapon
    {
        public Weapon()
        {

        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLenght)]
        public string Name { get; set; } = null!;

        [Required]

        public string Description { get; set; } = null!;

        [Required]
        [Range(MinComplexity, MaxComplexity)]
        public int Complexity { get; set; }

        [Required]
        [Range(MinRating, MaxRating)]

        public int Rating { get; set; }

        [Required]
        [ForeignKey(nameof(Type))]

        public int TypeId { get; set; }

        [Required]

        public Type Type { get; set; } = null!;

        [Required]
        public Guid EngineerId { get; set; }

        [Required]
        [ForeignKey(nameof(Engineer))]

        public Engineer Engineer { get; set; } = null!;

        public virtual ICollection<Tag> Tags { get; set; }
    }
}
