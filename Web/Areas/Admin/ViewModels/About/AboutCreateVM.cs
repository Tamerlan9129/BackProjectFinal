using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Web.Areas.Admin.ViewModels.About
{
    public class AboutCreateVM
    {
        [Required, MaxLength(20)]
        public string Header { get; set; }
        [Required]
        public string Description { get; set; }
        [Required, MaxLength(50)]
        public string Title { get; set; }
        public IFormFile IconPhoto { get; set; }
        [NotMapped]
        public List<IFormFile> Photos { get; set; }
    }
}
