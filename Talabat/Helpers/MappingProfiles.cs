using AutoMapper;
using Talabat.APIs.DTOs;
using Talabat.core.Entities;
using Talabat.core.Entities.Identity;
using Talabat.DTOs;
namespace Talabat.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductDTO>()
                .ForMember(d => d.ProductBrand, O => O.MapFrom(s => s.ProductBrand.Name))
                .ForMember(d => d.ProductType, O => O.MapFrom(s => s.ProductType.Name))
                .ForMember(d => d.PictureUrl, O => O.MapFrom<ProductPictureUrlResolver>());




            CreateMap<AddressDTO, Address>().ReverseMap();

            CreateMap<CustomerCartDTO, CustomerCart>();
            CreateMap<CartItemDTO, CartItem>();

        }
    }
}
