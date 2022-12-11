using System.ComponentModel.DataAnnotations;

namespace Web.Areas.Admin.ViewModels.Statistic
{
    public class StatisticUpdateVM
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Quantity { get; set; }
    }
}
