namespace Web.Areas.Admin.ViewModels.HomeMainSlider
{
    public class HomeMainSliderDetailsVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Slogan { get; set; }
        public string ButtonLink { get; set; }
        public string PhotoName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public int Order { get; set; }
    }
}
