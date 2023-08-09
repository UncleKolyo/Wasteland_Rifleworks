namespace WastelandRilfeworks.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static Common.EntityValidationConstraints.Weapon;

    public class Weapon
    {
        public Weapon()
        {

            this.Images = new HashSet<Image>();
            this.Tags = new HashSet<Tag>();

        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLenght)]
        public string Name { get; set; } = null!;



        [Required]
        public string ShortDescription { get; set; } = null!;

        [Required]
        public string FullDescription { get; set; } = null!;

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
        [ForeignKey(nameof(Engineer))]
        public Guid EngineerId { get; set; }


        public ICollection<Image> Images { get; set; }

        public DateTime CreatedOn { get; set; }


        [ForeignKey(nameof(WeaponSchematic))]

        public int WeaponSchematicId { get; set; }

        public Schematic WeaponSchematic { get; set; }

        [Required]

        public Engineer Engineer { get; set; } = null!;

        public virtual ICollection<Tag> Tags { get; set; }
    }
}
