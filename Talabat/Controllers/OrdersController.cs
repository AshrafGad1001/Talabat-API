﻿using AutoMapper;
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
        public async Task<ActionResult<OrderReturnDTO>> CreateOrder(OrderDTO orderDTO)
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

                return Ok(_mapper.Map<Order, OrderReturnDTO>(order));
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "An error occurred while creating the order.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderReturnDTO>>> GetOrdersForUser()
        {
            var customerEmail = User.FindFirstValue(ClaimTypes.Email);

            var orders = await _orderService.GetOrdersForUserAsync(customerEmail);

            return Ok(_mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderReturnDTO>>(orders));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderReturnDTO>> GetOrderForUser(int id)
        {
            var customerEmail = User.FindFirstValue(ClaimTypes.Email);

            var order = await _orderService.GetOrderByIdForUserAsync(id, customerEmail);

            if (order == null)
                return BadRequest(new ApiResponse(400));

            return Ok(_mapper.Map<Order, OrderReturnDTO>(order));
        }
        [HttpGet("deliveryMethods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
        {
            var deliveryMethod = _orderService.GetDeliveryMethodsAsync();
            return Ok(deliveryMethod);
        }
    }
}
