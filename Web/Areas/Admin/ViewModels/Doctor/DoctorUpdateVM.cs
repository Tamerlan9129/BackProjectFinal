using Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace Web.Areas.Admin.ViewModels.Doctor
{
    public class DoctorUpdateVM
    {
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public Position Position { get; set; }
        public string Qualification { get; set; }
        public string Info { get; set; }
        public string Introducing { get; set; }

        [Required, MaxLength(40)]
        public string Phone { get; set; }
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public IFormFile Photo { get; set; }
        public DateTime MondayToFridayStart { get; set; } = DateTime.Parse("8:00");
        public DateTime MondayToFridayEnd { get; set; } = DateTime.Parse("15:00");
        public DateTime SaturdayStart { get; set; } = DateTime.Parse("13:00");
        public DateTime SaturdayEnd { get; set; } = DateTime.Parse("17:00");
        public bool SundayIsWorking { get; set; }
        public bool ShowInHome { get; set; }
        public string Skills { get; set; }
        public string? Twitter { get; set; }
        public string? Facebook { get; set; }
        public string? LinkedIn { get; set; }
    }
}
