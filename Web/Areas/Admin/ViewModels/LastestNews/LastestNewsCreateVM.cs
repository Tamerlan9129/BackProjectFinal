using System.ComponentModel.DataAnnotations;

namespace Web.Areas.Admin.ViewModels.LatestNews
{
    public class LastestNewsCreateVM
    {
        [Required]
        public string Title { get; set; }
        public int Order { get; set; }
        [Required]
        public string Type { get; set; }
        public IFormFile Photo { get; set; }
    }
}
