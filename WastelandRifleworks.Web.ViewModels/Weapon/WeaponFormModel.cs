using WastelandRifleworks.Web.ViewModels.Home;

namespace WastelandRifleworks.Web.ViewModels.Weapon
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using WastelandRifleworks.Web.ViewModels.Tag;
    using WastelandRifleworks.Web.ViewModels.Type;
    using WastelandRilfeworks.Data.Models;

    public class WeaponFormModel
    {
        public WeaponFormModel()
        {
            this.Types = new List<WeaponTypeFormModel>();
            this.Tags = new List<WeaponTagFormModel>();
        }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; } = null!;

        [Required]

        public string ShortDescription { get; set; } = null!;

        [Required]

        public string FullDescription { get; set; } = null!;

        [Required]
        [Range(0, 100)]
        public int Complexity { get; set; }

 
        [Range(1, 100)]

        public int Rating { get; set; }

        [Required]

        public int TypeId { get; set; }

        [Required]
        public int FirstTagId { get; set; }
        [Required]
        public int SecondTagId { get; set; }
        [Required]
        public int ThirdTagId { get; set; }
        [Required]
        public int ForthTagId { get; set; }
        [Required]
        public int FifthTagId { get; set; }

        [Required]
        public Guid EngineerId { get; set; }

        public ICollection<Image> Images { get; set; }

        public IEnumerable<WeaponTypeFormModel> Types { get; set; }

        public IEnumerable<WeaponTagFormModel> Tags { get; set; }
    }
}
