using System.ComponentModel.DataAnnotations;

namespace Web.Areas.Admin.ViewModels.MedicalDepartment
{
    public class MedicalDepartmentUpdateVM
    {
        public int Id { get; set; }
        [Required, MaxLength(25)]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public IFormFile? Photo { get; set; }
    }
}
