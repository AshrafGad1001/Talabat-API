using Microsoft.AspNetCore.Mvc;
using Talabat.core.Entities;
using Talabat.core.Repositorires;

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
            var Products = await _ProductRepo.GetAllAsync();
            return Ok(Products);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product =await _ProductRepo.GetByIdAsync(id);

            return Ok(product);
        }
    }
}
