using System.ComponentModel.DataAnnotations;

namespace Web.Areas.Admin.ViewModels.WhyChooseUs
{
    public class WhyChooseUsCreateVM
    {
        [Required, MaxLength(50)]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Services { get; set; }
        [Required]
        public IFormFile Photo { get; set; }


    }
}
