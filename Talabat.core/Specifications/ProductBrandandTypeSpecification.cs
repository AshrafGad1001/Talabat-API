using Talabat.core.Entities;

namespace Talabat.core.Specifications
{
    public class ProductBrandandTypeSpecification : BaseSpecification<Product>
    {
        public ProductBrandandTypeSpecification(ProductSpecParms productParms)
            : base(P => (!productParms.BrandId.HasValue || P.ProductBrandId == productParms.BrandId.Value) &&
                        (!productParms.TypeId.HasValue || P.ProductTypeId == productParms.TypeId.Value)
                  )
        {
            Includes.Add(p => p.ProductBrand);
            Includes.Add(p => p.ProductType);


            ApplyPagination(productParms.PageSize * (productParms.pageIndex - 1), productParms.PageSize);
            if (!string.IsNullOrEmpty(productParms.sort))
            {
                switch (productParms.sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }
            }

        }
        public ProductBrandandTypeSpecification(int id) : base(p => p.Id == id)
        {
            Includes.Add(p => p.ProductBrand);
            Includes.Add(p => p.ProductType);
        }
    }
}
