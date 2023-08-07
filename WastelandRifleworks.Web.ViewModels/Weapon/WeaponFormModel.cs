using WastelandRifleworks.Web.ViewModels.Home;

namespace WastelandRifleworks.Web.ViewModels.Weapon
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using WastelandRifleworks.Web.ViewModels.Type;
    using WastelandRilfeworks.Data.Models;

    public class WeaponFormModel
    {
        public WeaponFormModel()
        {
            this.Types = new List<WeaponTypeFormModel>();
        }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; } = null!;

        [Required]

        public string Description { get; set; } = null!;

        [Required]
        [Range(0, 100)]
        public int Complexity { get; set; }

 
        [Range(1, 100)]

        public int Rating { get; set; }

        [Required]

        public int TypeId { get; set; }

        [Required]
        public Guid EngineerId { get; set; }

        public ICollection<Image> Images { get; set; }

        public IEnumerable<WeaponTypeFormModel> Types { get; set; }
    }
}
