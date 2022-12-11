using Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class MedicalDepartment : BaseEntity
    {
        [Required, MaxLength(25)]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public string PhotoName { get; set; }
    }
}
