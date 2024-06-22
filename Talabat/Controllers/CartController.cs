using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Controllers;
using Talabat.core.Entities;
using Talabat.core.Repositorires;

namespace Talabat.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : BaseApiController
    {
        private readonly ICartRepository _cartRepository;

        public CartController(ICartRepository cartRepository)
        {
            this._cartRepository = cartRepository;
        }
        [HttpGet]
        public async Task<ActionResult<CustomerCart>> GetCartById(string id)
        {
            var cart = await _cartRepository.GetCartAsync(id);
            return Ok(cart ?? new CustomerCart(id));
        }
        [HttpPost]
        public async Task<ActionResult<CustomerCart>> updateCart(CustomerCart cart)
        {
            var updatedOrCreatedCart = await _cartRepository.UpdateCartAsync(cart);
            return Ok(updatedOrCreatedCart);
        }
        [HttpDelete]
        public async Task DeleteCart(string id)
        {
            await _cartRepository.DeleteCart(id);
        }
    }
}
