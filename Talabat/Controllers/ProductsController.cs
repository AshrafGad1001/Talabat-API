using Microsoft.AspNetCore.Mvc;
using Talabat.core.Entities;
using Talabat.core.Repositorires;
using Talabat.core.Specifications;

namespace Talabat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _ProductRepo;
        public ProductsController(IGenericRepository<Product> ProductRepo)
        {
            _ProductRepo = ProductRepo;
        }

        //Api/Controller
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var spec = new ProductBrandandTypeSpecification();

            var Products = await _ProductRepo.GetAllWithSpecAsync(spec);
            return Ok(Products);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var spec = new ProductBrandandTypeSpecification(id);
            var product = await _ProductRepo.GetByIdWithSpecAsync(spec);

            return Ok(product);
        }
    }
}
