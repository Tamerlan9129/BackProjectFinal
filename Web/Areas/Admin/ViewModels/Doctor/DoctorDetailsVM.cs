using Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace Web.Areas.Admin.ViewModels.Doctor
{
    public class DoctorDetailsVM
    {
        public int Id { get; set; }
        
        public string FullName { get; set; }

        public Position Position { get; set; }
        public string Qualification { get; set; }
        public string Info { get; set; }
        public string Introducing { get; set; }

        
        public string Phone { get; set; }

        public string PhotoName { get; set; }

        public string Email { get; set; }
        public IFormFile Photo { get; set; }
        public DateTime MondayToFridayStart { get; set; }
        public DateTime MondayToFridayEnd { get; set; }
        public DateTime SaturdayStart { get; set; }
        public DateTime SaturdayEnd { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public bool SundayIsWorking { get; set; }
        public bool ShowInHome { get; set; }
        public string Skills { get; set; }
        public string? Twitter { get; set; }
        public string? Facebook { get; set; }
        public string? LinkedIn { get; set; }
    }
}
