using Talabat.core.Entities;

namespace Talabat.core.Specifications
{
    public class ProductWithFiltersForCountSpecfication : BaseSpecification<Product>
    {
        public ProductWithFiltersForCountSpecfication(ProductSpecParms productParms) 
            : base(P => (!productParms.BrandId.HasValue || P.ProductBrandId == productParms.BrandId.Value) &&
                        (!productParms.TypeId.HasValue || P.ProductTypeId == productParms.TypeId.Value))
        {

        }
    }
}
