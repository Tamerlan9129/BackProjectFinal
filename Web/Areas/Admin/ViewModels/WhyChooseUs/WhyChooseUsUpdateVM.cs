using System.ComponentModel.DataAnnotations;

namespace Web.Areas.Admin.ViewModels.WhyChooseUs
{
    public class WhyChooseUsUpdateVM
    {
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Services { get; set; }
        public IFormFile? Photo { get; set; }
     
    }
}
