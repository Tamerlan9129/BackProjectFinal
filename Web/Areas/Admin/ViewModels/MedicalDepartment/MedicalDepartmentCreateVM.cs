using System.ComponentModel.DataAnnotations;

namespace Web.Areas.Admin.ViewModels.MedicalDepartment
{
    public class MedicalDepartmentCreateVM
    {
        [Required, MaxLength(25)]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public IFormFile Photo { get; set; }
    }
}
