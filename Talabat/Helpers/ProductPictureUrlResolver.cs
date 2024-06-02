using AutoMapper;
using Talabat.core.Entities;
using Talabat.DTOs;

namespace Talabat.Helpers
{
    public class ProductPictureUrlResolver : IValueResolver<Product, ProductDTO, string>
    {   
        private IConfiguration _configuration { get; }

        public ProductPictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(Product source, ProductDTO destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
                return $"{_configuration["BaseApiUrl"]}{source.PictureUrl}";
            }
            return null;
        }
    }
}
