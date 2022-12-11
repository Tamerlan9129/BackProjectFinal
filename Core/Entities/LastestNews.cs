using Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class LastestNews : BaseEntity
    {
        public string Title { get; set; }
        public int Order { get; set; }
        public string Type { get; set; }
        public string PhotoName { get; set; }
    }
}
