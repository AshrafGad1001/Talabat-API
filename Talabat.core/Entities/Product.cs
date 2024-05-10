using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.core.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }







        public int ProductBrandId { get; set; }
        //Nav Prop
        public ProductBrand ProductBrand { get; set; }
        public int ProductTypeId { get; set; }
        //Nav Prop
        public ProductType ProductType { get; set; }
    }
}
