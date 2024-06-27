using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.Controllers;
using Talabat.core.Entities.OrderAggregate;
using Talabat.core.Services;

namespace Talabat.APIs.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrdersController : BaseApiController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(IOrderService orderService,
                        IMapper mapper, ILogger<OrdersController> logger)
        {
            this._orderService = orderService;
            this._mapper = mapper;
            this._logger = logger;
        }


        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(OrderDTO orderDTO)
        {
            try
            {
                //Get CustomerEmail
                var CustomerEmail = User.FindFirstValue(ClaimTypes.Email);


                var orderAddress = _mapper.Map<AddressDTO, Address>(orderDTO.ShippingAddress);

                var order = await _orderService.CreateOrderAsync
                                (CustomerEmail, orderDTO.CartId, orderDTO.DeliveryMethodId, orderAddress);

                if (order == null)
                    return BadRequest(new ApiResponse(400, "Find Issue In OrderProcessind"));

                return Ok(order);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "An error occurred while creating the order.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
