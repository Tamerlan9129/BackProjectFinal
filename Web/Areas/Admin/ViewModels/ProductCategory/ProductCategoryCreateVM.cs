using System.ComponentModel.DataAnnotations;

namespace Web.Areas.Admin.ViewModels.ProductCategory
{
    public class ProductCategoryCreateVM
    {
        [Required]
        public string Title { get; set; }
    }
}
