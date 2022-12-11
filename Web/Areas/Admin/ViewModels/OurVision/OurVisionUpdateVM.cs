using System.ComponentModel.DataAnnotations;

namespace Web.Areas.Admin.ViewModels.OurVision
{
    public class OurVisionUpdateVM
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public IFormFile? Photo { get; set; }
    }
}
