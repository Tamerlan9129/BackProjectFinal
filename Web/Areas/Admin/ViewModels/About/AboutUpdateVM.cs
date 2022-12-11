using Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Web.Areas.Admin.ViewModels.About
{
    public class AboutUpdateVM
    {
        public int Id { get; set; }
        [Required, MaxLength(14)]
        public string Header { get; set; }
        [Required, MaxLength(50)]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public IFormFile? IconPhoto { get; set; }
        public List<IFormFile>? Photos { get; set; }
        public List<AboutPhoto>? AboutPhoto { get; set; }
    }
}
