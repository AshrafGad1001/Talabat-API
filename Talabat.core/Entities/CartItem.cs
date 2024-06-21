using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.core.Entities
{
    public class CartItem : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal price { get; set; }
        public string pictureUrl { get; set; }
        public string Brand { get; set; }
        public string Type { get; set; }

    }
}
