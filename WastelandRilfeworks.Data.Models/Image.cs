namespace WastelandRilfeworks.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static Common.EntityValidationConstraints.Image;
    public class Image
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLenght)]
        public string FileName { get; set; }

        [ForeignKey(nameof(Weapon))]
        public int WeaponId { get; set; }
        public Weapon Weapon { get; set; }
    }
}
