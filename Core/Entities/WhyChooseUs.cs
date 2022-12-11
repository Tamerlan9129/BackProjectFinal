using Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class WhyChooseUs : BaseEntity
    {
        [Required, MaxLength(40)]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Services { get; set; }
        [Required]
        public string PhotoName { get; set; }
        [Required]
        public int ProfessionalDoctors { get; set; }
        [Required]
        public int SatisifiedPatients { get; set; }
        [Required]
        public int Quality { get; set; }
        [Required]
        public int YearExperience { get; set; }
    }
}
