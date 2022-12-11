using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Web.Areas.Admin.ViewModels.HomeVideo
{
    public class HomeVideoUpdateVM
    {
        public int Id { get; set; }
        public string Link { get; set; }
        [Display(Name = "Cover Photo")]
        public IFormFile? CoverPhoto { get; set; }
    }
}
