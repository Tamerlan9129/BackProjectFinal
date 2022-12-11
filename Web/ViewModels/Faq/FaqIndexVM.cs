using Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Web.ViewModels.Faq
{
    public class FaqIndexVM
    {
        public List<Question> Questions { get; set; }

        [Display(Name = "Category")]
        public int? CategoryId { get; set; }

        public static implicit operator List<object>(FaqIndexVM v)
        {
            throw new NotImplementedException();
        }
    }
}
