namespace WastelandRifleworks.Web.ViewModels.Home
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using WastelandRilfeworks.Data.Models;

    public class SubmitViewModel
    {
        [Required]
        [MaxLength()]
        public string Name { get; set; } = null!;

        [Required]

        public string Description { get; set; } = null!;

        [Required]
        [Range(0,100)]
        public int Complexity { get; set; }

        [Required]
        [Range(1,100)]

        public int Rating { get; set; }

        [Required]

        public int TypeId { get; set; }

        public ICollection<Image> Images { get; set; }

        public IEnumerable<TagViewModel> Tags { get; set; } = new List<TagViewModel>();
    }
}
