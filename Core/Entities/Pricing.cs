using Core.Constants;
using Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Pricing : BaseEntity
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Features { get; set; }
        public double Price { get; set; }
        public Period Period { get; set; }
        public Status Status { get; set; }
    }
}
