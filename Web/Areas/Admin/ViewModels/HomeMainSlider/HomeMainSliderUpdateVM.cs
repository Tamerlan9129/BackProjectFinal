using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Areas.Admin.ViewModels.HomeMainSlider
{
    public class HomeMainSliderUpdateVM
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Slogan { get; set; }
        [Required]
        public string ButtonLink { get; set; }
        [NotMapped]
        public IFormFile? Photo { get; set; }
        public int Order { get; set; }
    }
}
