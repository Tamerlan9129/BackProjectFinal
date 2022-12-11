using Core.Entities;

namespace Web.ViewModels.Home
{
    public class HomeIndexVM
    {
        public List<HomeMainSlider> HomeMainSlider { get; set; }
        public List<Core.Entities.Doctor> Doctors { get; set; }
        public List<MedicalDepartment> MedicalDepartments { get; set; }
        public List<OurVision> OurVision { get; set; }
        public WhyChooseUs WhyChooseUs { get; set; }
        public About About { get; set; }
        public List<AboutPhoto> AboutPhoto { get; set; }
        public HomeVideo HomeVideo { get; set; }
        public List<LastestNews> LastestNews { get; set; }
        public List<Statistic> Statistics { get; set; }
    }
}
