using Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Question : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int QuestionCategoryId { get; set; }
        public QuestionCategory QuestionCategory { get; set; }
    }
}
