using Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Product : BaseEntity
    {
        public string Title { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string Photoname { get; set; }
        public int CategoryId { get; set; }
        public ProductCategory Category { get; set; }
        public ICollection<BasketProduct> BasketProducts { get; set; }
    }
}
