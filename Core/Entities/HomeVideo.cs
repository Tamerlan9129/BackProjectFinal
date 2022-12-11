using Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class HomeVideo : BaseEntity
    {
        public string Link { get; set; }

        public string PhotoName { get; set; }
    }
}
