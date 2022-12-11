using Core.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.ViewModels.Doctor
{
    public class DoctorIndexVM
    {
        public List<Core.Entities.Doctor> Doctors { get; set; }
        public int Page { get; set; } = 1;
        public int Take { get; set; } = 3;
        public int PageCount { get; set; }
        public string? Name { get; set; }
    }
}
