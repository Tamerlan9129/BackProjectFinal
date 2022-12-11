using Microsoft.Build.Framework;

namespace Web.Areas.Admin.ViewModels.OurVision
{
    public class OurVisionCreateVM
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public IFormFile Photo { get; set; }

    }
}
