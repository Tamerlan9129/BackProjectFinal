using Core.Entities;

namespace Web.Areas.Admin.ViewModels.About
{
    public class AboutDetailsVM
    {
        public int Id { get; set; }
        public string Header { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string IconPhoto { get; set; }


        public List<AboutPhoto> Photos { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
