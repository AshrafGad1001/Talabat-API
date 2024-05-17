using Talabat.core.Entities;

namespace Talabat.core.Specifications
{
    public class ProductBrandandTypeSpecification : BaseSpecification<Product>
    {
        public ProductBrandandTypeSpecification()
        {
            Includes.Add(p => p.ProductBrand);
            Includes.Add(p => p.ProductType);
        }
        public ProductBrandandTypeSpecification(int id) : base(p => p.Id == id)
        {
            Includes.Add(p => p.ProductBrand);
            Includes.Add(p => p.ProductType);
        }
    }
}
