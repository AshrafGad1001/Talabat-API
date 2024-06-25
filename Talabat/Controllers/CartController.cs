using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTOs;
using Talabat.Controllers;
using Talabat.core.Entities;
using Talabat.core.Repositorires;

namespace Talabat.APIs.Controllers
{
    public class CartController : BaseApiController
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;

        public CartController(ICartRepository cartRepository,IMapper mapper)
        {
            this._cartRepository = cartRepository;
            this._mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<CustomerCart>> GetCartById(string id)
        {
            var cart = await _cartRepository.GetCartAsync(id);
            return Ok(cart ?? new CustomerCart(id));
        }


        /// <summary>
        /// Mappp - CustomerCartDTO ==> CustomerCart
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<CustomerCart>> updateCart(CustomerCartDTO cart)
        {

            var mappedCart = _mapper.Map<CustomerCartDTO, CustomerCart>(cart);

            var updatedOrCreatedCart = await _cartRepository.UpdateCartAsync(mappedCart);
            return Ok(updatedOrCreatedCart);
        }
        [HttpDelete]
        public async Task DeleteCart(string id)
        {
            await _cartRepository.DeleteCart(id);
        }
    }
}
