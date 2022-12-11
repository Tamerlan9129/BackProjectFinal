namespace Web.Areas.Admin.ViewModels.About
{
    public class AboutIndexVM
    {
        public Core.Entities.About About { get; set; }
        public ICollection<Core.Entities.AboutPhoto> Photos { get; set; }
    }
}
