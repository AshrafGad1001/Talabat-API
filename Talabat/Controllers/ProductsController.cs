using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.core.Entities;
using Talabat.core.Repositorires;
using Talabat.core.Specifications;
using Talabat.DTOs;

namespace Talabat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _ProductRepo;

        public readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> ProductRepo, IMapper mapper)
        {
            _ProductRepo = ProductRepo;
            _mapper = mapper;
        }

        //Api/Controller
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
        {
            var spec = new ProductBrandandTypeSpecification();

            var Products = await _ProductRepo.GetAllWithSpecAsync(spec);
            return Ok(_mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(Products));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProductById(int id)
        {
            var spec = new ProductBrandandTypeSpecification(id);
            var product = await _ProductRepo.GetByIdWithSpecAsync(spec);
            if (product == null)
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok(_mapper.Map<Product, ProductDTO>(product));
        }
    }
}
