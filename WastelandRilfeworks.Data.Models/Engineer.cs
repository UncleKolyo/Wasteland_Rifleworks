namespace WastelandRilfeworks.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static Common.EntityValidationConstraints.Engineer;

    public class Engineer
    {
        public Engineer()
        {
            this.Id = Guid.NewGuid();
            this.EngineeredWeapons = new HashSet<Weapon>();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(NameMaxLenght)]
        public string Username { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        [Required]
        public virtual ApplicationUser User { get; set; } = null!;

        [Required]
        [Range(MinAprovement, MaxAprovement)]
        public int Aprovement { get; set; }

        public virtual ICollection<Weapon> EngineeredWeapons { get; set; }

    }
}
