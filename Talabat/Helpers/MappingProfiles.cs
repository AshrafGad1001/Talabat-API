using AutoMapper;
using Talabat.APIs.DTOs;
using Talabat.core.Entities;
using Talabat.core.Entities.Identity;
using Talabat.core.Entities.OrderAggregate;
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




            CreateMap<AddressDTO, core.Entities.Identity.Address>().ReverseMap()
                 .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                 .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                 .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
                 .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                 .ForMember(dest => dest.street, opt => opt.MapFrom(src => src.street)); ;




            CreateMap<CustomerCartDTO, CustomerCart>();
            CreateMap<CartItemDTO, CartItem>();

            CreateMap<AddressDTO, core.Entities.OrderAggregate.Address>();


            CreateMap<Order, OrderReturnDTO>()
                    .ForMember(d => d.DeliveryMethod, O => O.MapFrom(s => s.DeliveryMethod.ShortName))
                    .ForMember(d => d.DeliveryMethodCost, O => O.MapFrom(s => s.DeliveryMethod.Cost));


            CreateMap<OrderItem, OrderItemDTO>()
                .ForMember(d => d.ProductId, o => o.MapFrom(s => s.Product.ProductId))
                .ForMember(d => d.ProductName, o => o.MapFrom(s => s.Product.ProductName))
                .ForMember(d => d.PictureUrl, o => o.MapFrom(s => s.Product.PictureUrl));

        }
    }
}
